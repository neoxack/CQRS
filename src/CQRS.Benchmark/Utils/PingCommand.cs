namespace CQRS.Benchmark.Utils;

public class PingCommand : ICommand<string>
{
    public string Value { get; }

    public PingCommand(string value)
    {
        Value = value;
    }
}

public class PingCommandHandler : ICommandHandler<PingCommand, string>
{
    public string Handle(PingCommand command)
    {
        return command.Value;
    }
}