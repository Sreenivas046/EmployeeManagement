namespace EmployeeManagement.API.Exception_Classes
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
