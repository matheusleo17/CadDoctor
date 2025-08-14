using CadDoctor.Domain;

namespace CadDoctor.Application.Interfaces
{
    public interface IAppointments
    {
        Task<ServiceResult<List<AppointmentsModel>>> GetAllAppointmentsAsync(Guid? id);
        Task<ServiceResult<AppointmentsModel>> InsertAppointmentsAsync(AppointmentsModel model);
        Task<ServiceResult<AppointmentsModel>> UpdateAppointmentsAsync(AppointmentsModel model, Guid? id);
        Task<ServiceResult<string>> RemoveAppointmentsAsync(Guid id);
    }
}
