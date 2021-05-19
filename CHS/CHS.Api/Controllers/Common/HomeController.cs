
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    public class HomeController : CommonController
    {
        private readonly CHSDbContext _db;

        public HomeController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Guid id = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "memberid").Value);
            if (id == default)
            {
                return BadRequest();
            }

            HomeViewModel model = new() { MemberSkills = new List<MemberSkillViewModel>() };

            model.MemberSkills = await _db.MemberSkills.Where(x => x.ApplicationUserId == id).Include(x => x.ApplicationUser).ThenInclude(x => x.Designations).Join(_db.Skills,
                m => m.ApplicationUserId,
                s => s.Id,
                (m, s) => new MemberSkillViewModel
                {
                    Name = s.Name,
                    YearsAsTrainer = m.YearsAsTrainer,
                    YearsInTraining = m.YearsInTraining,
                    ExpertLevel = m.ExpertLevel,
                }).ToListAsync();

            var user = await _db.AspNetUsers.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            //model.ProfilePicture = user.ProfilePicture;

            model.ProfilePicture = string.IsNullOrEmpty(user.ProfilePicture) ? "Images/avatar1.png" : user.ProfilePicture.Split(Constant.WwwRoot)[1].Replace('\\', '/');
            //model.ProfilePicture = Exists(model.ProfilePicture) ? model.ProfilePicture.Split(Constant.WwwRoot)[1].Replace('\\', '/') : "Images/profile.png";

            //if (string.IsNullOrEmpty(model.ProfilePicture))
            //{
            //    model.ProfilePicture = "Images/profile.png";
            //}
            model.TotalMembers = await _db.GymMembers.Where(x => x.GymId == id).CountAsync();
            model.Designation = await _db.Designations.Where(x => x.ApplicationUserId == id).Select(x => x.Title).FirstOrDefaultAsync();
            model.ActiveMembers = await _db.GymMembers.Where(x => x.GymId == id && !x.IsActive).CountAsync();

            return Ok(model);
        }
    }
}
