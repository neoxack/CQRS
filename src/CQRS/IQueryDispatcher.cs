namespace CQRS
{
    public interface IQueryDispatcher
    {
        TResult Ask<TResult>(IQuery<TResult> query);
    }
}
