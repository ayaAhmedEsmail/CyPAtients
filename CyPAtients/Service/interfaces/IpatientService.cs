using CyPatients.DTO;
using CyPatients.Models;

namespace CyPatients.Service.interfaces
{
    public interface IpatientService
    {
        
        Task<CursorPagination<PatientsDTO>> GetAllcursorPatientsAsync(int cursor);
        Task<PagePagination<Patient>> GetAllPagedPatientsAsync(int current);
        Task<PatientListDTO> GetPatientByIdAsync(int id);

        Task<PatientsDTO> CreatePatientAsync(PatientCreateDTO patient);

        Task DeletePatientAsnc(int id);
        Task<PatientsDTO> UpdatePatientAsync(int id, Patient patient);
    }
}
