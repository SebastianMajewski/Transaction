namespace TransactionLib.Operations
{
    public abstract class Operation<TInput, TOutput, TErrorInfo>
        where TErrorInfo : ErrorInfo
    {
        public abstract TOutput Execute(TInput input, out TErrorInfo errorInfo);

        public abstract void Rollback(TErrorInfo errorInfo);
    }
}
