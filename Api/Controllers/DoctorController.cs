using CadDoctor.Application.DTO;
using CadDoctor.Application.Interfaces;
using CadDoctor.Application.Services;
using CadDoctor.Domain;
using CadDoctor.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


namespace CadDoctor.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;
        private readonly IDoctorService _doctorService;
        private readonly AuthService _authService;

        public DoctorController (AppDBContext appContext, IDoctorService doctorService, AuthService authService)
        {
            _appDBContext = appContext;
            _doctorService = doctorService;
            _authService = authService;



        }
        [HttpPost]
        [Route("Login")]

        public async Task <ActionResult<DoctorModel>> Login(string email, string password)
        {
            var result = await _doctorService.LoginAsync(email, password);
            if (result.Success== true && result.Data != null)
            {
                var token = _authService.GenerateToken(email);

                var response = new LoginResponseDTO
                {
                    Doctor = result.Data,
                    Token = token
                };
                return Ok(response);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<DoctorModel>> Register(DoctorModel entity)
        {
            var result = await _doctorService.InsertUserDoctor(entity);

            if (result.Success && result.Data != null)
            {
                return Ok(result.Data ); 
            } else if (result.Success == false)
            {
                return BadRequest(result.ErrorMessage);

            }

            return BadRequest(result.ErrorMessage);

        }


        [HttpGet]
        [Authorize]
        [Route("getAllDoctors")]
        public async Task<ActionResult<List<DoctorModel>>> GetAllDoctors()
        {
            var entity = await _doctorService.GetAllDoctorsAsync();
            return Ok(entity);
        }
    }
}
