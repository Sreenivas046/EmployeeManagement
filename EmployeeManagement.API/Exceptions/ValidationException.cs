namespace EmployeeManagement.API.Exception_Classes
{
    public class ValidationException : Exception
    {
        public ValidationException(string message = "Validation failed while fetching Employees ") : base(message) { }
    }
}
