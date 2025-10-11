using System;

namespace ECS.Domain
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsCheckedOut { get; private set; } = false;

        public Equipment() { }

        public Equipment(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public void CheckOut() => IsCheckedOut = true;
        public void ReturnBack() => IsCheckedOut = false;
    }
}
