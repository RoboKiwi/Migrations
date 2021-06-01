using System;
using System.Collections.Generic;
using System.Linq;

namespace Migrations.Container
{
    public static class ContainerExtensions
    {
        public static T Resolve<T>(this IContainer container)
        {
            return (T) container.Resolve(typeof(T));
        }

        public static IEnumerable<T> ResolveAll<T>(this IContainer container)
        {
            return container.ResolveAll(typeof(T)).Cast<T>();
        }

        public static void Register(this IContainer container, Type type)
        {
            var registration = new ContainerRegistration
            {
                ImplementationType = type,
                ServiceType = type
            };

            container.Register(registration);
        }

        public static void Register(this IContainer container, object instance)
        {
            var registration = new ContainerRegistration
            {
                Instance = instance
            };

            container.Register(registration);
        }

        public static void Register<T>(this IContainer container, object instance)
        {
            var registration = new ContainerRegistration
            {
                Instance = instance,
                ServiceType = typeof(T)
            };

            container.Register(registration);
        }
    }
}