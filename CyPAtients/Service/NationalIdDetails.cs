namespace CyPatients.Service
{
    public class NationalIdDetails
    {

        public static int getYAge(string ID)
        {

            ID = "30105241234567";

            int century = ID[0] == '2' ? 1900 : 2000;
            int year = century + int.Parse(ID.Substring(1,2));
            int month = int.Parse((ID.Substring( 3,2)));
            int day = int.Parse((ID.Substring(5,2)));

            DateTime birthdate = new DateTime(year, month, day);

            Console.WriteLine(birthdate.Year);
            Console.WriteLine(DateTime.Today.Year);
            int age = DateTime.Today.Year - birthdate.Year;
            Console.WriteLine(age);

            return age;
        }
    }
}
