namespace CQRS
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Ask(TQuery query);
    }
}
