using System.Data;

namespace CadDoctor.Domain
{
    public class PatientModel
    {
        public Guid Id { get; set; }
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
        public DoctorModel? Doctor { get; set; }
    }
}
