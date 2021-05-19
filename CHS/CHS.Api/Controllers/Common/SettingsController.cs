using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers.Common
{
    public class SettingsController:CommonController
    {
        private readonly CHSDbContext _db;

        public SettingsController(CHSDbContext db)
        {
            _db = db;
        }
    }
}
