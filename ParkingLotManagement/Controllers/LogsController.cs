using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Models;
using static ParkingLotManagement.ParkingDataStore3;
using System.Linq;
using ParkingLotManagement.DbContexts;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.Entities;

namespace ParkingLotManagement.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ParkingInfoContext _dbContext;

        public LogsController(ParkingInfoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        //CREATE a new log:
        [HttpPost]
        public async Task<IActionResult> CreateLog(LogsForCreationModel logForCreation)
        {
            if (logForCreation == null)
            {
                return BadRequest("Log data is null.");
            }

            // We generate a new unique Id for the log (LogId):
            int newLogId = await _dbContext.Logs.MaxAsync(l => l.LogId) + 1;

            // We create a new log based on the provided data:
            Logs newLog = new Logs //ketu ne vend te Logs qe e mora nga Entities folder e kam pasur LogsModel me perpara po e ndryshova se me dilte error
            {
                LogId = newLogId,
                Code = logForCreation.Code,
                SubscriptionId = logForCreation.SubscriptionId,
                CheckInTime = logForCreation.CheckInTime,
                CheckOutTime = logForCreation.CheckOutTime,
                FirstName = logForCreation.FirstName,
                IsDeleted = false
            };

            // We add the new log to the logs list:
            _dbContext.Logs.Add(newLog);
            await _dbContext.SaveChangesAsync();
;
            // We return the newly created log in the response:
            return CreatedAtRoute("GetLog", new { logId = newLog.LogId }, newLog);
        }

        //VIEW:
        [HttpGet("GetLogs")]
        public async Task<ActionResult<IEnumerable<LogsModel>>> GetLogs(
        [FromQuery] DateTime? date = null,
        [FromQuery] string? code = null,
        [FromQuery] string? firstName = null)
        {
            var query = _dbContext.Logs.AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(log => log.CheckInTime.Date == date.Value.Date);
            }

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(log => log.Code == code);
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(log => log.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase));
            }

            var logsToReturn = await query.ToListAsync();

            if (logsToReturn.Count == 0)
            {
                return NotFound();
            }

            return Ok(logsToReturn);
        }
        /*
        Kodi per get methods para se t'i vendosja te gjitha ne nje (i kisha te ndara ketu):
        // Get logs by date (2a):
        [HttpGet("GetLogsByDate/{date}")]
        public async Task<ActionResult<IEnumerable<LogsModel>>> GetLogsByDate(DateTime date)
        {
            var logsToReturn = await _dbContext.Logs
                .Where(log => log.CheckInTime.Date == date.Date)
                .ToListAsync();

            if (logsToReturn.Count == 0)
            {
                return NotFound();
            }

             We could use this code if the retrieved logs are transformed into a specific model (LogsModel) before returning:

             var logsModelToReturn = logsToReturn.Select(log => new LogsModel
            {
                LogId = log.LogId,
                Code = log.Code,
                SubscriptionId = log.SubscriptionId,
                CheckInTime = log.CheckInTime,
                CheckOutTime = log.CheckOutTime,
                FirstName = log.FirstName,
                IsDeleted = log.IsDeleted
            }).ToList();
            return Ok(logsModelToReturn); 

             

            return Ok(logsToReturn);
        }

        //Get logs by code:
        [HttpGet("GetLogByCode/{code}")]
        public async Task<ActionResult<LogsModel>> GetLog (string code)
        {
            var logToReturn = await _dbContext.Logs.FirstOrDefaultAsync(z => z.Code == code);
            if (logToReturn == null)
            {
                return NotFound();
            }
            return Ok(logToReturn);
    }

    //Get logs by subscriber's first name:
    [HttpGet("GetLogBySubscriberFirstName/{firstName}")]
    public async Task<ActionResult<IEnumerable<LogsModel>>> GetLogsBySubscriberFirstName(string firstName)
    {
        var logsToReturn = await _dbContext.Logs
            .Where(log => log.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();

        if (logsToReturn.Count == 0)
        {
            return NotFound();
        }

        return Ok(logsToReturn);
    }
   */

        //DELETE:
        [HttpDelete("{code}")]
        public async Task<ActionResult> DeleteLog(string code)
        {
            var logFromDb = await _dbContext.Logs.FirstOrDefaultAsync(g => g.Code == code);

            if (logFromDb == null)
            {
                return NotFound();
            }

            //We should perform soft delete, so we do it by setting the IsDeleted property to true:
            logFromDb.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
