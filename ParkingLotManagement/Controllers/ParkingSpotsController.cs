using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.DbContexts;
using ParkingLotManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ParkingLotManagement.Entities;

namespace ParkingLotManagement.Controllers
{
    [ApiController]
    [Route("api/parkingSpots")]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly ParkingInfoContext _dbContext;

        public ParkingSpotsController(ParkingInfoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // CREATE: 
        [HttpPost]
        public async Task<IActionResult> CreateParkingSpot(ParkingSpotsForCreationModel parkingSpotForCreation)
        {
            if (parkingSpotForCreation == null)
            {
                return BadRequest("ParkingSpot data is null.");
            }

            // We generate a new unique Id for the ParkingSpot (Id):
            int newParkingSpotId = await _dbContext.ParkingSpots.MaxAsync(p => p.Id) + 1;

            // We create a new ParkingSpot based on the provided data:
            ParkingSpots newParkingSpot = new ParkingSpots
            {
                Id = newParkingSpotId,
                ReservedSpots = parkingSpotForCreation.ReservedSpots,
                RegularSpots = parkingSpotForCreation.RegularSpots,
                IsDeleted = false
            };

            // We add the new ParkingSpot to the parking spots list:
            _dbContext.ParkingSpots.Add(newParkingSpot);
            await _dbContext.SaveChangesAsync();
            ;
            // We return the newly created ParkingSpot in the response:
            return CreatedAtRoute("GetParkingSpot", new { id = newParkingSpot.Id }, newParkingSpot);
        }

        // VIEW:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpotsModel>>> GetParkingSpots()
        {
            var parkingSpots = await _dbContext.ParkingSpots.ToListAsync();
            return Ok(parkingSpots);
        }

         //UPDATE:
        [HttpPut] 
        public async Task<IActionResult> UpdateParkingSpots(ParkingSpotsForUpdateModel parkingSpot)
        {
            var parkingSpotFromDb = await _dbContext.ParkingSpots.FindAsync(); 

            if (parkingSpotFromDb == null)
            {
                return NotFound();
            }

            // We perform the full update on the entire collection of parking spots:
            parkingSpotFromDb.Id = parkingSpot.Id;
            parkingSpotFromDb.ReservedSpots = parkingSpot.ReservedSpots;
            parkingSpotFromDb.RegularSpots = parkingSpot.RegularSpots;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //DELETE (here we must write the code for a soft delete):

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpot(int id)
        {
            var parkingSpotFromDb = await _dbContext.ParkingSpots
                .FirstOrDefaultAsync(i => i.Id == id);

            if (parkingSpotFromDb == null)
            {
                return NotFound();
            }

            // We should perform the soft delete, so we do it by setting the IsDeleted property to true:
            parkingSpotFromDb.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
