using System;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers.Common
{
    public class UsersController : CommonController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CHSDbContext _db;

        public UsersController(UserManager<ApplicationUser> userManager, CHSDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet]
        [Route("GetUserId")]
        public async Task<Guid> GetUserId(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                return user.Id;
            }
            return default;
        }

        [HttpGet]
        [Route("GetProfilePictureById")]
        public async Task<IActionResult> GetProfilePictureById()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.Claims.Where(x => x.Type.Equals("id")).FirstOrDefault().Value);
            if (user != null)
            {
                return Ok(new PostImageViewModel { Picture = string.IsNullOrEmpty(user.LogoPicture) ? "Jobie/images/profile/17.jpg" : user.LogoPicture.Split(Constant.WwwRoot)[1].Replace('\\', '/') });
            }
            return NotFound();
        }

        [HttpGet]
        [Route("RemoveProfile")]
        public async Task<IActionResult> RemoveProfile()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.Claims.Where(x => x.Type.Equals("id")).FirstOrDefault().Value);
            if (user != null)
            {
                user.ProfilePicture = null;
                user.LogoPicture = null;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Route("RemoveCover")]
        public async Task<IActionResult> RemoveCover()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.Claims.Where(x => x.Type.Equals("id")).FirstOrDefault().Value);
            if (user != null)
            {
                user.CoverPicture = null;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
