using System.Collections.Generic;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Domains.Models;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers.Student
{
    [Authorize(Roles = "Student,Gym,Trainer")]
    public class SearchController : StudentController
    {
        private readonly CHSDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(CHSDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAllTrainers")]
        public async Task<IEnumerable<SearchViewModel>> GetAllTrainers(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            //foreach (var item in collection)
            //{

            //}
            //var result = await _db.Members.Where(x => x.Id == users.);
            return null;
        }


        [HttpGet]
        [Route("SearchByTrainer")]
        public async Task<List<SearchViewModel>> SearchByTrainer()
        {
            //var result = await _db.Members.Join().FirstOrDefaultAsync();

            return new List<SearchViewModel>
            {
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                }
            };
        }

        [HttpGet]
        [Route("SearchByGym")]
        public async Task<List<SearchViewModel>> SearchByGym()
        {
            return new List<SearchViewModel>
            {
                new SearchViewModel
                {
                    Title="V-Shape",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    //FirstName="Hamad",
                    //LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Gym of Pakistan",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    //FirstName="Hamad",
                    //LastName="Anjum",
                    City="Lahore",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Era",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    //FirstName="Hamad",
                    //LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=29000,
                    MinPrice=20000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                }
            };
        }

        [HttpGet]
        [Route("SearcTrainerhByCity")]
        public async Task<List<SearchViewModel>> SearcTrainerhByCity(string city)
        {
            return new List<SearchViewModel>
            {
                new SearchViewModel
                {
                    Title="V-Shape",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    //FirstName="Hamad",
                    //LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Address="1km Daska Road",
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Gym of Pakistan",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    //FirstName="Hamad",
                    //LastName="Anjum",
                    City="Lahore",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Address="1km Daska Road",
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Era",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    //FirstName="Hamad",
                    //LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=29000,
                    MinPrice=20000,
                    Address="1km Daska Road",
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                },
                new SearchViewModel
                {
                    Title="Fitness Trainer",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...",
                    FirstName="Hamad",
                    LastName="Anjum",
                    City="Sialkot",
                    Country="Pakistan",
                    MaxPrice=20000,
                    MinPrice=14000,
                    Picture="Jobie/images/logo.png"
                }
            };
        }
    }
}
