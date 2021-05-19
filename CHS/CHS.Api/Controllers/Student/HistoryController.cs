using System;
using System.Collections.Generic;

using CHS.Domains.Context;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers.Student
{
    public class HistoryController : StudentController
    {
        private readonly CHSDbContext _db;

        public HistoryController(CHSDbContext db)
        {
            _db = db;
        }

        [Route("GetHistory")]
        public IEnumerable<StudentHistoryViewModel> GetHistory()
        {
            return new List<StudentHistoryViewModel>
            {
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-4),
                    EndDate=DateTime.Now.AddMonths(4),
                    Price= 445.5M,
                    SubscriptionName="Gymnastic",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-1),
                    EndDate=DateTime.Now.AddMonths(4),
                    Price= 445.5M,
                    SubscriptionName="Karate",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-5),
                    EndDate=DateTime.Now.AddMonths(-1),
                    Price= 445.5M,
                    SubscriptionName="Judo",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(2),
                    EndDate=DateTime.Now.AddMonths(-1),
                    Price= 445.5M,
                    SubscriptionName="Fitness",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-4),
                    EndDate=DateTime.Now,
                    Price= 445.5M,
                    SubscriptionName="Boxing",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-2),
                    EndDate=DateTime.Now,
                    Price= 445.5M,
                    SubscriptionName="Cycling",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-6),
                    EndDate=DateTime.Now,
                    Price= 445.5M,
                    SubscriptionName="Running",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                }
            };
        }
    }
}
