using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class CountriesController : CommonController
    {
        private readonly CHSDbContext _db;

        public CountriesController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async IAsyncEnumerable<CountryViewModel> Get()
        {
            //ICollection<CountryViewModel> list = new List<CountryViewModel>();
            foreach (var item in await _db.Countries.Where(x => !x.IsDelete).ToListAsync())
            {
                yield return new CountryViewModel
                {
                    Id = item.Id,
                    Name = item.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                };
                //list.Add(new CountryViewModel { Id = item.Id, Name = item.CountryName });
            }
            //return list;
        }

        [HttpGet]
        [Route("GetActive")]
        public async IAsyncEnumerable<CountryViewModel> GetActive()
        {
            //ICollection<CountryViewModel> list = new List<CountryViewModel>();
            await foreach (var item in _db.Countries.Where(x => !x.IsDelete && x.IsActive).AsAsyncEnumerable())
            {
                yield return new CountryViewModel
                {
                    Id = item.Id,
                    Name = item.CountryName,
                    Status = item.IsActive ? "Active" : "In Active"
                };
                //list.Add(new CountryViewModel { Id = item.Id, Name = item.CountryName });
            }
            //return list;
        }

        [HttpPost]
        public async Task<HttpStatusCode> AddCountry(CountryViewModel model)
        {
            if (model is null)
            {
                return HttpStatusCode.BadRequest;
            }
            if (await _db.Countries.AnyAsync(x => x.CountryName.Equals(model.Name)))
            {
                return HttpStatusCode.BadRequest;
            }
            await _db.Countries.AddAsync(new Country
            {
                Id = model.Id,
                CountryName = model.Name,
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });

            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous, Route("AddCountry")]
        public async Task<HttpStatusCode> AddCountry(string temp)
        {
            var list = await _db.Countries.ToListAsync();
            CultureInfo[] infos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (var item in infos)
            {
                RegionInfo region = new RegionInfo(item.LCID);

                var t = item.EnglishName.Split('(')[1].Trim(')');
                //t = t.Split(',')[1].Trim(',').Trim(' ');
                if (!list.Any(x => x.CountryName.Equals(item.EnglishName)) && !item.EnglishName.Contains("World"))
                {
                    await _db.Countries.AddAsync(new Country
                    {
                        CountryCode = item.Name,
                        CountryName = t,
                        CreatedAt = DateTime.Now,
                        CreatedBy = HttpContext.User.Identity.Name
                    });
                    list.Add(new Country { CountryName = item.EnglishName });
                }
            }

            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
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

            var result = await _db.Countries.FirstOrDefaultAsync(x => x.Id == id);
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

            var result = await _db.Countries.FirstOrDefaultAsync(x => x.Id == id);
            result.IsActive = false;
            result.IsDelete = true;
            _db.Entry(result).State = EntityState.Modified;
            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }

        [HttpDelete]
        [Route("DeleteForever")]
        public async Task<HttpStatusCode> DeleteForever(Guid id)
        {
            if (id == default)
            {
                return HttpStatusCode.BadRequest;
            }

            var result = await _db.Countries.FirstOrDefaultAsync(x => x.Id == id);
            _db.Entry(result).State = EntityState.Modified;
            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }
    }
}
