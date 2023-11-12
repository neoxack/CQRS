namespace CQRS
{
    public interface ICommandDispatcher
    {
        TResult Execute<TResult>(ICommand<TResult> command);
    }
}
