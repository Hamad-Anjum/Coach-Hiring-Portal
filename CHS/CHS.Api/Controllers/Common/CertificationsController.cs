using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    public class CertificationsController : CommonController
    {
        private readonly CHSDbContext _db;

        public CertificationsController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async IAsyncEnumerable<CertificationViewModel> GetCertifications()
        {
            await foreach (var item in _db.Certifications.AsAsyncEnumerable())
            {
                yield return new CertificationViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                };
            }
        }

        [HttpPost]
        [Route("AddCertification")]
        public async Task<HttpStatusCode> AddCertification(string certification)
        {
            if (string.IsNullOrEmpty(certification))
            {
                return HttpStatusCode.BadRequest;
            }

            var result = await _db.Certifications.AnyAsync(x => x.Name.ToLower().Equals(certification.ToLower().Trim()));
            if (!result)
            {
                await _db.Certifications.AddAsync(new Certification
                {
                    Name = certification,
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
