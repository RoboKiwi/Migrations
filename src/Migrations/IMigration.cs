using System.Threading.Tasks;

namespace Migrations
{
    public interface IMigration
    {
        Task Up();

        Task Down();
    }
}