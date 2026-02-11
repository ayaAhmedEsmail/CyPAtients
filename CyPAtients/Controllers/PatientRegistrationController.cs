using CyPatients.DTO;
using CyPatients.Models;
using CyPatients.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyPatients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRegistrationController : ControllerBase
    {
        private readonly IpatientService _patientService;

        public PatientRegistrationController(IpatientService patientService)
        {
            _patientService = patientService;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterPatient(PatientCreateDTO dto)
        {
            try
            {
                var newPatient = await _patientService.CreatePatientAsync(dto);
                return CreatedAtAction(nameof(GetPatientsByID), new { id = newPatient.Id }, newPatient);
            }
            catch (DbUpdateException ex)
            {
                // Get DB error
                var innerMessage = ex.InnerException?.Message ?? ex.GetBaseException().Message;
                return BadRequest(new { message = innerMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientsByID(int id)
        {
            var patients = await _patientService.GetPatientByIdAsync(id);

            return Ok(patients);
        }


        [HttpGet("page/{page}")]
        public async Task<IActionResult> GetAllPatients(int page )
        {
            var patients = await _patientService.GetAllPagedPatientsAsync(page);

            return Ok(patients);
        }
        [HttpGet("cursor/{cursor}")]
        public async Task<IActionResult> GetAllCursorPatients(int cursor)
        {
            var patients = await _patientService.GetAllcursorPatientsAsync(cursor);

            return Ok(patients);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient dto)
        {
            if (id != dto.Id) return BadRequest("Patient ID mismatch");

            try
            {
                var updatedPatient = await _patientService.UpdatePatientAsync(id, dto);
                return Ok(updatedPatient);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                await _patientService.DeletePatientAsnc(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}