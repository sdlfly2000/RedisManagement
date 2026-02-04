using Common.Core.DependencyInjection;
using RedisCtl.CommandLine;

namespace RedisCtl
{
    [ServiceLocate(default)]
    public class Worker
    {
        private readonly CommandLineParser _parser;

        public Worker(CommandLineParser parser)
        {
            _parser = parser;
        }

        public async Task Execute(string[] args)
        {
            var parseResult = _parser.Parse(args);

            await parseResult.InvokeAsync();
        }
    }
}
