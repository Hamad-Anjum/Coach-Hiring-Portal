using System;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using static System.IO.File;

namespace CHS.Api.Controllers.Common
{
    public class ProfileController : CommonController
    {
        private readonly CHSDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(CHSDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetEverything")]
        public async Task<IActionResult> GetEverything(Guid id)
        {
            if (id == default)
            {
                return BadRequest();
            }

            Guid loggedInUser = default;
            if (Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id")).Value, out Guid result))
            {
                loggedInUser = result;
            }

            var profile = await _db.AspNetUsers.Where(x => x.Id.Equals(id)).Include(x => x.Designations).Select(x => new ProfileViewModel
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                //ProfilePicture = string.IsNullOrEmpty(x.ProfilePicture) ? "Images/profile.png" : x.ProfilePicture,
                ProfilePicture = x.ProfilePicture,
                //CoverPicture = string.IsNullOrEmpty(x.CoverPicture) ? "Images/cover.jpg" : x.CoverPicture,
                CoverPicture = x.CoverPicture,
                Email = x.Email,
                Designation = x.Designations.FirstOrDefault().Title
            }).FirstOrDefaultAsync();

            var model = new ProfileViewModel
            {
                UserId = profile.UserId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                ProfilePicture = !string.IsNullOrEmpty(profile.ProfilePicture) ? profile.ProfilePicture.Split(Constant.WwwRoot)[1].Replace('\\', '/') : "Images/profile.png",
                //ProfilePicture = Exists(profile.ProfilePicture) ? profile.ProfilePicture.Split(Constant.WwwRoot)[1] : "Images/profile.png",
                CoverPicture = !string.IsNullOrEmpty(profile.CoverPicture) ? profile.CoverPicture.Split(Constant.WwwRoot)[1].Replace('\\', '/') : "Images/cover.jpg",
                //CoverPicture = Exists(profile.CoverPicture) ? profile.CoverPicture.Split(Constant.WwwRoot)[1].Replace('\\', '/') : "Images/cover.jpg",
                Email = profile.Email,
                Designation = profile.Designation,
                Followers = await _db.Followers.Where(x => x.UserId.Equals(id)).CountAsync(),
                Followings = await _db.Followings.Where(x => x.UserId.Equals(id)).CountAsync(),
                IsFollowThisUser = await _db.Followers.AnyAsync(x => x.FollowerId.Equals(loggedInUser) && x.UserId.Equals(id)),

                Posts = await _db.Posts.Where(x => x.ApplicationUserId.Equals(id) && !x.IsDelete).Include(x => x.PostImages).Include(x => x.PostLikes).Include(x => x.PostComments).Select(x => new PostViewModel
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Detail = x.Description,
                    Image = string.IsNullOrEmpty(x.PostImages.FirstOrDefault().Picture) ? string.Empty : x.PostImages.FirstOrDefault().Picture,
                    DateTime = (DateTime)x.CreatedAt,
                    LikedThisPost = x.PostLikes.Any(x => x.ApplicationUserId.Equals(loggedInUser)),
                    Likes = x.PostLikes.Count(),
                    Replies = x.PostComments.Where(x => x.PostId.Equals(x.Id)).Select(x => x.Comments).ToList()
                }).ToListAsync()
            };

            foreach (var item in model.Posts)
            {
                if (!string.IsNullOrEmpty(item.Image))
                    item.Image = item.Image.Split(Constant.WwwRoot)[1].Replace('\\', '/');
            }

            return Ok(model);
        }

        [HttpPost]
        [Route("UserProfile")]
        public async Task<IActionResult> UserProfile(PostUploadViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.ProfilePicture = model.File;
                user.LogoPicture = model.File.Replace("ProfilePictures", "Logos");
                user.MainSiteImage = model.File.Replace("ProfilePictures", "MainSiteImage");
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            return UnprocessableEntity();
        }

        [HttpPost]
        [Route("UserCover")]
        public async Task<IActionResult> UserCover(PostUploadViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.CoverPicture = model.File;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            return UnprocessableEntity();
        }

    }
}
