using System;

namespace ECS.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Role { get; set; } = "DepotWorker";
        public bool IsActive { get; set; }  = true;

        public Employee() { }

        public Employee(Guid id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
