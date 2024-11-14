using HospitalManagment.Business.Operations.Patient;
using HospitalManagment.Business.Operations.Patient.Dtos;
using HospitalManagment.WebApi.Filters;
using HospitalManagment.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Doctor")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _patientService.GetPatient(id);

            if (patient is null)
                return NotFound();
            else
                return Ok(patient);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
            return Ok(patients);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]

        public async Task<IActionResult> AddPatient(AddPatientRequest request)
        {
            var addPatientDto = new AddPatientDto
            {
                Name = request.Name,
                SurName = request.SurName,
                HealthStatus= request.HealthStatus,
                DiseaseIds = request.DiseaseIds,
            };
            
           var result = await _patientService.AddPatient(addPatientDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
                return Ok();
        }

        [HttpPatch("{id}/healthStatusUpdate")]
        [Authorize(Roles = "Admin,Doctor ")]
        public async Task<IActionResult> DiseaseDiagnoses(int id, string newHealthStatus)
        {
            var result = await _patientService.DiseaseDiagnoses(id, newHealthStatus);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Doctor ")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatient(id);
            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor ")]
        [TimeControlFilter]
        public async Task<IActionResult> UpdatePatient(int id, UpdatePatientRequest request)
        {
            var updatePatientDto = new UpdatePatientDto
            {
                Id = id,
                Name = request.Name,
                SurName = request.SurName,
                DiseaseIds = request.DiseaseIds,
                HealthStatus = request.HealthStatus,
            };

            var result = await _patientService.UpdatePatient(updatePatientDto);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return await GetPatient(id);
        }
    }
}
