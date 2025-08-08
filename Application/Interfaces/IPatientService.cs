using CadDoctor.Domain;

namespace CadDoctor.Application.Interfaces
{
    public interface IPatientService
    {

        Task<ServiceResult<PatientModel>> CreatePatientAsync(PatientModel entity);
        Task<ServiceResult<List<PatientModel>>> GetAllDoctorsAsync();
        Task<ServiceResult<string>> UpdatePatientAsync(PatientModel entity, Guid id);
        Task<ServiceResult<string>> RemovePatientAsync(Guid id);
    }
}
