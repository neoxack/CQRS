namespace CQRS.Tests.Utils;

public class InternalPingCommand : ICommand<string>
{
    public string Value { get; }

    public InternalPingCommand(string value)
    {
        Value = value;
    }
}

internal class InternalPingCommandHandler : ICommandHandler<InternalPingCommand, string>
{
    public string Handle(InternalPingCommand command)
    {
        return command.Value;
    }
}