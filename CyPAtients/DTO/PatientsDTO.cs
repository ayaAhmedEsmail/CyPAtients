namespace CyPatients.DTO
{
    public class PatientsDTO
    {
        public int Id { get; set; }

        public int VisitTypeId { get; set; }

        public int BranchId { get; set; }

        public bool Status { get; set; }

        public bool IsBlackListed { get; set; }

        public bool PatientStatus { get; set; }

        public string FirstNameEn { get; set; }

        public string SecondNameEn { get; set; }

        public string ThirdNameEn { get; set; }

        public string FourthNameEn { get; set; }

        public string FirstNameAr { get; set; }

        public string SecondNameAr { get; set; }

        public string ThirdNameAr { get; set; }

        public string FourthNameAr { get; set; }

        public string PersonalId { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public int? Gender { get; set; }

        public int? AgeYears { get; set; }

        public int? TitleId { get; set; }

        public string Mobile { get; set; }

        public string Mobile2 { get; set; }

        public string Email { get; set; }

        public string Occupation { get; set; }

        public int? MaritalStatusId { get; set; }

        public int? ChildrenNumber { get; set; }

        public int? NationalityId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string AddressDescription { get; set; }

        public int? HowDidYouKnowUsId { get; set; }

        public int? ReferringEntityId { get; set; }

        public int? CampaignContact { get; set; }

        public int? FinancialType { get; set; }

        public  int? MedicalEntity  { get; set; }

        public int? MaritalStatus { get; set; }

        public int? Nationality { get; set; }
        public int? VisitType { get; set; }

        public int? Title { get; set; }

    }
}
