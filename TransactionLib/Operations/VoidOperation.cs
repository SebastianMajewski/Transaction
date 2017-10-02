namespace TransactionLib.Operations
{
    public abstract class VoidOperation<TInput, TErrorInfo>
        where TErrorInfo : ErrorInfo
    {
        public abstract void Execute(TInput input, out TErrorInfo errorInfo);

        public abstract void Rollback(TErrorInfo errorInfo);
    }
}
