using System;

namespace ECS.Domain
{
    public static class Domain
    {
        public static Guid NewId() => Guid.NewGuid();

        public static class Roles
        {
            public const string Maintenance = "Maintenance";
            public const string DepotWorker = "DepotWorker";
            public const string Management = "Management";
        }
    }
}
