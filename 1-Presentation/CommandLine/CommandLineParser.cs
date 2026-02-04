using Common.Core.DependencyInjection;
using Core.Enum;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using RedisCtl.CommandLine.Commands;
using System.CommandLine;

namespace RedisCtl.CommandLine
{
    [ServiceLocate(default)]
    public class CommandLineParser
    {
        private readonly IEnumerable<ICommandDefinition> _commandDefinitions;

        private Option<string> _hostOption;
        private Option<string> _accesskeyOption;

        public CommandLineParser(IEnumerable<ICommandDefinition> commandDefinitions)
        {
            _commandDefinitions = commandDefinitions;

            _hostOption = new Option<string>(SharedOption.Host, SharedOption.HostShort)
            {
                Required = true,
                Description = "Redis Host with Port, example: localhost:6380, enable ssl always."
            };
            _accesskeyOption = new Option<string>(SharedOption.AccessKey, SharedOption.AccessKeyShort)
            {
                Required = true,
                Description = "Redis Access Key"
            };
        }

        public ParseResult Parse(string[] args)
        {
            // Define commands
            var rootCommand = new RootCommand("Manage Redis");

            foreach (var commandDefinition in _commandDefinitions)
            {
                var subCommand = commandDefinition.Create();
                subCommand.Options.Add(_hostOption);
                subCommand.Options.Add(_accesskeyOption);

                rootCommand.Subcommands.Add(subCommand);
            }

            var parseResult = rootCommand.Parse(args);

            return parseResult;
        }
    }
}
