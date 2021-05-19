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
    public class CitiesController : CommonController
    {
        private readonly CHSDbContext _db;

        public CitiesController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("CountryStateCity")]
        public async Task<CountryStateCity> CountryStateCity()
        {
            CountryStateCity model = new CountryStateCity
            {
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
            //        Status = item.IsActive ? "In Active" : "Active"
            //    });
            //}

            model.StatesCount = await _db.States.CountAsync();

            foreach (var item in await _db.Cities.Where(x => !x.IsDelete).Include(x => x.State).ThenInclude(x => x.Country).ToListAsync())
            {
                model.Cities.Add(new CityViewModel
                {
                    Id = item.Id,
                    CityName = item.CityName,
                    StateId = item.StateId,
                    StateName = item.State.StateName,
                    CountryId = item.State.Country.Id,
                    CountryName = item.State.Country.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                });
            }
            return model;
        }

        [HttpGet]
        [Route("GetCitiesByState")]
        public async IAsyncEnumerable<CityViewModel> GetCitiesByState(Guid stateId)
        {
            if (stateId == default)
            {
                yield return null;
            }
            await foreach (var item in _db.Cities.Where(x => !x.IsDelete && x.IsActive).Where(x => x.StateId == stateId).AsAsyncEnumerable())
            {
                yield return new CityViewModel
                {
                    Id = item.Id,
                    CityName = item.CityName
                };
            }
        }

        [HttpGet]
        public async IAsyncEnumerable<CityViewModel> Get()
        {
            await foreach (var item in _db.Cities.Where(x => !x.IsDelete).Include(x => x.State).ThenInclude(x => x.Country).AsAsyncEnumerable())
            {
                yield return new CityViewModel
                {
                    Id = item.Id,
                    CityName = item.CityName,
                    StateId = item.StateId,
                    StateName = item.State.StateName,
                    CountryId = item.State.Country.Id,
                    CountryName = item.State.Country.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                };
            }
        }

        [HttpPost]
        public async Task<HttpStatusCode> AddCity(CityViewModel model)
        {
            if (model is null)
            {
                return HttpStatusCode.BadRequest;
            }

            if (!await _db.Cities.AnyAsync(x => x.StateId == model.StateId && x.CityName == model.CityName))
            {
                await _db.Cities.AddAsync(new City
                {
                    Id = model.Id,
                    CityName = model.CityName,
                    StateId = model.StateId,
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
            var result = await _db.Cities.FirstOrDefaultAsync(x => x.Id == id);
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
            var result = await _db.Cities.FirstOrDefaultAsync(x => x.Id == id);
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
