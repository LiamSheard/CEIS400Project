using ECS.Domain;

namespace ECS.Infrastructure
{
    public class EquipmentRepository
    {
        // Internal in-memory storage for equipment records
        private readonly List<Equipment> equipmentList = [];

        // Add a new equipment record
        public void Add(Equipment equipment)
        {
            equipmentList.Add(equipment);
        }

        // Get all equipment records
        public IEnumerable<Equipment> GetAll()
        {
            return equipmentList;
        }

        // Get an equipment record by its ID
        public Equipment? GetById(Guid id)
        {
            return equipmentList.FirstOrDefault(e => e.Id == id);
        }

        // Remove an equipment record by its ID
        public bool Remove(Guid id)
        {
            var equipment = GetById(id);
            if (equipment == null)
                return false;

            equipmentList.Remove(equipment);
            return true;
        }
    }
}
