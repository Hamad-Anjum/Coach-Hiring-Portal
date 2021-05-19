using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Domains.Context;
using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHS.Api.Controllers.Common
{
    [Authorize(Roles = "Student,Gym,Trainer")]
    public class DetailsController : CommonController
    {
        private readonly CHSDbContext _db;

        public DetailsController(CHSDbContext db)
        {
            _db = db;
        }

        //For Student Role
        [Route("TrainerDetailsForStudent")]
        public IEnumerable<MemberTrainersViewModel> TrainerDetailsForStudent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return new List<MemberTrainersViewModel>
            {
                new MemberTrainersViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now
                },
                new MemberTrainersViewModel
                {
                    Designation = "Karate Master",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Karate Training",
                    SubscriptionDuration=2,
                    SubscriptionDate=DateTime.Now
                },
                new MemberTrainersViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=3,
                    SubscriptionDate=DateTime.Now
                },
                new MemberTrainersViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=8,
                    SubscriptionDate=DateTime.Now
                },
                new MemberTrainersViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=7,
                    SubscriptionDate=DateTime.Now
                },
                new MemberTrainersViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now
                }
            };
        }

        //For Gym Role
        [Route("MemberDetailsForGym")]
        public IEnumerable<MemberGymsViewModel> MemberDetailsForGym(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return new List<MemberGymsViewModel>
            {
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddMonths(-1),
                    LastPresentDate=DateTime.Now.AddDays(-20),
                    IsStillActive=false
                },
                new MemberGymsViewModel
                {
                    Designation = "Karate Master",
                    FirstName="Arslan",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Karate Training",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddMonths(-2),
                    LastPresentDate=DateTime.Now.AddDays(-5),
                    IsStillActive=true
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddMonths(-3),
                    LastPresentDate=DateTime.Now.AddDays(-13),
                    IsStillActive=true
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddMonths(-4),
                    LastPresentDate=DateTime.Now.AddDays(-10),
                    IsStillActive=true
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddDays(-10),
                    LastPresentDate=DateTime.Now.AddMonths(-2),
                    IsStillActive=false
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddMonths(-2),
                    LastPresentDate=DateTime.Now.AddDays(-20),
                    IsStillActive=false
                },
                new MemberGymsViewModel
                {
                    Designation = "Karate Master",
                    FirstName="Arslan",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Karate Training",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddDays(-20),
                    LastPresentDate=DateTime.Now.AddDays(-5),
                    IsStillActive=true
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddDays(-28),
                    LastPresentDate=DateTime.Now.AddDays(-13),
                    IsStillActive=true
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddDays(-35),
                    LastPresentDate=DateTime.Now.AddDays(-10),
                    IsStillActive=true
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddDays(-40),
                    LastPresentDate=DateTime.Now.AddMonths(-2),
                    IsStillActive=false
                },
                new MemberGymsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionDate=DateTime.Now.AddMonths(-2),
                    LastPresentDate=DateTime.Now.AddMonths(-1),
                    IsStillActive=false
                }
            };
        }

        //For Gym Role
        [Route("MemberDetailsForTrainer")]
        public async Task<IEnumerable<MemberStudentsViewModel>> MemberDetailsForTrainer(Guid id)
        {
            if (id == default)
            {
                return null;
            }

            var result = await _db.Subscriptions.Where(x => x.SubscriptionOwnerId == id && x.IsActive)
                                  .Join(_db.MemberSubscriptions.Include(x => x.Payments).Include(x => x.Subscriber).ThenInclude(x => x.Designations),
                                        s => s.Id,
                                        ms => ms.SubscriptionId,
                                        (s, ms) => new MemberStudentsViewModel
                                        {
                                            MemberId = ms.Subscriber.Id,
                                            FirstName = ms.Subscriber.FirstName,
                                            LastName = ms.Subscriber.LastName,
                                            Designation = ms.Subscriber.Designations.FirstOrDefault().Title,
                                            SubscriptionId = s.Id,
                                            Price = s.Price,
                                            SubscriptionName = s.Name,
                                            SubscriptionDuration = s.SubscriptionMonths,
                                            SubscriptionStartDate = ms.StartTime,
                                            LastPresentDate = (DateTime)ms.Subscriber.LastModifiedDate,
                                            IsStillActive = ms.Subscriber.LastModifiedDate.Value.AddDays(20) > DateTime.Now,
                                            Payments = ms.Payments.Select(x => new PaymentViewModel
                                            {
                                                PayDate = x.DatePaid,
                                                PayedAmount = x.PayedAmount,
                                            }).ToList()
                                        }).ToListAsync();

            if (result != null)
            {
                if (result.Count > 0)
                {
                    return result;
                }
            }
            return new List<MemberStudentsViewModel>
            {
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-20),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Karate Master",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Karate Training",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-5),
                    IsStillActive=true
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-13),
                    IsStillActive=true
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-10),
                    IsStillActive=true
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddMonths(-2),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddMonths(-1),
                    IsStillActive=false
                }
            };
        }

        //For Gym Role
        [Route("NonActiveMemberDetailsForTrainer")]
        public IEnumerable<MemberStudentsViewModel> NonActiveMemberDetailsForTrainer(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return new List<MemberStudentsViewModel>
            {
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-20),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Karate Master",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Karate Training",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-5),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-13),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-10),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddMonths(-2),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddMonths(-1),
                    IsStillActive=false
                }
            };
        }

        //For Gym Role
        [Route("NonActiveMemberDetailsForGym")]
        public IEnumerable<MemberStudentsViewModel> NonActiveMemberDetailsForGym(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return new List<MemberStudentsViewModel>
            {
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing Training",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-20),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Karate Master",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Karate Training",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-5),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-13),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddDays(-10),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddMonths(-2),
                    IsStillActive=false
                },
                new MemberStudentsViewModel
                {
                    Designation = "Boxer",
                    FirstName="Hamad",
                    LastName="Anjum",
                    Price=212.32M,
                    SubscriptionName="Boxing",
                    SubscriptionDuration=5,
                    SubscriptionStartDate=DateTime.Now,
                    LastPresentDate=DateTime.Now.AddMonths(-1),
                    IsStillActive=false
                }
            };
        }
    }
}
