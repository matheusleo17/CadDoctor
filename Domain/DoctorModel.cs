using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CadDoctor.Domain
{
    public class DoctorModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
        public string? Cpf { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? createdBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy   { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}