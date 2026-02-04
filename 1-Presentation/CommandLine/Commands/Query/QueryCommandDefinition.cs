using Application.Services.Queries;
using Common.Core.DependencyInjection;
using Core.Enum;
using System.CommandLine;
using System.Diagnostics;

namespace RedisCtl.CommandLine.Commands.Query
{
    [ServiceLocate(typeof(ICommandDefinition))]
    public class QueryCommandDefinition : ICommandDefinition
    {
        private readonly QueryService _queryService;

        private Option<string> _patternOption;

        public QueryCommandDefinition(QueryService queryService)
        {
            _queryService = queryService;

            _patternOption = new Option<string>(name: "--pattern", "-p")
            {
                Required = true,
                Description = "Pattern to match keys, example: user:*"
            };
        }

        public Command Create()
        {
            var queryCommand = new Command("Query", "operation on Redis")
            {
                _patternOption
            };

            queryCommand.SetAction(DoAction);

            return queryCommand;
        }

        private async Task DoAction(ParseResult parseResult, CancellationToken token)
        {
            var host = parseResult.GetValue<string>(SharedOption.Host) ?? parseResult.GetValue<string>(SharedOption.HostShort);
            var accessKey = parseResult.GetValue<string>(SharedOption.AccessKey) ?? parseResult.GetValue<string>(SharedOption.AccessKeyShort);
            var pattern = parseResult.GetRequiredValue(_patternOption);

            Debug.Assert(!string.IsNullOrEmpty(host));
            Debug.Assert(!string.IsNullOrEmpty(accessKey));

            var count = await _queryService.Query(pattern, host, accessKey, token).ConfigureAwait(false);

            Console.WriteLine($"Total keys matched pattern '{pattern}': {count}");
        }
    }
}
