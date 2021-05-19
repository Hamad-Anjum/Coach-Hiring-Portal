using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    public class SkillsController : CommonController
    {
        private readonly CHSDbContext _db;

        public SkillsController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async IAsyncEnumerable<SkillViewModel> GetSkills()
        {
            await foreach (var item in _db.Skills.AsAsyncEnumerable())
            {
                yield return new SkillViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                };
            }
        }

        [HttpPost]
        [Route("AddSkill")]
        public async Task<HttpStatusCode> AddSkill(string skill)
        {
            if (string.IsNullOrEmpty(skill))
            {
                return HttpStatusCode.BadRequest;
            }

            var result = await _db.Skills.AnyAsync(x => x.Name.ToLower().Equals(skill.ToLower().Trim()));
            if (!result)
            {
                await _db.Skills.AddAsync(new Skill
                {
                    Name = skill,
                    Index = 0,
                    IsDelete = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = HttpContext.User.Identity.Name
                });
                if (await _db.SaveChangesAsync() > 0)
                {
                    return HttpStatusCode.OK;
                }
            }
            return HttpStatusCode.BadRequest;
        }
    }
}
