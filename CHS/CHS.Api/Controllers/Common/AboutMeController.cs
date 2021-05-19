using System;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    public class AboutMeController : CommonController
    {
        private readonly CHSDbContext _db;

        public AboutMeController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _db.AspNetUsers.Where(x => x.Id.Equals(id)).Include(x => x.MemberSkills).ThenInclude(x => x.Skill).Include(x => x.MemberLanguages).ThenInclude(x => x.Language).Include(x => x.Addresses).ThenInclude(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country).FirstOrDefaultAsync();

            if (result==null)
            {
                return NotFound();
            }
            var model = new AboutMeViewModel
            {
                BasicInfo = new BasicInfoViewModel
                {
                    MemberId = result.Id,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Address = result.Addresses.FirstOrDefault().AddressTitle,
                    Email = result.Email,
                    DistrictName = result.Addresses.FirstOrDefault().District.Name,
                    CityName = result.Addresses.FirstOrDefault().District.City.CityName,
                    StateName = result.Addresses.FirstOrDefault().District.City.State.StateName,
                    CountryName = result.Addresses.FirstOrDefault().District.City.State.Country.CountryName,
                    AvaiableToHireOrSpaceAvailable = result.AvaiableToHireOrSpaceAvailable,
                    TellAboutYou = result.AboutYourSelf
                },
                YearsOfExperience = result.YearsOfExperience,
                Skills = result.MemberSkills.Where(x => x.ApplicationUserId == result.Id).Select(x => new SkillViewModel
                {
                    Id = x.Skill.Id,
                    Name = x.Skill.Name
                }).ToList(),
                Languages = result.MemberLanguages.Where(x => x.MemberId == result.Id).Select(x => new LanguageViewModel
                {
                    Id = x.Language.Id,
                    LangaugeName = x.Language.Name
                }).ToList()
            };

            return Ok(model);
        }
    }
}
