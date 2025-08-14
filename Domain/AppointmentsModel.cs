using System.Text.Json.Serialization;

namespace CadDoctor.Domain
{
    public class AppointmentsModel
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime? date { get; set; }
        public string? AppointmentStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? createdBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public Guid DoctorId { get; set; }
        [JsonIgnore]
        public DoctorModel? Doctor { get; set; }
        public Guid PatientId { get; set; }
        [JsonIgnore]
        public PatientModel? Patient { get; set; }

    }
}
