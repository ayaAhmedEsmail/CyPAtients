using CyPatients.DTO;
using CyPatients.Models;
using CyPatients.Service.interfaces;
using Microsoft.EntityFrameworkCore;

namespace CyPatients.Service
{
    public class RegistrationValidation : IRegistrationValidation
    {
        private readonly CyhealthCare_dbContext _dbContext;

        public RegistrationValidation( CyhealthCare_dbContext context ) { 
            _dbContext = context;
        }


        public Task<ValidationDTO> AddValidation(int visitType, int enitiyID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientValidationFlag>> GetValidation()
        {
           var validation = await  _dbContext.PatientValidationFlags.ToListAsync();
            return validation;
        }

        public async Task<PatientValidationFlag> GetValidationbyVistisAsync(int visittype, int enitiyID)
        {
            var validation = await _dbContext.PatientValidationFlags
                .Where(p => p.VisitTypeId == visittype &&  p.MedicalEntityId == enitiyID).FirstOrDefaultAsync();
            return validation;
        }

        public async Task<ValidationDTO> UpdateValidationAsync(int visitID, int entityID, ValidationDTO validation)
        {
            var existing = await _dbContext.PatientValidationFlags
                .Where(f => f.VisitTypeId == visitID && f.MedicalEntityId==entityID).FirstOrDefaultAsync();

            if (existing == null) throw new Exception("Validation not found");

            existing.FirstNameAr = validation.FirstNameAr;
            existing.FirstNameEn = validation.FirstNameEn;
            existing.SecondNameAr = validation.SecondNameAr;
            existing.SecondNameEn = validation.SecondNameEn;
            existing.ThirdNameAr = validation.ThirdNameAr;
            existing.ThirdNameEn = validation.ThirdNameEn;
            existing.FourthNameAr = validation.FourthNameAr;
            existing.FourthNameEn = validation.FourthNameEn;
            existing.DateOfBirth = validation.DateOfBirth;
            existing.PersonalId = validation.PersonalId;
            existing.FinancialType = validation.FinancialType;
            existing.Gender = validation.Gender;
            existing.MaritalStatusId = validation.MaritalStatusId;
            existing.NationalityId = validation.NationalityId;
            existing.Occupation = validation.Occupation;
            existing.TitleId = validation.TitleId;
            existing.HowDidYouKnowUsId = validation.HowDidYouKnowUsId;
            existing.Country = validation.Country;
            existing.City = validation.City;
            existing.Region = validation.Region;
            existing.AddressDescription = validation.AddressDescription;
            existing.ZipCode = validation.ZipCode;
            existing.InstanceNumber = validation.InstanceNumber;
            existing.Mobile = validation.Mobile;
            existing.Mobile2 = validation.Mobile2;
            existing.Email = validation.Email;
            existing.Contractor = validation.Contractor;
            existing.ContractType = validation.ContractType;
            existing.BeneficiaryType = validation.BeneficiaryType;
            existing.ContractCategory = validation.ContractCategory;
            existing.ContractorClient = validation.ContractorClient;
            await _dbContext.SaveChangesAsync();

            return validation;

        }
    }
}
