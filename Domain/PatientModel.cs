using CadDoctor.Domain;
using System.Data;
using System.Text.Json.Serialization;

namespace CadDoctor.Domain
{
    public class PatientModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? lastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? MobileNumber { get; set; }
        public string? Cpf { get; set; }
        public DateTime CreatedOn   { get; set; }
        public string?  createdBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy {  get; set; }
        public Guid DoctorId { get; set; }
        [JsonIgnore]
        public DoctorModel? Doctor { get; set; }
        public ICollection<AppointmentsModel>? Appointments { get; set; }

    }
}
public ICollection<AppointmentsModel>? Appointments { get; set; }
public ICollection<AppointmentsModel>? Appointments { get; set; }
