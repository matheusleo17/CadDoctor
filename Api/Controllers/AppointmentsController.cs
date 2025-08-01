using CadDoctor.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace CadDoctor.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;

        public AppointmentsController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
