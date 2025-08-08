using CadDoctor.Application.Services;
using CadDoctor.Domain;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CadDoctor.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorModel>> GetAllDoctorsAsync();
        Task<ServiceResult<DoctorModel>> InsertUserDoctor(DoctorModel model);
        Task<ServiceResult<DoctorModel>> LoginAsync(string login, string password);
        Task<ServiceResult<DoctorModel>> UpdateDoctorAsync(DoctorModel model, Guid? id);

    }
}
