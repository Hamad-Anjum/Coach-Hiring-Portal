using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using static System.IO.File;

namespace CHS.Api.Controllers.Common
{
    public class BasicInfoController : CommonController
    {
        private readonly CHSDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BasicInfoController(CHSDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet("{memberId:guid?}")]
        public async Task<BasicInfoViewModel> GetProfile(Guid? memberId)
        {
            if (memberId == null)
            {
                memberId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "memberid").Value);
            }
            //var user = await _userManager.FindByIdAsync(memberId.ToString());
            //if (user != null)
            //{

            var result = await _db.AspNetUsers.Where(x => x.Id.Equals(memberId) && x.IsActive && !x.IsDelete).Include(x => x.Contacts).Include(x => x.Gender)
                .Join(_db.Addresses.Where(x => x.IsActive && !x.IsDelete).Include(x => x.District).Include(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country),
                      m => m.Id,
                      a => a.ApplicationUserId,
                      (m, a) => new BasicInfoViewModel
                      {
                          MemberId = m.Id,
                          Username = m.UserName,
                          FirstName = m.FirstName,
                          LastName = m.LastName,
                          ProfilePicture = m.ProfilePicture,
                          CoverPicture = m.CoverPicture,
                          //CoverPicture = string.IsNullOrEmpty(m.CoverPicture) ? "Images/cover.jpg" : m.CoverPicture,
                          ContactId = m.Contacts.FirstOrDefault().Id,    //in new table
                          ContactNumber = m.Contacts.FirstOrDefault().Number,    //in new table
                          Email = m.Email,
                          WillingToTravel = m.WillingToTravel,
                          Weight = m.Weight,
                          Height = m.Height,
                          GenderId = (Guid)m.GenderId,
                          GenderName = m.Gender.GenderName,
                          GroupTrainer = m.GroupTrainer,
                          PrivateTrainer = m.PrivateTrainer,
                          AddressId = a.Id,
                          AddressIsActive = a.IsActive,
                          AddressIsDelete = a.IsDelete,
                          AddressCreatedAt = a.CreatedAt,
                          AddressCreatedBy = a.CreatedBy,
                          AddressIndex = a.Index,
                          Address = a.AddressTitle,
                          DOB = m.DOB,
                          CityId = (Guid)a.CityId,
                          CityName = a.City.CityName,
                          DistrictId = (Guid)a.DistrictId,
                          DistrictName = a.District.Name,
                          StateId = a.City.StateId,
                          StateName = a.City.State.StateName,
                          CountryId = a.City.State.CountryId,
                          CountryName = a.City.State.Country.CountryName,
                          AvailableOrHire = m.AvaiableToHireOrSpaceAvailable,
                          TellAboutYou = m.AboutYourSelf
                      }).FirstOrDefaultAsync();
            result.CoverPicture = !string.IsNullOrEmpty(result.CoverPicture) ? result.CoverPicture.Split(Constant.WwwRoot)[1].Replace('\\', '/') : "Images/cover.jpg";
            //result.CoverPicture = Exists(result.CoverPicture) ? result.CoverPicture.Split(Constant.WwwRoot)[1] : "Images/cover.jpg";
            result.ProfilePicture = !string.IsNullOrEmpty(result.ProfilePicture) ? result.ProfilePicture.Split(Constant.WwwRoot)[1].Replace('\\', '/') : "Images/profile.png";
            //result.ProfilePicture = Exists(result.ProfilePicture) ? result.ProfilePicture.Split(Constant.WwwRoot)[1] : "Images/profile.png";

            result.Followers = await _db.Followers.Where(x => x.UserId.Equals(memberId)).CountAsync();
            result.Followings = await _db.Followings.Where(x => x.FolloweeId.Equals(memberId)).CountAsync();
            return result;
            //}
            //return null;
        }

        [HttpPut("{id:guid}")]
        public async Task<HttpStatusCode> PutProfile(Guid id, BasicInfoViewModel model)
        {
            if (model is null || id == default)
            {
                return HttpStatusCode.BadRequest;
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            user.Description = model.Description;
            user.Email = model.Email;
            user.DOB = model.DOB;
            user.AboutYourSelf = model.TellAboutYou;
            user.UserName = HttpContext.User.Identity.Name;
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.GenderId = model.GenderId;
            user.Height = model.Height;
            user.Weight = model.Weight;
            user.MinPrice = model.MinPrice;
            user.MaxPrice = model.MaxPrice;
            user.WillingToTravel = model.WillingToTravel;
            user.GroupTrainer = model.GroupTrainer;
            user.PrivateTrainer = model.PrivateTrainer;
            user.LastModifiedDate = DateTime.Now;
            user.LastModifiedBy = HttpContext.User.Identity.Name;
            user.TC_Accept = model.TC_Accepted;
            //};

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                //    Member member = new Member
                //{
                //    Id = id,
                //    Username = model.Username,
                //    FirstName = model.FirstName,
                //    LastName = model.LastName,
                //    DOB = model.DOB,
                //    GenderId = model.GenderId,
                //    Email = model.Email,
                //    Weight = model.Weight,
                //    Height = model.Height,
                //    GroupTrainer = model.GroupTrainer,
                //    PrivateTrainer = model.PrivateTrainer,
                //    WillingToTravel = model.WillingToTravel,
                //    AboutYourSelf = model.TellAboutYou,
                //    AvaiableToHireOrSpaceAvailable = model.AvailableOrHire,
                //    LastModifiedDate = DateTime.Now,
                //    LastModifiedBy = HttpContext.User.Identity.Name
                //};

                //_db.Entry(member).State = EntityState.Modified;

                Contact contact = new()
                {
                    Id = model.ContactId,
                    ApplicationUserId = id,
                    Number = model.ContactNumber,
                    Email = model.Email,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedBy = HttpContext.User.Identity.Name
                };

                _db.Entry(contact).State = EntityState.Modified;

                Address address = new()
                {
                    Id = model.AddressId,
                    ApplicationUserId = id,
                    AddressTitle = model.Address,
                    CityId = model.CityId,
                    DistrictId = model.DistrictId,
                    IsActive = model.AddressIsActive,
                    IsDelete = model.AddressIsDelete,
                    Index = model.AddressIndex,
                    CreatedAt = model.AddressCreatedAt,
                    CreatedBy = model.AddressCreatedBy,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedBy = HttpContext.User.Identity.Name
                };

                _db.Entry(address).State = EntityState.Modified;
                _db.Addresses.Update(address);

                if (await _db.SaveChangesAsync() > 0)
                {
                    return HttpStatusCode.OK;
                }
            }
            return HttpStatusCode.BadRequest;
        }
    }
}

/*
 The UPDATE statement conflicted with the FOREIGN KEY constraint "FK_Addresses_Members_MemberId". The conflict occurred in database "CHSDB", table "dbo.Members", column 'Id'.
The statement has been terminated.
*/