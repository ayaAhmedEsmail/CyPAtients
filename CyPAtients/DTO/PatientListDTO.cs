namespace CyPatients.DTO
{
    public class PatientListDTO
    {
            public int Id { get; set; }
            public string FullNameEn { get; set; }
            public int Identification { get; set; }
            public int Phone { get; set; }

            public string Address { get; set; }
            public string ? VisitType { get; set; }
            public string Branch { get; set; }
            public bool Status { get; set; }
            public bool IsBlackListed { get; set; }
            public string MaritalStatus { get; set; }
            public string Nationality { get; set; }
            public string Title { get; set; }

    }
}
