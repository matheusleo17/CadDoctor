using CadDoctor.Domain;

namespace CadDoctor.Application.DTO
{
    public class LoginResponseDTO
    {
        public DoctorModel Doctor { get; set; }
        public string Token { get; set; }
    }
}
