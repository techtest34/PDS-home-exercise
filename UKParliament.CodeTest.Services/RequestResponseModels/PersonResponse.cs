namespace UKParliament.CodeTest.Services.RequestResponseModels;

public class PersonResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int DepartmentId { get; set; }
}