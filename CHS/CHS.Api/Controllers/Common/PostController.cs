using System;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    public class PostController : CommonController
    {
        private readonly CHSDbContext _db;

        public PostController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id:guid}")]
        public string GetById(Guid id)
        {
            return "";
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostUploadViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var post = await _db.Posts.AddAsync(new Post
            {
                Id = model.PostId,
                ApplicationUserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value),
                Title = model.Text,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });

            if (model.PostImages.Count > 0)
            {
                foreach (var item in model.PostImages)
                {
                    await _db.PostImages.AddAsync(new PostImage
                    {
                        PostId = post.Entity.Id,
                        Picture = item.Picture,
                        ContentType = item.ContentType,
                        SortOrder = item.SortOrder,
                        CreatedAt = DateTime.Now,
                        CreatedBy = HttpContext.User.Identity.Name
                    });
                }
            }

            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok(new PostViewModel
                {
                    PostId = post.Entity.Id,
                    Title = post.Entity.Title,
                    Detail = post.Entity.Description,
                    DateTime = (DateTime)post.Entity.CreatedAt,
                    Image = model.PostImages.FirstOrDefault().Picture.Split(Constant.WwwRoot)[1].Replace('\\', '/')
                });
            }

            return UnprocessableEntity();
        }

        [HttpPost]
        [Route("Like")]
        public async Task<IActionResult> Like(LikePostViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var result = _db.PostLikes.AddAsync(new PostLike
            {
                PostId = model.PostId,
                ApplicationUserId = model.UserId,
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });

            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok(await _db.PostLikes.Where(x => x.PostId.Equals(model.PostId)).CountAsync());
            }
            return UnprocessableEntity();
        }

        [HttpPost]
        [Route("UnLike")]
        public async Task<IActionResult> UnLike(LikePostViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var result = _db.PostLikes.Remove(await _db.PostLikes.Where(x => x.ApplicationUserId.Equals(model.UserId) && x.PostId.Equals(model.PostId)).FirstOrDefaultAsync());
            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok(await _db.PostLikes.Where(x => x.PostId.Equals(model.PostId)).CountAsync());
            }
            return UnprocessableEntity();
        }

        [HttpPost]
        [Route("Follow")]
        public async Task<IActionResult> Follow(FollowViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            await _db.Followers.AddAsync(new Follower
            {
                FollowerId = model.FollowerId,
                UserId = model.UserId,
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });
            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok(await _db.Followers.Where(x => x.UserId.Equals(model.UserId)).CountAsync());
            }
            return UnprocessableEntity();
        }

        [HttpPost]
        [Route("UnFollow")]
        public async Task<IActionResult> UnFollow(FollowViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            _db.Followers.Remove(await _db.Followers.Where(x => x.FollowerId.Equals(model.FollowerId) && x.UserId.Equals(model.UserId)).FirstOrDefaultAsync());
            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok(await _db.Followers.Where(x => x.UserId.Equals(model.UserId)).CountAsync());
            }
            return UnprocessableEntity();
        }

        [HttpPut]
        [Route("EditPost")]
        public async ValueTask<IActionResult> EditPost(Guid id, PostViewModel model)
        {
            if (model is null || id == default)
            {
                return BadRequest();
            }
            var post = await _db.Posts.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            post.Title = model.Title;
            _db.Entry(post).State = EntityState.Modified;
            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("DeletePost")]
        public async ValueTask<IActionResult> DeletePost(Guid id, PostViewModel model)
        {
            var post = await _db.Posts.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            post.IsDelete = true;
            _db.Entry(post).State = EntityState.Modified;
            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
