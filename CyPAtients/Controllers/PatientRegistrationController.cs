using CyPatients.DTO;
using CyPatients.Hubs;
using CyPatients.Models;
using CyPatients.Service;
using CyPatients.Service.interfaces;
using CyPatients.Service.SSE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CyPatients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRegistrationController : ControllerBase
    {
        private readonly IpatientService _patientService;
        //private readonly IHubContext<PatientHub,IpatientHub> _hubContext;
        private readonly SseConnectionManager _sseConnectionManager;


        //public PatientRegistrationController(IpatientService patientService, IHubContext<PatientHub, IpatientHub> hubContext)
        //{
        //    _patientService = patientService;
        //    _hubContext = hubContext;
        //}
        public PatientRegistrationController(IpatientService patientService, SseConnectionManager sseConnectionManager)
        {
            _patientService = patientService;
            _sseConnectionManager = sseConnectionManager;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterPatient(Patient patient)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var newPatient = await _patientService.CreatePatientAsync(patient);

                // add signalR notification
                //await _hubContext.Clients.All
                //    .PatientRegisterd( $"New patient registered: {newPatient.FirstNameEn} {newPatient.SecondNameEn}");


                // Add Sse 
                await _sseConnectionManager.Boadcastasync(
                    "PatientRegistered",
                    $"New patient registered: {newPatient.FirstNameEn} {newPatient.SecondNameEn}"
                    );
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
                return BadRequest(new { message = ex.Message });
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
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            var updatedPatient = await _patientService.UpdatePatientAsync(id, dto);


            // add signalR notification
            //await _hubContext.Clients.All
            //    .PatientUpdated( $"Patient with ID {id} has been updated.");

            //add sse
            await _sseConnectionManager.Boadcastasync("PatientUpdated", $"Patient with ID {id} has been updated.");

            return Ok(updatedPatient);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                await _patientService.DeletePatientAsnc(id);

                // add signalR notification
                //await _hubContext.Clients.All
                //    .PatientDeleted( $"Patient with ID {id} has been deleted.");

                await _sseConnectionManager.Boadcastasync("PatientDeleted", $"Patient with ID {id} has been deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}