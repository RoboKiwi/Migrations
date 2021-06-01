using System;

namespace Migrations.Container
{
    public class ContainerRegistration
    {
        public Lifestyle Lifestyle { get; set; }

        public Type ServiceType { get; set; }

        public Type ImplementationType { get; set; }

        public object Instance { get; set; }
    }
}