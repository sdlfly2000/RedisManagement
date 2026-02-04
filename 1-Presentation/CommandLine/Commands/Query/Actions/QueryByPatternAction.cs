using Common.Core.DependencyInjection;

namespace RedisCtl.CommandLine.Commands.Query.Actions
{
    [ServiceLocate(default)]
    public class QueryByPatternAction
    {

        public QueryByPatternAction()
        {

        }

        public async Task Act(string pattern, CancellationToken token)
        {
        }
    }
}
