using CadDoctor.Application.Interfaces;
using CadDoctor.Application.Services;
using CadDoctor.Domain;
using CadDoctor.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadDoctor.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDBContext _appContext;
        private readonly PatientService _patientService;
        public PatientsController(AppDBContext appContext, PatientService patientService)
        {
            _appContext = appContext;
            _patientService = patientService;
        }
        [HttpPost]
        [Route("CreatePatient")]
        public async Task<ActionResult<ServiceResult<PatientModel>>> CreatePatient(PatientModel entity)
        {
            var result = await _patientService.CreatePatientAsync(entity);
            if(result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpGet]
        [Route("GetDoctors")]
        [Authorize]
        public async Task<ServiceResult<List<PatientModel>>> GetDoctors()
        {
            var result = await _patientService.GetAllDoctorsAsync();
            return result;
        }
        [HttpGet]
        [Route("UpdatePatient")]
        [Authorize]
        public async Task<ServiceResult<string>> UpdatePatient(PatientModel entity, Guid id)
        {

            var result = await _patientService.UpdatePatientAsync(entity, id);

            if (result.Success == true)
            {
                return result;
            }
            else 
            {
                return result;

            }

            
        }
        [HttpDelete]
        [Route("DeletePatient")]
        [Authorize]
        public async Task<ServiceResult<string>> DeletePatient(Guid id)
        {
            var result = await _patientService.RemovePatientAsync(id);

            if (result.Success == false)
            {
                return result;
            }
            else
            {
                return result;
            }
        }
    }
}
