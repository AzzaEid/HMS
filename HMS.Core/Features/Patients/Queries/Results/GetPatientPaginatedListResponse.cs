namespace HMS.Core.Features.Patients.Queries.Results
{
    public class GetPatientPaginatedListResponse
    {
        public int PatientID { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public GetPatientPaginatedListResponse(int patientID, string name, string address)
        {
            PatientID = patientID;
            Name = name;
            Address = address;
        }
    }
}
