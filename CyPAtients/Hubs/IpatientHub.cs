namespace CyPatients.Hubs
{
    public interface IpatientHub
    {
        Task PatientRegisterd(string msg);
        Task PatientUpdated(string msg);
        Task PatientDeleted(string msg);


    }
}
