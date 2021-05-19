using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    public class DistrictsController : CommonController
    {
        private readonly CHSDbContext _db;

        public DistrictsController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("CountryStateCityDistrict")]
        public async Task<CountryStateCityDistrict> CountryStateCityDistrict()
        {
            CountryStateCityDistrict model = new CountryStateCityDistrict
            {
                Districts = new List<DistrictViewModel>(),
                Cities = new List<CityViewModel>(),
                States = new List<StateViewModel>(),
                Countries = new List<CountryViewModel>()
            };

            foreach (var item in await _db.Countries.Where(x => !x.IsDelete && x.IsActive).ToListAsync())
            {
                model.Countries.Add(new CountryViewModel
                {
                    Id = item.Id,
                    Name = item.CountryName
                });
            }

            //foreach (var item in await _db.States.Where(x => !x.IsDelete).Include(x => x.Country).ToListAsync())
            //{
            //    model.States.Add(new StateViewModel
            //    {
            //        StateId = item.Id,
            //        StateName = item.StateName,
            //        CountryId = item.CountryId,
            //        CountryName = item.Country.CountryName,
            //        Status = item.IsDelete ? "In Active" : "Active"
            //    });
            //}

            model.StatesCount = await _db.States.CountAsync();
            model.CitiesCount = await _db.Cities.CountAsync();

            //foreach (var item in await _db.Cities.Where(x => !x.IsDelete).Include(x => x.State).ThenInclude(x => x.Country).ToListAsync())
            //{
            //    model.Cities.Add(new CityViewModel
            //    {
            //        Id = item.Id,
            //        CityName = item.CityName,
            //        StateId = item.StateId,
            //        StateName = item.State.StateName,
            //        CountryId = item.State.Country.Id,
            //        CountryName = item.State.Country.CountryName,
            //        Status = item.IsDelete ? "In Active" : "Active"
            //    });
            //}

            await foreach (var item in _db.Districts.Where(x => !x.IsDelete).Include(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country).AsAsyncEnumerable())
            {
                model.Districts.Add(new DistrictViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    CityId = item.CityId,
                    CityName = item.City.CityName,
                    StateId = item.City.StateId,
                    StateName = item.City.State.StateName,
                    CountryId = item.City.State.CountryId,
                    CountryName = item.City.State.Country.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                });
            }
            return model;
        }

        [HttpGet]
        [Route("GetDistrictsByCity")]
        public async IAsyncEnumerable<DistrictViewModel> GetDistrictsByCity(Guid cityId)
        {
            if (cityId == default)
            {
                yield return null;
            }
            await foreach (var item in _db.Districts.Where(x => !x.IsDelete).Where(x => x.CityId == cityId).AsAsyncEnumerable())
            {
                yield return new DistrictViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                };
            }
        }

        [HttpGet]
        public async IAsyncEnumerable<DistrictViewModel> Get()
        {
            await foreach (var item in _db.Districts.Where(x => !x.IsDelete).Include(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country).AsAsyncEnumerable())
            {
                yield return new DistrictViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    CityId = item.CityId,
                    CityName = item.City.CityName,
                    StateId = item.City.StateId,
                    StateName = item.City.State.StateName,
                    CountryId = item.City.State.CountryId,
                    CountryName = item.City.State.Country.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                };
            }
        }

        [HttpPost]
        public async Task<HttpStatusCode> AddDistrict(DistrictViewModel model)
        {
            if (model is null)
            {
                return HttpStatusCode.BadRequest;
            }
            if (!await _db.Districts.AnyAsync(x => x.CityId == model.CityId && x.Name == model.Name))
            {
                await _db.Districts.AddAsync(new District
                {
                    Id = model.Id,
                    Name = model.Name,
                    CityId = model.CityId,
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

        [HttpDelete]
        [Route("ActiveToggle")]
        public async Task<HttpStatusCode> ActiveToggle(Guid id)
        {
            if (id == default)
            {
                return HttpStatusCode.BadRequest;
            }
            var result = await _db.Districts.FirstOrDefaultAsync(x => x.Id == id);
            result.IsActive = !result.IsActive;
            _db.Entry(result).State = EntityState.Modified;
            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }

        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> Delete(Guid id)
        {
            if (id == default)
            {
                return HttpStatusCode.BadRequest;
            }
            var result = await _db.Districts.FirstOrDefaultAsync(x => x.Id == id);
            result.IsActive = false;
            result.IsDelete = true;
            _db.Entry(result).State = EntityState.Modified;
            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }
    }
}
