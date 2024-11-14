using HospitalManagment.Business.Operations.Disease;
using HospitalManagment.Business.Operations.Disease.Dto;
using HospitalManagment.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseasesController(IDiseaseService disease)
        {
            _diseaseService = disease;
        }

        [HttpPost]
        [Authorize(Roles = "Doctor ")]
        public async Task<IActionResult> AddDisease(AddDiseaseRequest request)
        {
            var addDiseaseDto = new AddDiseaseDto
            {
                DiseaseName = request.DiseaseName,
            };

            var result = await _diseaseService.AddDisease(addDiseaseDto);

            if (result.IsSucceed)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }
}
