using ECS.Infrastructure; // for referencing Employee records stored in local memory
using ECS.Domain; // for Employee classes. 

namespace ECS.Application
{
    public class AuthService
    {
        private readonly EmployeeRepository repoRO; // just for checking entries and ID, not for making changes 

        public AuthService (EmployeeRepository employeeRepository)
        {
            repoRO = employeeRepository; 
        }

        // login method 
        public (bool Valid, Guid? EmployeeId , string Message) Login(string username, string password)
        {
            // quick input valid - check for empty field
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
             return (false, null, "Missing username or password"); 

            //clean whitespaces just to be safe...
            username = username.Trim();
            password = password.Trim();

            //lookup reference in EmployeeRepo... Should have User and Password, along with ID, and other Emp info. 
            var empRefPW = repoRO.CheckEmployeePassword(username);

            //Check Username is correct based on lookup - if didn't find, must not be right or doesn't exist. 
            if (empRefPW is null) 
                return (false, null, "Invalid username or password");
            
            // Check employees password is correct for the emp record retrieved from DB. 
            bool matches = string.Equals(empRefPW, password, StringComparison.Ordinal);
            if (!matches)
                return (false, null, "Invalid Username or Password");
            //if Username works, and Password matches, we have valid login!
            return (true, null, "Login Sucessfull");





        }


    }
}
