using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.DbContexts;
using ParkingLotManagement.Entities;
using ParkingLotManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotManagement.Controllers
{
    [Route("api/pricingPlans")]
    [ApiController]
    public class PricingPlansController : ControllerBase
    {
        private readonly ParkingInfoContext _dbContext;
        public PricingPlansController(ParkingInfoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // CREATE: 
        [HttpPost]
        public async Task<IActionResult> CreatePricingPlan(PricingPlansForCreationModel pricingPlanForCreation)
        {
            if (pricingPlanForCreation == null)
            {
                return BadRequest("PricingPlan data is null.");
            }

            // We generate a new unique Id for the PricingPlan (Id):
            int newPricingPlanId = await _dbContext.PricingPlans.MaxAsync(p => p.Id) + 1;

            // We create a new PricingPlan based on the provided data:
            PricingPlans newPricingPlan = new PricingPlans
            {
                Id = newPricingPlanId,
                HourlyPricing = pricingPlanForCreation.HourlyPricing,
                DailyPricing = pricingPlanForCreation.DailyPricing,
                MinimumHours = pricingPlanForCreation.MinimumHours,
                Type = pricingPlanForCreation.Type,
                IsDeleted = false
            };

            // We add the new PricingPlan to the pricing plan list:
            _dbContext.PricingPlans.Add(newPricingPlan);
            await _dbContext.SaveChangesAsync();
            ;
            // We return the newly created ParkingSpot in the response:
            return CreatedAtRoute("GetPricingPlan", new { id = newPricingPlan.Id }, newPricingPlan);
        }


        //VIEW:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PricingPlansModel>>> GetPricingPlans()
        {
            var pricingPlans = await _dbContext.PricingPlans.ToListAsync();
            return Ok(pricingPlans);
        }

        //UPDATE:
        [HttpPut]
        public async Task<IActionResult> UpdatePricingPlans (PricingPlansForUpdateModel pricingPlan)
        {
            var pricingPlanFromDb = await _dbContext.PricingPlans.FindAsync();

            if(pricingPlanFromDb == null)
            {
                return NotFound();
            }

            //We perform the full update on the entire collection of pricing plans:
            pricingPlanFromDb.Id = pricingPlan.Id;
            pricingPlanFromDb.HourlyPricing = pricingPlan.HourlyPricing;
            pricingPlanFromDb.DailyPricing = pricingPlan.DailyPricing;
            pricingPlanFromDb.MinimumHours = pricingPlan.MinimumHours;
            pricingPlanFromDb.Type = pricingPlan.Type;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //DELETE (here we must write the code for a soft delete):
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePricingPlan(int id)
        {
            var pricingPlanFromDb = await _dbContext.ParkingSpots
                .FirstOrDefaultAsync(i => i.Id == id);

            if (pricingPlanFromDb == null)
            {
                return NotFound();
            }

            // We should perform the soft delete, so we do it by setting the IsDeleted property to true:
            pricingPlanFromDb.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
