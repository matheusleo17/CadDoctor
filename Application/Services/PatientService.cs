using CadDoctor.Application.Interfaces;
using CadDoctor.Domain;
using CadDoctor.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices.Marshalling;

namespace CadDoctor.Application.Services
{
    public class PatientService : ControllerBase, IPatientService
    {
        private readonly AppDBContext _appDBContext;
         
        public PatientService (AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }


        public async Task<ServiceResult<PatientModel>> CreatePatientAsync(PatientModel entity)
        {
            var result = new ServiceResult<PatientModel>();
            var validatePatient = _appDBContext.patients.Where(x => x.Cpf == entity.Cpf).FirstOrDefault();

            if (validatePatient != null) {

                result.Success = false;
                result.ErrorMessage = "Ja existe um paciente cadastrado com esse CPF";
                result.StateCode = "409";
                return result;
            } 
            else
            {
                _appDBContext.Add(entity);
                await _appDBContext.SaveChangesAsync();
                result.Success = true;
                result.AddMessage = "Cadastro realizado com sucesso";
                result.Value = entity;
                result.StateCode = "200";
                return result;
            }

        }
        public async Task<ServiceResult<List<PatientModel>>> GetAllPatientsAsync(Guid? id)
        {
            var result = new ServiceResult<List<PatientModel>>();
            if (id != null)
            {
                var getPatient = _appDBContext.patients.Where(x => x.Id == id).FirstOrDefault();
                if (getPatient != null) {

                    result.Success = true;
                    result.Value =  new List<PatientModel> { getPatient};
                    result.StateCode = "200";
                    return result;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "Nenhum Paciente encontrado com esse Id";
                    result.StateCode = "200";
                    return result;
                }


            }
            else
            {
                var allDoctors = _appDBContext.patients.Where(x => x.DeletedBy == null && x.DeletedOn == null).ToList();

                if (allDoctors == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Nenhum paciente encontrado na base";
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
            
        public async Task<ServiceResult<string>>UpdatePatientAsync(PatientModel entity, Guid id)
        {
            var result = new ServiceResult<string>();
            var search = _appDBContext.patients.Where(x=> x.Id == id).FirstOrDefault();
            if (search == null)
            {
                result.Success = false;
                result.ErrorMessage = "Paciente não encontrado.";
                result.AddMessage = "400";
                return result;

            }

            foreach (var newvalues in typeof(PatientModel).GetProperties())
            {
                var updateNewValues = newvalues.GetValue(entity);

                if (updateNewValues != null)
                {
                    newvalues.SetValue(search, updateNewValues);
                }
            }
            var isValid = await _appDBContext.SaveChangesAsync();

            if (isValid > 0)
            {
                result.Success = true;
                result.AddMessage = "Paciente atualizado com sucesso.";
                return result;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Erro ao atualizar o Paciente.";
                return result;
            }
        }
        public async Task<ServiceResult<string>> RemovePatientAsync(Guid id)
        {
            var result = new ServiceResult<string>();
            var removePatient = _appDBContext.patients.Where(x => x.Id == id).FirstOrDefault();

            if (removePatient == null)
            {
                result.Success = false;
                result.ErrorMessage = "Não foi possivel remover o paciente, Id não encontrado";
                result.StateCode = "400";
                return result;

            }
            else
            {
                _appDBContext.Remove(removePatient);
                await _appDBContext.SaveChangesAsync();
                result.Success = true;
                result.AddMessage = "Paciente deletado da Base";
                return result;
            }

        }
    }
}
