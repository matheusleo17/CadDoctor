using System;
using System.Collections.Generic;

namespace CadDoctor.Domain
{
    public class DoctorModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
        public string? Cpf { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? createdBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy   { get; set; }
        public ICollection<PatientModel>? Patients { get; set; }
    }
}