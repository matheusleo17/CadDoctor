using CadDoctor.Application.Interfaces;
using CadDoctor.Domain;
using CadDoctor.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CadDoctor.Application.Services
{
    public class AppointmentsService : ControllerBase, IAppointments
    {
        private readonly AppDBContext _AppContext;

        public AppointmentsService(AppDBContext dbContext)
        {
            _AppContext = dbContext;
        }

        public async Task<ServiceResult<List<AppointmentsModel>>> GetAllAppointmentsAsync(Guid? id)
        {
            var result = new ServiceResult<List<AppointmentsModel>>();

            if (id != null)
            {
                var getAppointments = _AppContext.appointments.Where(x => x.id == id).FirstOrDefault();
                if (getAppointments != null)
                {

                    result.Success = true;
                    result.Value = new List<AppointmentsModel> { getAppointments };
                    result.StateCode = "200";
                    return result;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "Nenhuma consulta encontrada com esse Id";
                    result.StateCode = "200";
                    return result;
                }


            }
            else
            {
                var allAppointments = _AppContext.appointments.Where(x => x.DeletedBy == null && x.DeletedOn == null).ToList();

                if (allAppointments == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Nenhuma consulta encontrado na base";
                    result.StateCode = "200";
                    return result;
                }
                else
                {
                    result.Success = true;
                    result.Value = allAppointments;
                    result.StateCode = "200";
                    return result;

                }
            }

        }
        public async Task<ServiceResult<AppointmentsModel>> InsertAppointmentsAsync(AppointmentsModel model)
        {
            var result = new ServiceResult<AppointmentsModel>();

            try
            {

                   _AppContext.appointments.Add(model);
                   await _AppContext.SaveChangesAsync();
                    return result.Ok(model);

            }
            catch (Exception ex)
            {
                return result.Fail("Erro ao cadastrar médico: " + ex.Message);
            }
        }
        public async Task<ServiceResult<AppointmentsModel>> UpdateAppointmentsAsync(AppointmentsModel model, Guid? id)
        {
            var result = new ServiceResult<AppointmentsModel>();


            var search = _AppContext.appointments.Where(x => x.id == id).FirstOrDefault();

            if (search == null)
            {
                result.Success = false;
                result.ErrorMessage = "Médico não encontrado.";
                result.AddMessage = "400";
                return result;

            }

            foreach (var newvalues in typeof(AppointmentsModel).GetProperties())
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
                result.AddMessage = "Consulta atualizado com sucesso.";
                result.Data = search;
                return result;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Erro ao atualizar consulta.";
                return result;
            }
        }
        public async Task<ServiceResult<string>> RemoveAppointmentsAsync(Guid id)
        {
            var result = new ServiceResult<string>();

            var AppointmentByid = _AppContext.doctors.Where(x => x.Id == id).FirstOrDefault();
            if (AppointmentByid != null)
            {
                _AppContext.Remove(AppointmentByid);
                await _AppContext.SaveChangesAsync();
                result.Success = true;
                result.AddMessage = "Consulta deletada da Base";
                return result;
            }
            else
            {
                result.ErrorMessage = "Consulta não encontrada na base.";
                result.Success = false;
                return result;
            }

        }



    }
}
