using System.IO;
using System.Reflection;

namespace Migrations
{
    static class ResourceHelper
    {
        public static Stream GetEmbeddedResource(string name)
        {
            return GetEmbeddedResource(Assembly.GetCallingAssembly(), name);
        }

        public static Stream GetEmbeddedResource(Assembly assembly, string name)
        {
            var stream = assembly.GetManifestResourceStream(name);
            if (stream == null)
                throw new MigrationException(
                    $"Couldn't load resource named '{name}'. Available resource names: {string.Join("\r\n", assembly.GetManifestResourceNames())}");

            return stream;
        }
    }
}