using CyPatients.DTO;
using CyPatients.Models;

namespace CyPatients.Service.interfaces
{
    public interface IValidation
    {
        // get flages depend on v,e
        Task<PatientValidationFlag> getValidatVisit(int visitTypeID, int entityID);

        // validated 
        List<string> Validat(Object _patient, PatientValidationFlag _rules);
       
    }
}