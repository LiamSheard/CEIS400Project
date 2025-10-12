using System.Collections.Generic;
using System.Linq;

namespace ECS.Domain.Services
{
    public class ReportService
    {
        private readonly InMemoryCheckoutRepo _repo;
        private readonly List<Equipment> _equipment;
        private readonly List<Employee> _employees;

        public ReportService(InMemoryCheckoutRepo repo, List<Employee> employees, List<Equipment> equipment)
        {
            _repo = repo;
            _employees = employees;
            _equipment = equipment;
        }

        public IEnumerable<string> GetCheckedOutItems()
        {
            var activeIds = _repo.GetAll()
                .Where(r => r.ReturnedAtUtc == null)
                .Select(r => r.EquipmentId)
                .ToHashSet();

            return _equipment
                .Where(e => activeIds.Contains(e.Id))
                .Select(e => $"{e.Name} (ID: {e.Id}) is checked out");
        }

        public IEnumerable<string> GetAvailableItems()
        {
            var activeIds = _repo.GetAll()
                .Where(r => r.ReturnedAtUtc == null)
                .Select(r => r.EquipmentId)
                .ToHashSet();

            return _equipment
                .Where(e => !activeIds.Contains(e.Id))
                .Select(e => $"{e.Name} (ID: {e.Id}) is available");
        }
    }
}
