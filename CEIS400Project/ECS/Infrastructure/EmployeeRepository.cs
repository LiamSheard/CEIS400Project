
using ECS.Domain; 

namespace ECS.Infrastructure
{
    public class EmployeeRepository
    {
        private class EmployeeRecord
        {
            public Employee Employee { get; set; } = default!;
            public string Username { get; set; } = default!;
            public string Password { get; set; } = default!;

            public EmployeeRecord(Employee emp, string user, string pass)
            {
                Employee = emp;
                Username = user;
                Password = pass;

            }
        }
        // The List of internal example records
        private readonly List<EmployeeRecord> testDB = [];

        //Method for matching Employee Username, search DB, and return Emp record if found. 
        internal string? CheckEmployeePassword(string username)
        {
            var record = testDB.FirstOrDefault(x => x.Username == username);
            var pass = record?.Password;
            return pass;
        }


    }
}
