using System.Collections.Generic;

using CHS.Domains.Context;
using CHS.Domains.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers.Trainer
{
    public class HomeController : TrainerController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CHSDbContext _db;

        public HomeController(UserManager<ApplicationUser> userManager, CHSDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet]
        public IAsyncEnumerable<Contact> Get()
        {
            return _db.Contacts.AsAsyncEnumerable();
        }
    }
}
