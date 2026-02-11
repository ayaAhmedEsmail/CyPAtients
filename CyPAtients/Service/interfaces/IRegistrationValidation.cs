using CyPatients.DTO;
using CyPatients.Models;

namespace CyPatients.Service.interfaces
{
    public interface IRegistrationValidation
    {
        Task<List<PatientValidationFlag>> GetValidation();
        Task<PatientValidationFlag> GetValidationbyVistisAsync(int visittype, int enitiyID);
        Task<ValidationDTO> AddValidation(int visittype, int enitiyID);
        Task<ValidationDTO> UpdateValidationAsync(int visitID, int enitiyID, ValidationDTO validation);




    }
}
