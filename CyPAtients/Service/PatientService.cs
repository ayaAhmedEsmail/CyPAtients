using CyPatients.DTO;
using CyPatients.Models;
using CyPatients.Service.interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CyPatients.Service
{
    public class PatientService: IpatientService
    {
        private readonly CyhealthCare_dbContext _context;

        private readonly IValidation _validation;


      public PatientService(CyhealthCare_dbContext context  ,IValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<PatientsDTO> CreatePatientAsync(PatientCreateDTO _patient)
        {

            var _rules = await _validation.getValidatVisit(_patient.VisitTypeId, _patient.BranchId);


            var errors =  _validation.Validat(_patient, _rules);

            var patient = new Patient
            {
                VisitTypeId = _patient.VisitTypeId,
                BranchId = _patient.BranchId,
                Status = _patient.Status,
                IsBlackListed = _patient.IsBlackListed,
                PatientStatus = _patient.PatientStatus,
                FirstNameEn = _patient.FirstNameEn,
                SecondNameEn = _patient.SecondNameEn,
                ThirdNameEn = _patient.ThirdNameEn,
                FourthNameEn = _patient.FourthNameEn,
                FirstNameAr = _patient.FirstNameAr,
                SecondNameAr = _patient.SecondNameAr,
                ThirdNameAr = _patient.ThirdNameAr,
                FourthNameAr = _patient.FourthNameAr,
                PersonalId = _patient.PersonalId,
                DateOfBirth = _patient.DateOfBirth,
                Gender = _patient.Gender,
                AgeYears = _patient.AgeYears,
                TitleId = _patient.TitleId,
                Mobile = _patient.Mobile,
                Mobile2 = _patient.Mobile2,
                Email = _patient.Email,
                Occupation = _patient.Occupation,
                AddressDescription = _patient.AddressDescription,
                MaritalStatusId = _patient.MaritalStatusId,
                ChildrenNumber = _patient.ChildrenNumber,
                NationalityId = _patient.NationalityId,
                Country = _patient.Country,
                City = _patient.City,
                Region = _patient.Region,
                FinancialType = _patient.FinancialType
            };
            if (errors.Any())
                    throw new Exception(string.Join(", ", errors));
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return new PatientsDTO
            {
                Id = patient.Id,
                VisitTypeId = patient.VisitTypeId,
                BranchId = patient.BranchId,
                Status = patient.Status,
                IsBlackListed = patient.IsBlackListed,
                PatientStatus = patient.PatientStatus,
                FirstNameEn = patient.FirstNameEn,
                SecondNameEn = patient.SecondNameEn,
                ThirdNameEn = patient.ThirdNameEn,
                FourthNameEn = patient.FourthNameEn,
                FirstNameAr = patient.FirstNameAr,
                SecondNameAr = patient.SecondNameAr,
                ThirdNameAr = patient.ThirdNameAr,
                FourthNameAr = patient.FourthNameAr,
                PersonalId = patient.PersonalId,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                FinancialType = patient.FinancialType,
                AgeYears = patient.AgeYears,
                TitleId = patient.TitleId,
                Mobile = patient.Mobile,
                Mobile2 = patient.Mobile2,
                Email = patient.Email,
                Occupation = patient.Occupation,
                ChildrenNumber = patient.ChildrenNumber,
                Country = patient.Country,
                City = patient.City,
                Region = patient.Region,
                MaritalStatusId=patient.MaritalStatusId,
                NationalityId = patient.NationalityId,
                Title= (int)patient.TitleId,
                MedicalEntity= patient.BranchId,

            };
            
        }

        public async Task DeletePatientAsnc(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) 
                throw new Exception("Patient not found");
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<PagePagination<Patient>> GetAllPagedPatientsAsync(int current )
        {
            int pageSize = 1;
            return await PagePagination<Patient>.GetPagintaionPatient(
                _context.Patients,
                current,
                pageSize);
        }

        public async Task<CursorPagination<PatientsDTO>> GetAllcursorPatientsAsync(int cursor)
        {
            int pageSize = 1;
            var result = await CursorPagination<Patient>.GetCursorPaginationPatient(
                _context.Patients.OrderBy(p=>p.Id), pageSize, cursor);

            return result;
        }

        public async Task<PatientListDTO> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
               .Include(p => p.VisitType)
               .Include(p => p.Nationality)
               .Include(p => p.MaritalStatus)
               .Include(p => p.Title)
               .Where(p => p.Id == id)
               .Select(p => new PatientListDTO
               {
                   Id = p.Id,
                   FullNameEn = p.FirstNameEn + ' ' + p.SecondNameEn + ' ' + p.ThirdNameEn + p.FourthNameEn,
                   Branch = p.Branch.EnName,
                   VisitType= p.VisitType.ErVisitName,
                   Address = p.City + ' ' + p.Region + ' ' + p.Country,
                   Identification = p.Nationality.NationalityId
               }).FirstOrDefaultAsync();

            return patient;
        }

        public async Task<PatientsDTO> UpdatePatientAsync(int id, Patient _patient)
        {
            var patient = await _context.Patients
                .Include(p => p.Branch)
                .Include(p => p.VisitType)
                .Include(p => p.MaritalStatus)
                .Include(p => p.Nationality)
                .Include(p => p.Title)
                .FirstOrDefaultAsync(p => p.Id == id);


            var rules =await  _validation.getValidatVisit(patient.VisitTypeId, patient.BranchId);
            var validation = _validation.Validat(patient, rules);

            if (patient == null)
                throw new Exception("Patient not found");

            // Update Patient properties
            patient.VisitTypeId = _patient.VisitTypeId;
            patient.BranchId = _patient.BranchId;
            patient.Status = _patient.Status;
            patient.IsBlackListed = _patient.IsBlackListed;
            patient.PatientStatus = _patient.PatientStatus;

            patient.FirstNameEn = _patient.FirstNameEn;
            patient.SecondNameEn = _patient.SecondNameEn;
            patient.ThirdNameEn = _patient.ThirdNameEn;
            patient.FourthNameEn = _patient.FourthNameEn;

            patient.FirstNameAr = _patient.FirstNameAr;
            patient.SecondNameAr = _patient.SecondNameAr;
            patient.ThirdNameAr = _patient.ThirdNameAr;
            patient.FourthNameAr = _patient.FourthNameAr;

            patient.PersonalId = _patient.PersonalId;
            patient.DateOfBirth = _patient.DateOfBirth;
            patient.Gender = _patient.Gender;
            patient.AgeYears = _patient.AgeYears;

            patient.TitleId = _patient.TitleId;
            patient.Mobile = _patient.Mobile;
            patient.Mobile2 = _patient.Mobile2;
            patient.Email = _patient.Email;
            patient.Occupation = _patient.Occupation;
            patient.MaritalStatusId = _patient.MaritalStatusId;
            patient.ChildrenNumber = _patient.ChildrenNumber;
            patient.NationalityId = _patient.NationalityId;

            patient.Country = _patient.Country;
            patient.City = _patient.City;
            patient.Region = _patient.Region;
            patient.AddressDescription = _patient.AddressDescription;

            patient.FinancialType = _patient.FinancialType ?? patient.FinancialType;


            if (validation.Any()) 
                throw new Exception(string.Join(", ", validation));
          

            await _context.SaveChangesAsync();


            return new PatientsDTO
            {
                Id = patient.Id,
                VisitTypeId = patient.VisitTypeId,
                BranchId = patient.BranchId,
                Status = patient.Status,
                IsBlackListed = patient.IsBlackListed,
                PatientStatus = patient.PatientStatus,
                FirstNameEn = patient.FirstNameEn,
                SecondNameEn = patient.SecondNameEn,
                ThirdNameEn = patient.ThirdNameEn,
                FourthNameEn = patient.FourthNameEn,
                FirstNameAr = patient.FirstNameAr,
                SecondNameAr = patient.SecondNameAr,
                ThirdNameAr = patient.ThirdNameAr,
                FourthNameAr = patient.FourthNameAr,
                PersonalId = patient.PersonalId,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                AgeYears = patient.AgeYears,
                TitleId = patient.TitleId,
                Mobile = patient.Mobile,
                Mobile2 = patient.Mobile2,
                Email = patient.Email,
                Occupation = patient.Occupation,
                MaritalStatusId = patient.MaritalStatusId,
                ChildrenNumber = patient.ChildrenNumber,
                NationalityId = patient.NationalityId,
                Country = patient.Country,
                City = patient.City,
                Region = patient.Region,
                AddressDescription = patient.AddressDescription,
                HowDidYouKnowUsId = patient.HowDidYouKnowUsId,
                ReferringEntityId = patient.ReferringEntityId,
                CampaignContact = patient.CampaignContact,
                FinancialType = patient.FinancialType,

               
                MedicalEntity = patient.Branch.Id,
                VisitType = patient.VisitType.Id,
                Title = patient.Title.Id,
                MaritalStatus = patient.MaritalStatus.MaritalStatusId,
                Nationality = patient.Nationality.NationalityId
            };
        }
    }
}
