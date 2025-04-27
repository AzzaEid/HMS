using HMS.Data.Entities.Enums;

namespace HMS.Core.Features.Patients.Queries.Results
{
    public class GetPatientPaginatedListResponse
    {
        public int PatientID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public GetPatientPaginatedListResponse(int patientID, string name, int age, Gender gender, string contactNumber, string address)
        {
            PatientID = patientID;
            Name = name;
            Age = age;
            Gender = gender;
            ContactNumber = contactNumber;
            Address = address;
        }
    }
}
