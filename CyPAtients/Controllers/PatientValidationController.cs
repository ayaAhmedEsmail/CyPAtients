using CyPatients.Models;
using CyPatients.Service.interfaces;
using CyPatients.Service.SSE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CyPatients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientValidationController : ControllerBase
    {
        private IRegistrationValidation _registrationValidation;
        private readonly SseConnectionManager _sseConnectionManager;

        public PatientValidationController(IRegistrationValidation registrationValidation)
        {
            _registrationValidation = registrationValidation;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var query = await _registrationValidation.GetValidation();

            return Ok(query);
        }

        [HttpGet("{visitType}/{entityID}")]
        public async Task<IActionResult> GetbyVisitType(int visitType, int entityID)
        {
            var validations = await _registrationValidation.GetValidationbyVistisAsync(visitType, entityID);
            return Ok(validations);
        }


        [HttpPost]
        public async Task<IActionResult> Post(int visitID, int entityID,DTO.ValidationDTO validation)
        {
            try
            {
                var updatedValidation = await _registrationValidation.UpdateValidationAsync(visitID, entityID, validation);

               await _sseConnectionManager.Boadcastasync("ValidationUpdate", updatedValidation.ToString());
                return Ok(updatedValidation);
            }
            catch (DbUpdateException ex)
            {
                // Get DB error
                var innerMessage = ex.InnerException?.Message ?? ex.GetBaseException().Message;
                return BadRequest(new { message = innerMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}