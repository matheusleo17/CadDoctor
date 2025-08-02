using CadDoctor.Application.Interfaces;
using CadDoctor.Domain;
using CadDoctor.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Security.AccessControl;


namespace CadDoctor.Application.Services
{
    public class DoctorService : ControllerBase, IDoctorService
    {
        private readonly AppDBContext _AppContext;
        private readonly DoctorService _DoctorService;
        private readonly AuthService _AuthService;

        public DoctorService(AppDBContext appContext, AuthService authService)
        {
            _AppContext = appContext;
            _AuthService = authService;
        }
        public async Task<List<DoctorModel>> GetAllDoctorsAsync()
        {
            var getdoctors = await _AppContext.doctors.ToListAsync();
            return getdoctors;
        }
        public async Task<ServiceResult<DoctorModel>> InsertUserDoctor(DoctorModel model)
        {
            var result = new ServiceResult<DoctorModel>();

            try
            {
                if (model.Cpf != null)
                {
                    _AppContext.doctors.Add(model);
                    await _AppContext.SaveChangesAsync();
                    return result.Ok(model);
                }else
                {
                    return result.Fail( "Campo CPF é obrigatorio");
                }
                
            }
            catch (Exception ex)
            {
                return result.Fail("Erro ao cadastrar médico: " + ex.Message);
            }
        }
        public async Task<ServiceResult<DoctorModel>> LoginAsync(string email, string password)
        {
            var result = new ServiceResult<DoctorModel>();

            var getUser = _AppContext.doctors.Where(_ => _.Email == email && _.Password == password).FirstOrDefault();

            if (getUser != null) 
            {
                var token = _AuthService.GenerateToken(email);

                result.Success = true;
                result.Value = getUser;
                result.AddMessage = token;
                return result;

            } else
            {
                result.Success = false;
                result.ErrorMessage = "Usuário ou senha inválidos";
                result.AddMessage = "404";
                return result;

            }
        }

    }
}
