using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.DbContexts;
using ParkingLotManagement.Entities;
using ParkingLotManagement.Models;
using System.Linq;

namespace ParkingLotManagement.Controllers
{
    [Route("api/Subscriptions")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        public readonly ParkingInfoContext _dbContext;

        public SubscriptionsController(ParkingInfoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        //We add a method to check if a subscription with the same code already exists:
        private async Task<bool> DoesCodeExist(string code)
        {
            return await _dbContext.Subscriptions.AnyAsync(s => s.Code == code);
        }


        //CREATE a new subscription:
        [HttpPost]
        public async Task<IActionResult> CreateSubscription(SubscriptionsForCreationModel subscriptionForCreation)
        {
            if (subscriptionForCreation == null)
            {
                return BadRequest("Subscription data is null.");
            }

            // We check if the subscription code already exists: BESOJJJJJJJ se duhet si tek chat gpt dhe korrigjo dhe tek subscribers kete pjesen qe ta besh njesoj dhe tek Logs shiko njehere
            if (await DoesCodeExist(subscriptionForCreation.Code))
            {
                return BadRequest("A subscription with this code already exists.");
            }

            // Generate a new unique id for the subscription:
            int newSubscriptionId = await _dbContext.Subscriptions.MaxAsync(s => s.SubscriptionId) + 1;

            // We create a new subscription based on the provided data: DHEEEEEEEE ketu shiko se shton nje variabel ne chat gpt
            Subscriptions newSubscription = new Subscriptions
            {
                SubscriptionId = newSubscriptionId,
                Code = subscriptionForCreation.Code,
                SubscriberId = subscriptionForCreation.SubscriberId,
                DiscountValue = subscriptionForCreation.DiscountValue,
                StartDate = subscriptionForCreation.StartDate,
                EndDate = subscriptionForCreation.EndDate,
                IsDeleted = false,
            };

            // Add the new subscription to the subscription list:
            _dbContext.Subscriptions.Add(newSubscription);
            await _dbContext.SaveChangesAsync();

            // Return the newly created subscription in the response:
            return CreatedAtRoute("GetSubscription", new { subscriptionId = newSubscription.SubscriptionId }, newSubscription);
        }

        //VIEW:
        [HttpGet("GetSubscriptions")]
        public async Task<ActionResult<IEnumerable<Subscriptions>>> GetSubscriptions(string code, string subscriberName)
        {
            IQueryable<Subscriptions> query = _dbContext.Subscriptions.Include(s => s.Subscriber);

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(s => s.Code == code);
            }

            if (!string.IsNullOrEmpty(subscriberName))
            {
                query = query.Where(s =>
                    s.Subscriber != null &&
                    s.Subscriber.FirstName.Equals(subscriberName, StringComparison.OrdinalIgnoreCase));
            }

            var subscriptions = await query.ToListAsync();

            if (subscriptions.Count == 0)
            {
                return NotFound("No subscriptions found based on the provided data.");
            }

            return Ok(subscriptions);
        }

        /* Kur i kisha vec requestet per get:
        //Get subscriptions by code:
        [HttpGet("GetSubscriptionByCode/{code}")]
        public async Task<ActionResult<SubscriptionsModel>> GetSubscription(string code)
        {
            var subscriptionToReturn = await _dbContext.Subscriptions
                .FirstOrDefaultAsync(o => o.Code == code);
            if (subscriptionToReturn == null)
            {
                return NotFound();
            }
            return Ok(subscriptionToReturn);
        }

        //Get subscriptions by subscriber name:
        [HttpGet("GetSubscriptionBySubscriberFirstName/{firstName}")]
        public async Task<ActionResult<SubscriptionsModel>> GetSubscriptionBySubscriberFirstName(string firstName)
        {
            var subscriber = await _dbContext.Subscribers.FirstOrDefaultAsync(s => s.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase));
            if (subscriber == null)
            {
                return NotFound($"Subscriber with first name '{firstName}' not found.");
            }

            var subscriptionToReturn = await _dbContext.Subscriptions.FirstOrDefaultAsync(s => s.SubscriberId == subscriber.SubscriberId);
            if (subscriptionToReturn == null)
            {
                return NotFound($"No subscription found for subscriber '{firstName}'.");
            }

            return Ok(subscriptionToReturn);
        } */

        /* public ActionResult<IEnumerable<SubscriptionsModel>> GetSubscriptionsBySubscriberFirstName(string firstName)
         {
             var subscriptionsToReturn = _parkingDataStore3.Subscriptions
                 .Where(subscription => _parkingDataStore2.Subscribers
                     .Any(subscriber => subscriber.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                        subscriber.SubscriberId == subscription.SubscriberId))
                 .ToList();

             if (subscriptionsToReturn.Count == 0)
             {
                 return NotFound();
             }

             return Ok(subscriptionsToReturn);
         } */

        //UPDATE:
        [HttpPut]
        public async Task<IActionResult> UpdateSubscriptions(SubscriptionsForUpdateModel subscription)
        {
            var subscriptionFromDb = await _dbContext.Subscriptions.FirstOrDefaultAsync();

            if (subscriptionFromDb == null)
            {
                return NotFound();
            }

            //Perform the full update on the entire collection of subscriptions:
            subscriptionFromDb.SubscriptionId = subscription.SubscriptionId;
            subscriptionFromDb.Code = subscription.Code;
            subscriptionFromDb.SubscriberId = subscription.SubscriberId;
            subscriptionFromDb.DiscountValue = subscription.DiscountValue;
            subscriptionFromDb.StartDate = subscription.StartDate;
            subscriptionFromDb.EndDate = subscription.EndDate;
            subscriptionFromDb.IsDeleted = subscription.IsDeleted;

            await _dbContext.SaveChangesAsync();


            return NoContent();
        }

        //DELETE:
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteSubscription(string code)
        {
            var subscriptionFromDb = await _dbContext.Subscriptions
                .FirstOrDefaultAsync(p => p.Code == code);

            if (subscriptionFromDb == null)
            {
                return NotFound();
            }

            //We should perform the soft delete, so we do it by setting the IsDeleted property to true:
            subscriptionFromDb.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


    }
}

