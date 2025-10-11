using System;

namespace ECS.Domain
{
    public class CheckoutRecord
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid EquipmentId { get; set; }

        public DateTime CheckedOutAtUtc { get; set; }
        public DateTime? DueAtUtc { get; set; }
        public DateTime? ReturnedAtUtc { get; set; }

        public bool IsReturned => ReturnedAtUtc.HasValue;

        public CheckoutRecord() { }

        public CheckoutRecord(Guid id, Guid employeeId, Guid equipmentId, DateTime checkedOutAtUtc, DateTime? dueAtUtc = null)
        {
            Id = id;
            EmployeeId = employeeId;
            EquipmentId = equipmentId;
            CheckedOutAtUtc = checkedOutAtUtc;
            DueAtUtc = dueAtUtc;
        }

        public void MarkReturned(DateTime whenUtc)
        {
            ReturnedAtUtc = whenUtc;
        }
    }
}
