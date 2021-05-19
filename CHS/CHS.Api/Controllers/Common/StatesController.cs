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
    public class StatesController : CommonController
    {
        private readonly CHSDbContext _db;

        public StatesController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("CountryAndState")]
        public async Task<CountryAndState> CountryAndState()
        {
            CountryAndState model = new CountryAndState
            {
                States = new List<StateViewModel>(),
                Countries = new List<CountryViewModel>()
            };

            foreach (var item in await _db.States.Where(x => !x.IsDelete && x.IsActive).Include(x => x.Country).ToListAsync())
            {
                model.States.Add(new StateViewModel
                {
                    StateId = item.Id,
                    StateName = item.StateName,
                    CountryId = item.CountryId,
                    CountryName = item.Country.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                });
            }

            foreach (var item in await _db.Countries.Where(x => !x.IsDelete && x.IsActive).ToListAsync())
            {
                model.Countries.Add(new CountryViewModel
                {
                    Id = item.Id,
                    Name = item.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                });
            }
            return model;
        }

        [HttpGet]
        [Route("GetStatesByCountry")]
        public async IAsyncEnumerable<StateViewModel> GetStatesByCountry(Guid countryId)
        {
            if (countryId != default)
            {
                await foreach (var item in _db.States.Where(x => !x.IsDelete && x.IsActive).Where(x => x.CountryId == countryId).OrderBy(x => x.StateName).AsAsyncEnumerable())
                {
                    yield return new StateViewModel
                    {
                        StateId = item.Id,
                        StateName = item.StateName
                    };
                }
            }
            else
            {
                yield return null;
            }
        }

        [HttpGet]
        public async IAsyncEnumerable<StateViewModel> Get()
        {
            await foreach (var item in _db.States.Where(x => !x.IsDelete).OrderBy(x => x.StateName).Include(x => x.Country)
                                                                                                   .AsAsyncEnumerable())
            {
                yield return new StateViewModel
                {
                    StateId = item.Id,
                    StateName = item.StateName,
                    CountryId = item.CountryId,
                    CountryName = item.Country.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                };
            }
        }

        [HttpPost]
        public async Task<HttpStatusCode> AddState(StateViewModel model)
        {
            if (model is null)
            {
                return HttpStatusCode.BadRequest;
            }
            if (!await _db.States.AnyAsync(x => x.StateName.Equals(model.StateName)))
            {
                await _db.States.AddAsync(new State
                {
                    Id = model.StateId,
                    StateName = model.StateName,
                    CountryId = model.CountryId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = HttpContext.User.Identity.Name,
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
            var result = await _db.States.FirstOrDefaultAsync(x => x.Id == id);
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
            var result = await _db.States.FirstOrDefaultAsync(x => x.Id == id);
            result.IsDelete = false;
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
