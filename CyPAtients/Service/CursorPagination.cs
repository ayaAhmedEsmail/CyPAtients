using CyPatients.DTO;
using CyPatients.Models;
using Microsoft.EntityFrameworkCore;

namespace CyPatients.Service
{
    public class CursorPagination<T>
    {
        public IEnumerable<T> patients { get; set; }
        public int? nextCursor { set; get; }

        public CursorPagination(IEnumerable<T> _patients,  int?  _nextCursor)
        {
            patients = _patients;
            nextCursor = _nextCursor;
        }
        public static async Task<CursorPagination<PatientsDTO>> GetCursorPaginationPatient(IQueryable<Patient> patients, int pageSize, int? cursor)
        {
            if (cursor.HasValue)
                patients = patients.Where(p => p.Id > cursor.Value);


            var result = await patients.OrderBy(p => p.Id).Select(patient => new PatientsDTO {
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

            }).Take(pageSize).ToListAsync();

            int? nextCursor = result.Any() ? result.Last().Id : (int?)null;

            return new CursorPagination<PatientsDTO>( result, nextCursor);
        }
    }
}
