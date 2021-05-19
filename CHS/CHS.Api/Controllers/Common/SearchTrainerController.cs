using System;
using System.Collections.Generic;
using System.Linq;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    public class SearchTrainerController : CommonController
    {
        private readonly CHSDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchTrainerController(CHSDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async IAsyncEnumerable<SearchViewModel> GetAll()
        {
            var query = _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Trainer").Include(x => x.User).Where(x => x.User.FilledSurveyForm && x.User.IsActive).Include(x => x.User.Addresses).ThenInclude(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                    .Join(_db.Designations,
                    a => a.User.Id,
                    d => d.ApplicationUserId,
                    (a, d) => new SearchViewModel
                    {
                        Id = a.User.Id,
                        FirstName = a.User.FirstName,
                        LastName = a.User.LastName,
                        Username = a.User.UserName,
                        Picture = a.User.ProfilePicture,
                        Description = a.User.Description,
                        AboutYourSelf = a.User.AboutYourSelf,
                        MaxPrice = a.User.MaxPrice,
                        MinPrice = a.User.MinPrice,
                        Address = a.User.Addresses.FirstOrDefault().AddressTitle,
                        City = a.User.Addresses.FirstOrDefault().District.City.CityName,
                        District = a.User.Addresses.FirstOrDefault().District.Name,
                        Country = a.User.Addresses.FirstOrDefault().District.City.State.Country.CountryName,
                        Title = d.Title,
                        GroupTrainer = a.User.GroupTrainer,
                        PrivateTrainer = a.User.PrivateTrainer,
                        AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                        CreatedAt = (DateTime)a.User.CreatedAt
                    });

            await foreach (var item in query.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetByName")]
        public async IAsyncEnumerable<SearchViewModel> GetByName(string searchTerm)
        {
            var users = await _userManager.GetUsersInRoleAsync("Trainer");

            searchTerm = searchTerm.ToLower();

            var query = _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Trainer").Include(x => x.User).Where(x => (x.User.FirstName.ToLower().Contains(searchTerm) || x.User.LastName.ToLower().Contains(searchTerm)) && x.User.FilledSurveyForm).Include(x => x.User.Addresses).ThenInclude(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                .Join(_db.Designations,
                a => a.User.Id,
                d => d.ApplicationUserId,
                (a, d) => new SearchViewModel
                {
                    Id = a.User.Id,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Username = a.User.UserName,
                    Picture = a.User.ProfilePicture,
                    Description = a.User.Description,
                    AboutYourSelf = a.User.AboutYourSelf,
                    MaxPrice = a.User.MaxPrice,
                    MinPrice = a.User.MinPrice,
                    Title = a.User.Designations.FirstOrDefault().Title,
                    Address = a.User.Addresses.FirstOrDefault().AddressTitle,
                    City = a.User.Addresses.FirstOrDefault().District.City.CityName,
                    District = a.User.Addresses.FirstOrDefault().District.Name,
                    Country = a.User.Addresses.FirstOrDefault().District.City.State.Country.CountryName,
                    GroupTrainer = a.User.GroupTrainer,
                    PrivateTrainer = a.User.PrivateTrainer,
                    AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                    CreatedAt = (DateTime)a.User.CreatedAt
                });

            await foreach (var item in query.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetByDesignation")]
        public async IAsyncEnumerable<SearchViewModel> GetByDesignation(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            var query = _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Gym").Include(x => x.User).Where(x => x.User.FilledSurveyForm).Include(x => x.User.Addresses).ThenInclude(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                    .Join(_db.Designations.Where(x => x.Title.ToLower().Contains(searchTerm)),
                    a => a.User.Id,
                    d => d.ApplicationUserId,
                    (a, d) => new SearchViewModel
                    {
                        Id = a.User.Id,
                        FirstName = a.User.FirstName,
                        LastName = a.User.LastName,
                        Username = a.User.UserName,
                        Picture = a.User.ProfilePicture,
                        Description = a.User.Description,
                        AboutYourSelf = a.User.AboutYourSelf,
                        MaxPrice = a.User.MaxPrice,
                        MinPrice = a.User.MinPrice,
                        Title = d.Title,
                        Address = a.User.Addresses.FirstOrDefault().AddressTitle,
                        City = a.User.Addresses.FirstOrDefault().District.City.CityName,
                        District = a.User.Addresses.FirstOrDefault().District.Name,
                        Country = a.User.Addresses.FirstOrDefault().District.City.State.Country.CountryName,
                        GroupTrainer = a.User.GroupTrainer,
                        PrivateTrainer = a.User.PrivateTrainer,
                        AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                        CreatedAt = (DateTime)a.User.CreatedAt
                    });

            await foreach (var item in query.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetByGroupTrainer")]
        public async IAsyncEnumerable<SearchViewModel> GetByGroupTrainer(bool searchTerm)
        {
            var result = _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Gym").Include(x => x.User).Where(x => x.User.GroupTrainer.Equals(searchTerm)).Where(x => x.User.PrivateTrainer.Equals(searchTerm)).Include(x => x.User.Designations)
                .Join(_db.Addresses.Include(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country),
                a => a.User.Id,
                ad => ad.ApplicationUserId,
                (a, ad) => new SearchViewModel
                {
                    Id = a.User.Id,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Username = a.User.UserName,
                    Picture = a.User.ProfilePicture,
                    Description = a.User.Description,
                    AboutYourSelf = a.User.AboutYourSelf,
                    MaxPrice = a.User.MaxPrice,
                    MinPrice = a.User.MinPrice,
                    Title = a.User.Designations.FirstOrDefault().Title,
                    Address = ad.AddressTitle,
                    City = ad.District.City.CityName,
                    District = ad.District.Name,
                    Country = ad.District.City.State.Country.CountryName,
                    GroupTrainer = a.User.GroupTrainer,
                    PrivateTrainer = a.User.PrivateTrainer,
                    AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                    CreatedAt = (DateTime)a.User.CreatedAt
                });

            await foreach (var item in result.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetByPrivateTrainer")]
        public async IAsyncEnumerable<SearchViewModel> GetByPrivateTrainer(bool searchTerm)
        {
            var result = _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Gym").Include(x => x.User).Where(x => x.User.PrivateTrainer.Equals(searchTerm)).Where(x => x.User.PrivateTrainer.Equals(searchTerm)).Include(x => x.User.Designations)
                .Join(_db.Addresses.Include(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country),
                a => a.User.Id,
                ad => ad.ApplicationUserId,
                (a, ad) => new SearchViewModel
                {
                    Id = a.User.Id,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Username = a.User.UserName,
                    Picture = a.User.ProfilePicture,
                    Description = a.User.Description,
                    AboutYourSelf = a.User.AboutYourSelf,
                    MaxPrice = a.User.MaxPrice,
                    MinPrice = a.User.MinPrice,
                    Title = a.User.Designations.FirstOrDefault().Title,
                    Address = ad.AddressTitle,
                    City = ad.District.City.CityName,
                    District = ad.District.Name,
                    Country = ad.District.City.State.Country.CountryName,
                    GroupTrainer = a.User.GroupTrainer,
                    PrivateTrainer = a.User.PrivateTrainer,
                    AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                    CreatedAt = (DateTime)a.User.CreatedAt
                });

            await foreach (var item in result.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetByAvailableToHire")]
        public async IAsyncEnumerable<SearchViewModel> GetByAvailableToHire(bool searchTerm)
        {
            var result = _db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Gym").Include(x => x.User).Where(x => x.User.AvaiableToHireOrSpaceAvailable.Equals(searchTerm)).Include(x => x.User.Designations)
                .Join(_db.Addresses.Include(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country),
                a => a.UserId,
                ad => ad.ApplicationUserId,
                (a, ad) => new SearchViewModel
                {
                    Id = a.User.Id,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Username = a.User.UserName,
                    Picture = a.User.ProfilePicture,
                    Description = a.User.Description,
                    AboutYourSelf = a.User.AboutYourSelf,
                    MaxPrice = a.User.MaxPrice,
                    MinPrice = a.User.MinPrice,
                    Title = a.User.Designations.FirstOrDefault().Title,
                    Address = ad.AddressTitle,
                    City = ad.District.City.CityName,
                    District = ad.District.Name,
                    Country = ad.District.City.State.Country.CountryName,
                    GroupTrainer = a.User.GroupTrainer,
                    PrivateTrainer = a.User.PrivateTrainer,
                    AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                    CreatedAt = (DateTime)a.User.CreatedAt
                });

            await foreach (var item in result.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetByCity")]
        public async IAsyncEnumerable<SearchViewModel> GetByCity(string searchTerm)
        {
            var result = _db.Cities.Where(x => x.CityName.ToLower().Contains(searchTerm.ToLower())).Include(x => x.State).ThenInclude(x => x.Country).Join(_db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Trainer").Include(x => x.User).ThenInclude(x => x.Addresses).ThenInclude(x => x.District).Include(x => x.User.Designations),
                ad => ad.Id,
                a => a.User.Addresses.FirstOrDefault().District.CityId,
                (ad, a) => new SearchViewModel
                {
                    Id = a.User.Id,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Username = a.User.UserName,
                    Picture = a.User.ProfilePicture,
                    Description = a.User.Description,
                    AboutYourSelf = a.User.AboutYourSelf,
                    MaxPrice = a.User.MaxPrice,
                    MinPrice = a.User.MinPrice,
                    Title = a.User.Designations.FirstOrDefault().Title,
                    Address = a.User.Addresses.FirstOrDefault().AddressTitle,
                    City = ad.Districts.FirstOrDefault().City.CityName,
                    District = ad.Districts.FirstOrDefault().Name,
                    Country = ad.Districts.FirstOrDefault().City.State.Country.CountryName,
                    GroupTrainer = a.User.GroupTrainer,
                    PrivateTrainer = a.User.PrivateTrainer,
                    AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                    CreatedAt = (DateTime)ad.CreatedAt
                });

            await foreach (var item in result.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetByDistrict")]
        public async IAsyncEnumerable<SearchViewModel> GetByDistrict(string searchTerm)
        {
            var result = _db.Districts.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).Include(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                .Join(_db.ApplicationUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Trainer").Include(x => x.User).ThenInclude(x => x.Addresses).Include(x => x.User.Designations),
                d => d.Id,
                a => a.User.Addresses.FirstOrDefault().DistrictId,
                (d, a) => new SearchViewModel
                {
                    Id = a.User.Id,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Username = a.User.UserName,
                    Picture = a.User.ProfilePicture,
                    Description = a.User.Description,
                    AboutYourSelf = a.User.AboutYourSelf,
                    MaxPrice = a.User.MaxPrice,
                    MinPrice = a.User.MinPrice,
                    Title = a.User.Designations.FirstOrDefault().Title,
                    Address = a.User.Addresses.FirstOrDefault().AddressTitle,
                    City = d.City.CityName,
                    District = d.Name,
                    Country = d.City.State.Country.CountryName,
                    GroupTrainer = a.User.GroupTrainer,
                    PrivateTrainer = a.User.PrivateTrainer,
                    AvaiableToHireOrSpaceAvailable = a.User.AvaiableToHireOrSpaceAvailable,
                    CreatedAt = (DateTime)d.CreatedAt
                });

            await foreach (var item in result.AsNoTracking().AsAsyncEnumerable())
            {
                if (string.IsNullOrEmpty(item.Picture))
                {
                    item.Picture = "Images/profile.png";
                    yield return item;
                }
                else
                {
                    var arr = item.Picture.Split(Constant.WwwRoot);
                    if (arr.Length == 2)
                    {
                        item.Picture = arr[1].Replace('\\', '/');
                        yield return item;
                    }
                }
            }
        }
    }
}
