using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CHS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<LoginViewModel> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            //if (user.FilledSurveyForm)
            //{
            if (await _signInManager.CanSignInAsync(user))
            {
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    model.FilledSurveyForm = user.FilledSurveyForm;
                    model.Id = user.Id;
                    try
                    {
                        RefreshToken token = RefreshToken();
                        token.ApplicationUserId = user.Id;
                        //await _db.RefreshTokens.AddAsync(token);
                        //await _db.SaveChangesAsync();
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            UserWithToken userWithToken = new UserWithToken { UserId = user.Id, RefreshToken = token.Token };
                            //return Ok(new { token = Token(model.Email, model.Password) });
                            var roles = await _userManager.GetRolesAsync(user: user);

                            for (int i = 0; i < roles.Count; i++)
                            {
                                model.Role += "," + roles[i];
                            }
                            if (!string.IsNullOrEmpty(model.Role))
                            {
                                model.Role = model.Role.Trim(',');
                            }
                            else
                            {
                                model.Role = string.Empty;
                            }

                            //await _signInManager.SignInAsync(user, model.RememberMe);

                            model.Id = user.Id;
                            if (user.Id != default)
                            {
                                model.MemberId = user.Id;
                            }
                            //model.ProfileImage = user.ProfilePicture;
                            userWithToken.AccessToken = AccessToken(model);
                            model.AccessToken = AccessToken(model);
                            model.Id = user.Id;
                        }
                        //model.RefreshToken = token.Token;
                        return model;
                    }
                    catch (Exception)
                    {

                    }
                }
                //}
                return model;
            }
            return null;
        }

        [HttpPost]
        [Route("Logout")]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpGet]
        [Route("CheckUsername")]
        public async Task<bool> CheckUsername(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("CheckEmail")]
        public async Task<bool> CheckEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<RegisterViewModel> Register(RegisterViewModel model)
        {
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    CreatedAt = DateTime.Now,
                    CreatedBy = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var registeredUser = await _userManager.FindByIdAsync(user.Id.ToString());
                    if (registeredUser == null)
                        return null;

                    RefreshToken token = RefreshToken();
                    token.ApplicationUserId = registeredUser.Id;
                    if (await _roleManager.RoleExistsAsync(model.Role))
                    {
                        UserWithToken userWithToken = new UserWithToken { UserId = user.Id, RefreshToken = token.Token };

                        var assigned = await _userManager.AddToRoleAsync(registeredUser, model.Role);
                        userWithToken.AccessToken = AccessToken(model.Email, model.Role, user.Id);
                        if (assigned.Succeeded)
                        {
                            return new RegisterViewModel
                            {
                                Email = registeredUser.Email,
                                UserName = registeredUser.UserName,
                                Role = model.Role,
                                AccessToken = AccessToken(model.Email, model.Role, user.Id),
                                Id = registeredUser.Id,
                                FilledSurveyForm = false
                            };
                        }
                    }
                    else
                    {
                        UserWithToken userWithToken = new UserWithToken { UserId = user.Id, RefreshToken = token.Token };
                        var roleCreated = await _roleManager.CreateAsync(new ApplicationRole { Name = model.Role, IsEnable = true });
                        if (roleCreated.Succeeded)
                        {
                            var assigned = await _userManager.AddToRoleAsync(registeredUser, model.Role);
                            userWithToken.AccessToken = AccessToken(model.Email, model.Role, user.Id);
                            if (assigned.Succeeded)
                            {
                                return new RegisterViewModel
                                {
                                    Email = registeredUser.Email,
                                    UserName = registeredUser.UserName,
                                    Role = model.Role,
                                    AccessToken = AccessToken(model.Email, model.Role, user.Id),
                                    Id = registeredUser.Id
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return null;
            }
        }

        private string AccessToken(string email, string role, Guid id)
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, email),
                    new Claim("id", id.ToString()),
                    new Claim("memberid", id.ToString()),
                    new Claim("FilledSurveyForm", "false"),
                    new Claim(ClaimTypes.Role, role)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddHours(7), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string AccessToken(LoginViewModel model)
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim("id", model.Id.ToString()),
                    new Claim("memberid", model.Id.ToString()),
                    new Claim("FilledSurveyForm", model.FilledSurveyForm.ToString()),
                    //new Claim("ProfileImage", model.ProfileImage),
                    new Claim(ClaimTypes.Role, model.Role)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddHours(7), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken RefreshToken()
        {
            RefreshToken token = new RefreshToken();
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token.Token = Convert.ToBase64String(randomNumber);
            }
            token.ExpiryDate = DateTime.UtcNow.AddHours(7);
            return token;
        }
    }
}
