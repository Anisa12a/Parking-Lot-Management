using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.DbContexts;
using ParkingLotManagement.Entities;
using ParkingLotManagement.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading.Tasks;

namespace ParkingLotManagement.Controllers
{
    [Route("api/Subscribers")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly ParkingInfoContext _dbContext;

        public SubscribersController(ParkingInfoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        //This helps to check if a person with the same Id card number already exists (1b):
        private async Task<bool> DoesIdCardNumberExist(string idCardNumber)
        {
            return await _dbContext.Subscribers.AnyAsync(s => s.IdCardNumber == idCardNumber);
        }

        // CREATE a new subscriber:
        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(SubscribersForCreationModel subscriberForCreation)
        {
            if (subscriberForCreation == null)
            {
                return BadRequest("Subscriber data is null.");
            }

            // We check if the Id card number already exists
            if (await DoesIdCardNumberExist(subscriberForCreation.IdCardNumber))
            {
                return BadRequest("A subscriber with the same ID card number already exists.");
            }

            // We generate a new unique Id for the subscriber:
            int newSubscriberId = await _dbContext.Subscribers.MaxAsync(s => s.SubscriberId) + 1;

            // We create a new subscriber based on the provided data:
            Subscribers newSubscriber = new Subscribers
            {
                SubscriberId = newSubscriberId,
                FirstName = subscriberForCreation.FirstName,
                LastName = subscriberForCreation.LastName,
                IdCardNumber = subscriberForCreation.IdCardNumber,
                Email = subscriberForCreation.Email,
                PhoneNumber = subscriberForCreation.PhoneNumber,
                Birthday = subscriberForCreation.Birthday,
                PlateNumber = subscriberForCreation.PlateNumber,
                IsDeleted = false
            };

            // We add the new subscriber to the subscribers list:
            _dbContext.Subscribers.Add(newSubscriber);
            await _dbContext.SaveChangesAsync();

            // We return the newly created subscriber in the response:
            return CreatedAtRoute("GetSubscriber", new { subscriberId = newSubscriber.SubscriberId }, newSubscriber);
        }

        //VIEW:
        //Get subscribers by firstname, lastname, idcardnumber and email:
        [HttpGet("GetSubscribers")]
        public async Task<ActionResult<IEnumerable<SubscribersModel>>> GetSubscribers(
        [FromQuery] string firstName,
        [FromQuery] string lastName,
        [FromQuery] string idCardNumber,
        [FromQuery] string email)
        {
            IQueryable<Subscribers> query = _dbContext.Subscribers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(s => s.FirstName == firstName);
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(s => s.LastName == lastName);
            }

            if (!string.IsNullOrWhiteSpace(idCardNumber))
            {
                query = query.Where(s => s.IdCardNumber == idCardNumber);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(s => s.Email == email);
            }

            var subscribersToReturn = await query.ToListAsync();

            if (subscribersToReturn.Count == 0)
            {
                return NotFound("No subscribers found based on the provided data.");
            }

            return Ok(subscribersToReturn);
        }

        /* Get methods kur i kisha vec metodat
        //Get subscribers by firstname:
        [HttpGet("GetSubscriberByFirstName/{firstName}")]
        public async Task<ActionResult<SubscribersModel>> GetSubscriberByFirstName(string firstName)
        {
            var subscriberToReturn = await _dbContext.Subscribers
                .FirstOrDefaultAsync(s => s.FirstName == firstName);
            if (subscriberToReturn == null)
            {
                return NotFound();
            }
            return Ok(subscriberToReturn);
        }

        //Get subscribers by lastname:
        [HttpGet("GetSubscriberByLastName/{lastName}")] 
        public async Task<ActionResult<SubscribersModel>> GetSubscriberByLastName(string lastName)
        {
            var subscriberToReturn = await _dbContext.Subscribers
                .FirstOrDefaultAsync(s => s.LastName == lastName); 
            if (subscriberToReturn == null)
            {
                return NotFound();
            }
            return Ok(subscriberToReturn);
        }

        //Get subscribers by id card number:
        [HttpGet("GetSubscriberByIdCardNumber/{idCardNumber}")]
        public async Task<ActionResult<SubscribersModel>> GetSubscriberByIdCardNumber(string idCardNumber)
        {
            var subscriberToReturn = await _dbContext.Subscribers
                .FirstOrDefaultAsync(s => s.IdCardNumber == idCardNumber);
            if (subscriberToReturn == null)
            {
                return NotFound();
            }
            return Ok(subscriberToReturn);
        }

        //Get subscribers by email:
        [HttpGet("GetSubscriberByEmail/{email}")]
        public async Task<ActionResult<SubscribersModel>> GetSubscriberByEmail(string email)
        {
            var subscriberToReturn = await _dbContext.Subscribers
                .FirstOrDefaultAsync(s => s.Email == email);
            if (subscriberToReturn == null)
            {
                return NotFound();
            }
            return Ok(subscriberToReturn);
        }
        */

        //UPDATE:
        [HttpPut]
        public async Task<IActionResult> UpdateSubscribers(SubscribersForUpdateModel subscribers)
        {
            var subscriberFromDb = await _dbContext.Subscribers.FirstOrDefaultAsync();

            if (subscriberFromDb == null)
            {
                return NotFound();
            }

            //We perform the full update on the entire collection of subscribers:
            subscriberFromDb.SubscriberId = subscribers.SubscriberId;
            subscriberFromDb.FirstName = subscribers.FirstName;
            subscriberFromDb.LastName = subscribers.LastName;
            subscriberFromDb.IdCardNumber = subscribers.IdCardNumber;
            subscriberFromDb.Email = subscribers.Email;
            subscriberFromDb.PhoneNumber = subscribers.PhoneNumber;
            subscriberFromDb.Birthday = subscribers.Birthday;
            subscriberFromDb.PlateNumber = subscribers.PlateNumber;
            subscriberFromDb.IsDeleted = subscribers.IsDeleted;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //DELETE (here we must write the code for a soft delete):
        [HttpDelete("{firstName}")]
        public async Task<IActionResult> DeleteSubscriber(string firstName)
        {
            var subscriberFromDb = await _dbContext.Subscribers
                .FirstOrDefaultAsync(s => s.FirstName == firstName);

            if (subscriberFromDb == null)
            {
                return NotFound();
            }

            // We should perform the soft delete, so we do it by setting the IsDeleted property to true:
            subscriberFromDb.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
