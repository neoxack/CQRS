namespace CQRS.Tests.Utils;

public class PingQuery : IQuery<string>
{
    public string Value { get; }

    public PingQuery(string value)
    {
        Value = value;
    }
}

public class PingQueryHandler : IQueryHandler<PingQuery, string>
{
    public string Ask(PingQuery query)
    {
        return query.Value;
    }
}