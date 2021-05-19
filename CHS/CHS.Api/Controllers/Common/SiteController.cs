using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly CHSDbContext _db;

        public SiteController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        //[Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            SiteViewModel model = new()
            {
                GymMemberShips = new List<GymMemberShipViewModel>()
            };

            //var user = await _db.AspNetUsers.Where(x => x.UserName.Equals("gym@g.com")).Include(x => x.UserRoles).ThenInclude(x => x.Role).ToListAsync();
            //return Ok(user);

            model.GymMemberShips = await _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Gym").Include(x => x.User).Where(x => x.User.IsActive)
                                           .Join(_db.Subscriptions,
                                           a => a.User.Id,
                                           s => s.SubscriptionOwnerId,
                                           (a, s) => new GymMemberShipViewModel
                                           {
                                               SubscriptionOwnerId = s.SubscriptionOwnerId,
                                               SubscriptionId = s.Id,
                                               SubscriptionName = s.Name,
                                               AboutYourSelf = a.User.AboutYourSelf,
                                               Price = s.Price,
                                               Picture = s.Picture,
                                               SubscriptionMonths = s.SubscriptionMonths,
                                           }).AsNoTracking().ToListAsync();

            model.TrainerPrograms = await _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Trainer").Include(x => x.User).Where(x => x.User.IsActive)
                                           .Join(_db.Subscriptions,
                                           a => a.User.Id,
                                           s => s.SubscriptionOwnerId,
                                           (a, s) => new TrainerProgramViewModel
                                           {
                                               SubscriptionOwnerId = s.SubscriptionOwnerId,
                                               SubscriptionId = s.Id,
                                               SubscriptionName = s.Name,
                                               TrainingDays = s.TrainingDays,
                                               TrainingTiming = s.TrainingTiming,
                                               Description = s.Description,
                                               Price = s.Price,
                                               Picture = s.Picture,
                                               SubscriptionMonths = s.SubscriptionMonths,
                                           }).AsNoTracking().ToListAsync();

            model.BestGyms = await _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Gym")
                                      .Include(x => x.User).Where(x => x.User.IsActive).Include(x => x.User.Designations)
                                      .Select(x => new BestGymViewModel
                                      {
                                          Id = x.User.Id,
                                          Name = x.User.FirstName,
                                          Designation = x.User.Designations.FirstOrDefault().Title,
                                          Picture = x.User.ProfilePicture
                                      }).Take(5)
                                      .AsNoTracking().ToListAsync();

            model.BestTrainer = await _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Trainer")
                                      .Include(x => x.User).Where(x => x.User.IsActive).Include(x => x.User.Designations)
                                      .Select(x => new BestTrainerViewModel
                                      {
                                          Id = x.User.Id,
                                          FirstName = x.User.FirstName,
                                          LastName = x.User.LastName,
                                          Designation = x.User.Designations.FirstOrDefault().Title,
                                          Picture = x.User.ProfilePicture
                                      }).Take(5)
                                      .AsNoTracking().ToListAsync();

            model.LatestGyms = await _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Gym")
                                      .Include(x => x.User).Where(x => x.User.IsActive && x.User.FilledSurveyForm).Include(x => x.User.Designations).OrderByDescending(x => x.User.LastModifiedDate)
                                      .Select(x => new LatestGymViewModel
                                      {
                                          Id = x.User.Id,
                                          FirstName = x.User.FirstName,
                                          LastName = x.User.LastName,
                                          GymName = x.User.MiddleName,
                                          Designation = x.User.Designations.FirstOrDefault().Title,
                                          AboutYourSelf = x.User.AboutYourSelf,
                                          JoinDate = x.User.LastModifiedDate
                                      }).Take(3)
                                      .AsNoTracking().ToListAsync();

            model.LatestTrainers = await _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Trainer")
                                      .Include(x => x.User).Where(x => x.User.IsActive && x.User.FilledSurveyForm).Include(x => x.User.Designations).OrderByDescending(x => x.User.LastModifiedDate)
                                      .Select(x => new LatestTrainerViewModel
                                      {
                                          Id = x.User.Id,
                                          FirstName = x.User.FirstName,
                                          LastName = x.User.LastName,
                                          Designation = x.User.Designations.FirstOrDefault().Title,
                                          AboutYourSelf = x.User.AboutYourSelf,
                                          JoinDate = x.User.LastModifiedDate
                                      }).Take(3)
                                      .AsNoTracking().ToListAsync();
            //var json = JsonConvert.SerializeObject(obj,
            //    new JsonSerializerSettings
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });
            return Ok(model);
        }
    }
}
