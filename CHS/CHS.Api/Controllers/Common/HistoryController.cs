using System;
using System.Collections.Generic;

using CHS.Domains.Context;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers.Common
{
    public class HistoryController : CommonController
    {
        private readonly CHSDbContext _db;

        public HistoryController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetStudentTransactionHistory")]
        public IEnumerable<StudentHistoryViewModel> GetStudentTransactionHistory(string name)
        {
            if (name is null)
            {
                return null;
            }
            return new List<StudentHistoryViewModel>
            {
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-4),
                    EndDate=DateTime.Now.AddMonths(4),
                    Name="Fitness",
                    ForGymOrTrainer="Trainer",
                    Price= 445.5M,
                    SubscriptionName="Gymnastic",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-1),
                    EndDate=DateTime.Now.AddMonths(4),
                    Name="Gymnastic",
                    ForGymOrTrainer="Gym",
                    Price= 445.5M,
                    SubscriptionName="Karate",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-5),
                    EndDate=DateTime.Now.AddMonths(-1),
                    Name="Fitness",
                    ForGymOrTrainer="Trainer",
                    Price= 445.5M,
                    SubscriptionName="Judo",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(2),
                    EndDate=DateTime.Now.AddMonths(-1),
                    Name="Fitness",
                    ForGymOrTrainer="Trainer",
                    Price= 445.5M,
                    SubscriptionName="Fitness",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-4),
                    EndDate=DateTime.Now,
                    Name="Fitness",
                    ForGymOrTrainer="Trainer",
                    Price= 445.5M,
                    SubscriptionName="Boxing",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-2),
                    EndDate=DateTime.Now,
                    Name="Fitness",
                    ForGymOrTrainer="Trainer",
                    Price= 445.5M,
                    SubscriptionName="Cycling",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new StudentHistoryViewModel
                {
                    StartDate=DateTime.Now.AddMonths(-6),
                    EndDate=DateTime.Now,
                    Name="Fitness",
                    ForGymOrTrainer="Trainer",
                    Price= 445.5M,
                    SubscriptionName="Running",
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                }
            };

        }

        [HttpGet]
        [Route("GetGymTransactionHistory")]
        public IEnumerable<GymHistoryViewModel> GetGymTransactionHistory(string name)
        {
            if (name is null)
            {
                return null;
            }
            return new List<GymHistoryViewModel>
            {
                new GymHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    InstallmentNumber=1,
                    TransactionDate=DateTime.Now.AddDays(-50),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=10.32M,
                    SubscriptionName="Gymnastic Training",
                    InstallmentNumber=1,
                    TransactionDate=DateTime.Now.AddDays(-12),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=220.32M,
                    SubscriptionName="Karate Training",
                    InstallmentNumber=1,
                    TransactionDate=DateTime.Now.AddDays(-20),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=200.32M,
                    SubscriptionName="Judo Training",
                    InstallmentNumber=1,
                    TransactionDate=DateTime.Now,
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=350.32M,
                    SubscriptionName="Fitness Training",
                    InstallmentNumber=1,
                    TransactionDate=DateTime.Now.AddDays(-10),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Cycling Training",
                    InstallmentNumber=1,
                    TransactionDate=DateTime.Now.AddDays(-30),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-30),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-14),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=10.32M,
                    SubscriptionName="Gymnastic Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-24),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=220.32M,
                    SubscriptionName="Karate Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-34),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=200.32M,
                    SubscriptionName="Judo Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-27),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=350.32M,
                    SubscriptionName="Fitness Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-37),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Cycling Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-39),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=2,
                    TransactionDate=DateTime.Now.AddDays(-43),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-46),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-47),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=10.32M,
                    SubscriptionName="Gymnastic Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-40),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=220.32M,
                    SubscriptionName="Karate Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-17),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=200.32M,
                    SubscriptionName="Judo Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-4),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=350.32M,
                    SubscriptionName="Fitness Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-31),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Cycling Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-9),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new GymHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=3,
                    TransactionDate=DateTime.Now.AddDays(-48),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                }
            };


        }

        [HttpGet]
        [Route("GetTrainerTransactionHistory")]
        public IEnumerable<TrainerHistoryViewModel> GetTrainerTransactionHistory(string name)
        {
            if (name is null)
            {
                return null;
            }

            return new List<TrainerHistoryViewModel>
            {
                new TrainerHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    InstallmentNumber=1,
                    StartDate=DateTime.Now.AddDays(-50),
                    EndDate=DateTime.Now.AddDays(50),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=10.32M,
                    SubscriptionName="Gymnastic Training",
                    InstallmentNumber=1,
                    StartDate=DateTime.Now.AddDays(-10),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=220.32M,
                    SubscriptionName="Karate Training",
                    InstallmentNumber=1,
                    StartDate=DateTime.Now.AddDays(10),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=200.32M,
                    SubscriptionName="Judo Training",
                    InstallmentNumber=1,
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now.AddDays(60),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=350.32M,
                    SubscriptionName="Fitness Training",
                    InstallmentNumber=1,
                    StartDate=DateTime.Now.AddDays(10),
                    EndDate=DateTime.Now.AddDays(75),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Cycling Training",
                    InstallmentNumber=1,
                    StartDate=DateTime.Now.AddDays(-30),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now.AddDays(-30),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now.AddDays(-50),
                    EndDate=DateTime.Now.AddDays(50),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=10.32M,
                    SubscriptionName="Gymnastic Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now.AddDays(-10),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=220.32M,
                    SubscriptionName="Karate Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now.AddDays(10),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=200.32M,
                    SubscriptionName="Judo Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now.AddDays(60),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=350.32M,
                    SubscriptionName="Fitness Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now.AddDays(10),
                    EndDate=DateTime.Now.AddDays(75),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Cycling Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now.AddDays(-30),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=2,
                    StartDate=DateTime.Now.AddDays(-30),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now.AddDays(-30),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now.AddDays(-50),
                    EndDate=DateTime.Now.AddDays(50),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=10.32M,
                    SubscriptionName="Gymnastic Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now.AddDays(-10),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=220.32M,
                    SubscriptionName="Karate Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now.AddDays(10),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=200.32M,
                    SubscriptionName="Judo Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now.AddDays(60),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=350.32M,
                    SubscriptionName="Fitness Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now.AddDays(10),
                    EndDate=DateTime.Now.AddDays(75),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=212.32M,
                    SubscriptionName="Cycling Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now.AddDays(-30),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                },
                new TrainerHistoryViewModel
                {
                    Price=250.32M,
                    SubscriptionName="Running Training",
                    InstallmentNumber=3,
                    StartDate=DateTime.Now.AddDays(-30),
                    EndDate=DateTime.Now.AddDays(65),
                    Description="Ut fusce varius nisl ac ipsum gravida vel ."
                }
            };
        }
    }
}
