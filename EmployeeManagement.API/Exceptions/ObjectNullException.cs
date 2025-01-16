namespace EmployeeManagement.API.Exceptions
{
    public class ObjectNullException : Exception
    {
        public ObjectNullException(string message = "Object should not be null") : base(message)
        {
                
        }
    }
}
