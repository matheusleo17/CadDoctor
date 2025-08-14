using CadDoctor.Application.Interfaces;
using CadDoctor.Application.Services;
using CadDoctor.Domain;
using CadDoctor.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace CadDoctor.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        private readonly IAppointments _appointments;

        public AppointmentsController(AppDBContext appDbContext, IAppointments appointments)
        {
            _appDbContext = appDbContext;
            _appointments = appointments;
        }
        [HttpGet]
        [Authorize]
        [Route("GetAppointments")]
        public async Task <ActionResult<AppointmentsModel>> GetAllAppointments(Guid? id)
        {
            var entity = await _appointments.GetAllAppointmentsAsync(id);
            return Ok(entity);
        }

        [HttpPost]
        [Authorize]
        [Route("InsertAppointments")]
        public async Task<ActionResult<AppointmentsModel>> InsertAppointments(AppointmentsModel entity)
        {
            var result = await _appointments.InsertAppointmentsAsync(entity);

            if (result.Success && result.Data != null)
            {
                return Ok(result.Data);
            }
            else if (result.Success == false)
            {
                return BadRequest(result.ErrorMessage);

            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        [Authorize]
        [Route("UpdateAppointMents")]
        public async Task<ActionResult<AppointmentsModel>> UpdateAppointMents(AppointmentsModel entity, Guid id)
        {
            var result = await _appointments.UpdateAppointmentsAsync(entity, id);

            if (result.Success && result.Data != null)
            {
                return Ok(result.Data);
            }
            else if (result.Success == false)
            {
                return BadRequest(result.ErrorMessage);

            }

            return BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpPost]
        [Route("InactiveAppointments")]
        public async Task<ActionResult<AppointmentsModel>> InactiveAppointments(Guid id)
        {
            var result = await _appointments.RemoveAppointmentsAsync(id);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }

        }
    }
}
