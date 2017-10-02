namespace TransactionLib.Operations
{
    public abstract class ParameterlessVoidOperation<TErrorInfo>
        where TErrorInfo : ErrorInfo
    {
        public abstract void Execute(out TErrorInfo errorInfo);

        public abstract void Rollback(TErrorInfo errorInfo);
    }
}
