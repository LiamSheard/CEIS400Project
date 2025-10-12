using ECS.Domain;

namespace ECS.Infrastructure
{
    public class CheckoutRepository
    {
        // Internal in-memory storage for checkout records
        private readonly List<CheckoutRecord> records = [];

        // Add a new checkout record
        public void Add(CheckoutRecord record)
        {
            records.Add(record);
        }

        // Get all checkout records
        public IEnumerable<CheckoutRecord> GetAll()
        {
            return records;
        }

        // Get a checkout record by its ID
        public CheckoutRecord? GetById(Guid id)
        {
            return records.FirstOrDefault(r => r.Id == id);
        }

        // Get all checkout records for a specific employee
        public IEnumerable<CheckoutRecord> GetByEmployeeId(Guid employeeId)
        {
            return records.Where(r => r.EmployeeId == employeeId);
        }

        // Mark a record as returned
        public bool MarkReturned(Guid id, DateTime returnedAtUtc)
        {
            var record = GetById(id);
            if (record == null || record.IsReturned)
                return false;

            record.MarkReturned(returnedAtUtc);
            return true;
        }
    }
}
