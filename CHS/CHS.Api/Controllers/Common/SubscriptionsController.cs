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
    public class SubscriptionsController : CommonController
    {
        private readonly CHSDbContext _db;

        public SubscriptionsController(CHSDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id:guid}")]
        public async Task<SubscriptionViewModel> Get(Guid id)
        {
            var result = await _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
            return new SubscriptionViewModel
            {
                SubscriptionId = result.Id,
                Name = result.Name,
                Price = result.Price,
                SubscriptionMonths = result.SubscriptionMonths,
                Detail = result.Detail,
                TrainingDays = result.TrainingDays,
                TrainingTiming = result.TrainingTiming,
                Picture = result.Picture,
                Description = result.Description,
                SubscriptionOwnerMemberId = result.SubscriptionOwnerId,
                Index = result.Index,
                IsActive = result.IsActive,
                IsDelete = result.IsDelete,
                CreatedAt = result.CreatedAt,
                CreatedBy = result.CreatedBy,
                LastModifiedDate = DateTime.Now,
                LastModifiedBy = HttpContext.User.Identity.Name
            };
        }

        [HttpGet]
        [Route("GetAllByOwners")]
        public async IAsyncEnumerable<SubscriptionViewModel> GetAllByOwners()
        {
            if (HttpContext.User.Claims.Any(x => x.Type == "memberid"))
            {
                var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "memberid").Value;
                //var result = await _db.Subscriptions.Where(x => x.SubscriptionOwnerMemberId == Guid.Parse(id)).ToListAsync();
                await foreach (var item in _db.Subscriptions.Where(x => x.SubscriptionOwnerId == Guid.Parse(id))
                                                            .Select(x => new SubscriptionViewModel
                                                            {
                                                                SubscriptionId = x.Id,
                                                                Name = x.Name,
                                                                Price = x.Price,
                                                                SubscriptionMonths = x.SubscriptionMonths,
                                                                Detail = x.Detail,
                                                                Description = x.Description,
                                                                SubscriptionOwnerMemberId = x.SubscriptionOwnerId,
                                                                Index = x.Index,
                                                                IsActive = x.IsActive,
                                                                IsDelete = x.IsDelete,
                                                                CreatedAt = x.CreatedAt,
                                                                CreatedBy = x.CreatedBy,
                                                                LastModifiedDate = x.LastModifiedDate,
                                                                LastModifiedBy = x.LastModifiedBy
                                                            }).AsAsyncEnumerable())
                {
                    yield return item;
                }
            }
        }

        [HttpGet]
        [Route("GetAllActive")]
        public async IAsyncEnumerable<SubscriptionViewModel> GetAllActive(Guid id)
        {
            var result = await _db.Subscriptions.Where(x => x.SubscriptionOwnerId == id && x.IsActive).ToListAsync();
            await foreach (var item in _db.Subscriptions.Where(x => x.SubscriptionOwnerId == id && x.IsActive)
                                                        .Select(x => new SubscriptionViewModel
                                                        {
                                                            SubscriptionId = x.Id,
                                                            Name = x.Name,
                                                            Picture = x.Picture,
                                                            Detail = x.Detail,
                                                            Price = x.Price,
                                                            SubscriptionMonths = x.SubscriptionMonths,
                                                            Description = x.Description,
                                                            SubscriptionOwnerMemberId = x.SubscriptionOwnerId,
                                                            CreatedAt = x.CreatedAt,
                                                            LastModifiedDate = x.LastModifiedDate,
                                                        }).AsAsyncEnumerable())
            {
                yield return item;
            }
        }

        [HttpPost]
        [Route("Subscribe")]
        public async Task<IActionResult> Subscribe(MemberSubscriptionViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            decimal monthsToDays = model.SubscriptionMonths * 30;
            var entity = await _db.MemberSubscriptions.AddAsync(new MemberSubscription
            {
                SubscriberId = model.SubscriberId,
                SubscriptionId = model.SubscriptionId,
                StartTime = DateTime.Now,
                ExpiryTime = DateTime.Now.AddDays((int)Math.Floor(monthsToDays)),
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });

            await _db.Payments.AddAsync(new Payment
            {
                PayedAmount = model.PayedAmount,
                MemberSubscriptionId = entity.Entity.Id,
                DatePaid = DateTime.Now,
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });

            if (await _db.SaveChangesAsync() > 0)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("Payment")]
        public async ValueTask<IActionResult> Payment(PaymentViewModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            await _db.Payments.AddAsync(new Payment
            {
                PayedAmount = model.PayedAmount,
                MemberSubscriptionId = model.MemberSubscriptionId,
                DatePaid = DateTime.Now,
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });

            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("ValidateName")]
        public async Task<bool> ValidateName(string newName)
        {
            return await _db.Subscriptions.AnyAsync(x => x.Name.ToLower().Equals(newName.ToLower()));
        }

        [HttpGet]
        [Route("ValidateNameInEdit")]
        public async Task<bool> ValidateNameInEdit(string currentName, string newName)
        {
            var list = await _db.Subscriptions.Where(x => x.Name != currentName).ToArrayAsync();
            return list.Any(x => x.Name.ToLower().Equals(newName.ToLower()));
        }

        [HttpPost]
        public async Task<HttpStatusCode> AddSubscription(SubscriptionViewModel model)
        {
            if (model is null)
            {
                return HttpStatusCode.BadRequest;
            }

            await _db.Subscriptions.AddAsync(new Subscription
            {
                Name = model.Name,
                Price = model.Price,
                SubscriptionMonths = model.SubscriptionMonths,
                Detail = model.Detail,
                TrainingDays = model.TrainingDays,
                TrainingTiming = model.TrainingTiming,
                Picture = model.Picture,
                Description = model.Description,
                SubscriptionOwnerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("memberid")).Value),
                CreatedAt = DateTime.Now,
                CreatedBy = HttpContext.User.Identity.Name
            });

            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.BadRequest;
        }

        [HttpPut("{id:guid}")]
        public async Task<HttpStatusCode> EditSubscription(Guid id, SubscriptionViewModel model)
        {
            if (id == default || model is null)
            {
                return HttpStatusCode.BadRequest;
            }

            model.Index++;
            var subscription = new Subscription
            {
                Id = id,
                Name = model.Name,
                Price = model.Price,
                SubscriptionMonths = model.SubscriptionMonths,
                TrainingDays = model.TrainingDays,
                TrainingTiming = model.TrainingTiming,
                Picture = model.Picture,
                Detail = model.Detail,
                Description = model.Description,
                SubscriptionOwnerId = model.SubscriptionOwnerMemberId,
                Index = model.Index,
                IsActive = model.IsActive,
                IsDelete = model.IsDelete,
                CreatedAt = model.CreatedAt,
                CreatedBy = model.CreatedBy,
                LastModifiedDate = DateTime.Now,
                LastModifiedBy = HttpContext.User.Identity.Name
            };

            _db.Entry(subscription).State = EntityState.Modified;

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

            var result = await _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
            result.Index++;
            result.IsActive = !result.IsActive;
            _db.Entry(result).State = EntityState.Modified;
            if (await _db.SaveChangesAsync() > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }

        [HttpDelete("{id:guid}")]
        public async Task<HttpStatusCode> DeleteSubscription(Guid id)
        {
            if (id == default)
            {
                return HttpStatusCode.BadRequest;
            }

            var result = await _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
            result.Index++;
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
