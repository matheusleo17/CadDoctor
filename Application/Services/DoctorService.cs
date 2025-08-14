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
        private readonly AuthService _AuthService;

        public DoctorService(AppDBContext appContext, AuthService authService)
        {
            _AppContext = appContext;
            _AuthService = authService;
        }
        public async Task<ServiceResult<List<DoctorModel>>> GetAllDoctorsAsync(Guid? id)
        {
            var result = new ServiceResult<List<DoctorModel>>();
            if (id != null)
            {
                var getDoctors = _AppContext.doctors.Where(x => x.Id == id).FirstOrDefault();
                if (getDoctors != null)
                {

                    result.Success = true;
                    result.Value = new List<DoctorModel> { getDoctors };
                    result.StateCode = "200";
                    return result;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "Nenhum Medico encontrado com esse Id";
                    result.StateCode = "200";
                    return result;
                }


            }
            else
            {
                var allDoctors = _AppContext.doctors.Where(x => x.DeletedBy == null && x.DeletedOn == null).ToList();

                if (allDoctors == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Nenhum Medico encontrado na base";
                    result.StateCode = "200";
                    return result;
                }
                else
                {
                    result.Success = true;
                    result.Value = allDoctors;
                    result.StateCode = "200";
                    return result;

                }
            }
        }
        public async Task<ServiceResult<DoctorModel>> InsertUserDoctor(DoctorModel model)
        {
            var result = new ServiceResult<DoctorModel>();

            try
            {
                if (model.Cpf != null)
                {
                    if (model.Password != null)
                    {
                        var HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                        model.Password = HashedPassword;

                    } else
                    {
                        result.AddMessage = "A senha não pode ser nula.";
                        result.Success = false;
                        result.StateCode = "400";
                        return result;
                    }
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

            var getUser = _AppContext.doctors.Where(_ => _.Email == email).FirstOrDefault();
            if (getUser == null)
            {
                result.Success = false;
                result.AddMessage = "Usuário não encontrado.";
                return result;
            }
            bool senhaValida = BCrypt.Net.BCrypt.Verify(password, getUser?.Password);

            if (getUser != null && senhaValida == true) 
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
        public async Task<ServiceResult<DoctorModel>> UpdateDoctorAsync(DoctorModel model, Guid? id)
        {
            var result = new ServiceResult<DoctorModel>();


            var search = _AppContext.doctors.Where(x=>x.Id ==id).FirstOrDefault();

            if (search == null)
            {
                result.Success = false;
                result.ErrorMessage = "Médico não encontrado.";
                result.AddMessage = "400";
                return result;
                
            }

            foreach (var newvalues in typeof(DoctorModel).GetProperties())
            {
                var updateNewValues = newvalues.GetValue(model);

                if (updateNewValues != null)
                {
                    newvalues.SetValue(search, updateNewValues);
                }
            }
            var isValid = await _AppContext.SaveChangesAsync();

            if (isValid > 0)
            {
                result.Success = true;
                result.AddMessage = "Medico atualizado com sucesso.";
                result.Data = search;
                return result;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Erro ao atualizar o medico.";
                return result;
            }

        }
        public async Task<ServiceResult<string>> RemoveDoctor(Guid id)
        {
            var result = new ServiceResult<string>();

            var doctorByid = _AppContext.doctors.Where(x => x.Id == id).FirstOrDefault();
            if (doctorByid != null)
            {
                _AppContext.Remove(doctorByid);
                await _AppContext.SaveChangesAsync();
                result.Success= true;
                result.AddMessage = "Medico deletado da Base";
                return result;
            }
            else
            {
                result.ErrorMessage = "Medico não encontrado na base.";
                result.Success = false;
                return result;
            }
        }

    }
}
