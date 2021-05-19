using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    [Authorize(Roles = "Trainer,Gym")]
    public class SurveyController : CommonController
    {
        private readonly CHSDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SurveyController(CHSDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("SurveyForm")]
        public async Task<IActionResult> SurveyForm()
        {
            SurveyGetViewModel model = new()
            {
                Skills = new List<SkillViewModel>(),
                Certifications = new List<CertificationViewModel>(),
                Countries = new List<CountryViewModel>(),
                Genders = new List<GenderViewModel>()
            };

            await foreach (var item in _db.Skills.AsAsyncEnumerable())
            {
                model.Skills.Add(new SkillViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            await foreach (var item in _db.Certifications.AsAsyncEnumerable())
            {
                model.Certifications.Add(new CertificationViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            await foreach (var item in _db.Countries.Where(x => !x.IsDelete).AsAsyncEnumerable())
            {
                model.Countries.Add(new CountryViewModel
                {
                    Id = item.Id,
                    Name = item.CountryName
                });
            }

            await foreach (var item in _db.Genders.Where(x => x.IsActive && !x.IsDelete).AsAsyncEnumerable())
            {
                model.Genders.Add(new GenderViewModel
                {
                    Id = item.Id,
                    Name = item.GenderName
                });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> SurveyForm(SurveyPostViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id")).Value);
            //user = new ApplicationUser
            //{
            if (user != null)
            {
                user.FilledSurveyForm = true;
                if (!string.IsNullOrEmpty(model.PersonalInfo.ProfilePicture))
                {
                    user.LogoPicture = model.PersonalInfo.ProfilePicture.Replace("ProfilePictures", "Logos");
                    user.MainSiteImage = model.PersonalInfo.ProfilePicture.Replace("ProfilePictures", "MainSiteImage");
                }
                user.ProfilePicture = model.PersonalInfo.ProfilePicture;
                user.CoverPicture = model.PersonalInfo.CoverPicture;
                user.Description = model.PersonalInfo.Description;
                user.Email = model.Contact.Email;
                user.DOB = model.PersonalInfo.DateOfBirth;
                user.AboutYourSelf = model.PersonalInfo.AboutMySelf;
                user.UserName = HttpContext.User.Identity.Name;
                user.FirstName = model.PersonalInfo.FirstName;
                user.MiddleName = model.PersonalInfo.MiddleName;
                user.LastName = model.PersonalInfo.LastName;
                user.GenderId = model.PersonalInfo.GenderId;
                user.Height = model.PersonalInfo.Height;
                user.Weight = model.PersonalInfo.Weight;
                user.MinPrice = model.PersonalInfo.MinPrice;
                user.MaxPrice = model.PersonalInfo.MaxPrice;
                user.WillingToTravel = model.TrainerComfortZone.WillingToTravel;
                user.GroupTrainer = model.PersonalInfo.GroupTrainer;
                user.PrivateTrainer = model.PersonalInfo.PrivateTrainer;
                user.YearsOfExperience = model.PersonalInfo.YearsOfExperience;
                user.YearsAsTraining = model.PersonalInfo.YearsAsTraining;
                user.LastModifiedDate = DateTime.Now;
                user.LastModifiedBy = HttpContext.User.Identity.Name;
                user.TC_Accept = model.TC_Accepted;
                //};
                try
                {
                    var resultr = await _userManager.UpdateAsync(user);

                }
                catch (Exception ex)
                {

                    throw;
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    //foreach (var item in model.Designations)
                    //{
                    //    await _db.Designations.AddAsync(new Designation
                    //    {
                    //        MemberId = memberId,
                    //        Title = item,
                    //        CreatedAt = DateTime.Now,
                    //        CreatedBy = HttpContext.User.Identity.Name
                    //    });
                    //}
                    await _db.Designations.AddAsync(new Designation
                    {
                        ApplicationUserId = user.Id,
                        Title = model.Designation,
                        CreatedAt = DateTime.Now,
                        CreatedBy = HttpContext.User.Identity.Name
                    });

                    foreach (var item in model.Skills)
                    {
                        await _db.MemberSkills.AddAsync(new MemberSkill
                        {
                            ApplicationUserId = user.Id,
                            SkillId = item.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    foreach (var item in model.Certifications)
                    {
                        await _db.MemberCertifications.AddAsync(new MemberCertification
                        {
                            ApplicationUserId = user.Id,
                            CertificationId = item.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    if (TimeSpan.TryParse(model.TimeSlot.FromMonday, out TimeSpan fromMonday) && TimeSpan.TryParse(model.TimeSlot.ToMonday, out TimeSpan toMonday))
                    {
                        await _db.Timings.AddAsync(new Timing
                        {
                            Day = "Monday",
                            From = fromMonday,
                            To = toMonday,
                            ApplicationUserId = user.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    if (TimeSpan.TryParse(model.TimeSlot.FromMonday, out TimeSpan fromTuesday) && TimeSpan.TryParse(model.TimeSlot.ToMonday, out TimeSpan toTuesday))
                    {
                        await _db.Timings.AddAsync(new Timing
                        {
                            Day = "Tuesday",
                            From = fromTuesday,
                            To = toTuesday,
                            ApplicationUserId = user.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    if (TimeSpan.TryParse(model.TimeSlot.FromMonday, out TimeSpan fromWednesday) && TimeSpan.TryParse(model.TimeSlot.ToMonday, out TimeSpan toWednesday))
                    {
                        await _db.Timings.AddAsync(new Timing
                        {
                            Day = "Wednesday",
                            From = fromWednesday,
                            To = toWednesday,
                            ApplicationUserId = user.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    if (TimeSpan.TryParse(model.TimeSlot.FromMonday, out TimeSpan fromThursday) && TimeSpan.TryParse(model.TimeSlot.ToMonday, out TimeSpan toThursday))
                    {
                        await _db.Timings.AddAsync(new Timing
                        {
                            Day = "Thursday",
                            From = fromThursday,
                            To = toThursday,
                            ApplicationUserId = user.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    if (TimeSpan.TryParse(model.TimeSlot.FromMonday, out TimeSpan fromFriday) && TimeSpan.TryParse(model.TimeSlot.ToMonday, out TimeSpan toFriday))
                    {
                        await _db.Timings.AddAsync(new Timing
                        {
                            Day = "Friday",
                            From = fromFriday,
                            To = toFriday,
                            ApplicationUserId = user.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    if (TimeSpan.TryParse(model.TimeSlot.FromMonday, out TimeSpan fromSaturday) && TimeSpan.TryParse(model.TimeSlot.ToMonday, out TimeSpan toSaturday))
                    {
                        await _db.Timings.AddAsync(new Timing
                        {
                            Day = "Saturday",
                            From = fromSaturday,
                            To = toSaturday,
                            ApplicationUserId = user.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    if (TimeSpan.TryParse(model.TimeSlot.FromMonday, out TimeSpan fromSunday) && TimeSpan.TryParse(model.TimeSlot.ToMonday, out TimeSpan toSunday))
                    {
                        await _db.Timings.AddAsync(new Timing
                        {
                            Day = "Sunday",
                            From = fromSunday,
                            To = toSunday,
                            ApplicationUserId = user.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = HttpContext.User.Identity.Name
                        });
                    }

                    await _db.Contacts.AddAsync(new Contact
                    {
                        ApplicationUserId = user.Id,
                        Number = model.Contact.PhoneNumber,
                        Email = model.Contact.Email,
                        LinkedInUrl = model.Contact.LinkedInUrl,
                        FacebookUrl = model.Contact.FaceBookUrl,
                        CreatedAt = DateTime.Now,
                        CreatedBy = HttpContext.User.Identity.Name
                    });

                    await _db.Addresses.AddAsync(new Address
                    {
                        ApplicationUserId = user.Id,
                        GymId = null,
                        CityId = model.Contact.CityId == default ? null : model.Contact.CityId,
                        DistrictId = model.Contact.DistrictId == default ? null : model.Contact.DistrictId,
                        AddressTitle = model.Contact.Address,
                        CreatedAt = DateTime.Now,
                        CreatedBy = HttpContext.User.Identity.Name
                    });

                    if (await _db.SaveChangesAsync() > 0)
                    {
                        return Ok(/*model.PersonalInfo.ProfilePicture*/);
                    }
                }
            }
            return BadRequest();
        }
    }
}
