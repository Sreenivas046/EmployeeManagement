namespace EmployeeManagement.API.Exceptions
{
    public class ExcuitionException:Exception
    {
        public ExcuitionException(string message ="An error occured during the excution of the request") : base(message)
        {

        }
    }
}
