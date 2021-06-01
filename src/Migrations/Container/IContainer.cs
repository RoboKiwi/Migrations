using System;
using System.Collections.Generic;

namespace Migrations.Container
{
    public interface IContainer : IDisposable
    {
        object Resolve(Type type);

        IEnumerable<object> ResolveAll(Type type);

        void Register(ContainerRegistration registration);
    }
}