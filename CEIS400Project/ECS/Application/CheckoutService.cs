using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS.Domain.Services
{
    public class CheckoutRecord
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime CheckedOutAtUtc { get; set; }
        public DateTime? ReturnedAtUtc { get; set; }   // used to see if it’s still out
    }

    public class Equipment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsCheckedOut { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class InMemoryCheckoutRepo
    {
        private readonly List<CheckoutRecord> _records = new();
        private int _nextId = 1;

        public CheckoutRecord Add(CheckoutRecord record)
        {
            record.Id = _nextId++;
            _records.Add(record);
            return record;
        }

        public CheckoutRecord? GetActiveByEquipment(int equipmentId)
        {
            return _records.FirstOrDefault(r => r.EquipmentId == equipmentId && r.ReturnedAtUtc == null);
        }

        public void Update(CheckoutRecord record)
        {
            var idx = _records.FindIndex(r => r.Id == record.Id);
            if (idx >= 0) _records[idx] = record;
        }

        public IEnumerable<CheckoutRecord> GetAll() => _records;
    }

    public class CheckoutService
    {
        private readonly InMemoryCheckoutRepo _repo;
        private readonly List<Equipment> _equipment;
        private readonly List<Employee> _employees;

        public CheckoutService(InMemoryCheckoutRepo repo, List<Employee> employees, List<Equipment> equipment)
        {
            _repo = repo;
            _equipment = equipment;
            _employees = employees;
        }

        public CheckoutRecord Checkout(int employeeId, int equipmentId)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == employeeId)
                ?? throw new InvalidOperationException("Employee not found.");

            var eq = _equipment.FirstOrDefault(e => e.Id == equipmentId)
                ?? throw new InvalidOperationException("Equipment not found.");

            if (eq.IsCheckedOut)
                throw new InvalidOperationException("Equipment already checked out.");

            var record = new CheckoutRecord
            {
                EmployeeId = employeeId,
                EquipmentId = equipmentId,
                CheckedOutAtUtc = DateTime.UtcNow,
                ReturnedAtUtc = null
            };

            _repo.Add(record);
            eq.IsCheckedOut = true;
            return record;
        }

        public void Return(int equipmentId)
        {
            var active = _repo.GetActiveByEquipment(equipmentId);
            if (active == null) return;

            active.ReturnedAtUtc = DateTime.UtcNow;
            _repo.Update(active);

            var eq = _equipment.FirstOrDefault(e => e.Id == equipmentId);
            if (eq != null) eq.IsCheckedOut = false;
        }

        public bool IsCheckedOut(int equipmentId)
        {
            var active = _repo.GetActiveByEquipment(equipmentId);
            return active != null;
        }
    }
}
