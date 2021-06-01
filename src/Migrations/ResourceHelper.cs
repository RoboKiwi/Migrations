using System.IO;
using System.Reflection;

namespace Migrations
{
    static class ResourceHelper
    {
        public static Stream GetEmbeddedResource(string name)
        {
            var assembly = Assembly.GetCallingAssembly();
            var stream = assembly.GetManifestResourceStream(name);
            if (stream == null)
                throw new MigrationException(
                    $"Couldn't load resource named '{name}'. Available resource names: {string.Join("\r\n", assembly.GetManifestResourceNames())}");

            return stream;
        }

        public static Stream GetEmbeddedResource(Assembly assembnly, string name)
        {
            var assembly = Assembly.GetCallingAssembly();
            var stream = assembly.GetManifestResourceStream(name);
            if (stream == null)
                throw new MigrationException(
                    $"Couldn't load resource named '{name}'. Available resource names: {string.Join("\r\n", assembly.GetManifestResourceNames())}");

            return stream;
        }
    }
}