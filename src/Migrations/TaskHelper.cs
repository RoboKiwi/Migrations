using System.Threading.Tasks;

namespace Migrations
{
    public class TaskHelper
    {
        public static readonly Task Completed = Task.FromResult(1);

        public static readonly Task Failed = Task.FromResult(0);

        public static Task<T> NullResult<T>() where T : class
        {
            return Task.FromResult<T>(null);
        }
    }
}