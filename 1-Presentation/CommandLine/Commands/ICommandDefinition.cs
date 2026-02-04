using System.CommandLine;

namespace RedisCtl.CommandLine.Commands
{
    public interface ICommandDefinition
    {
        public Command Create();
    }
}
