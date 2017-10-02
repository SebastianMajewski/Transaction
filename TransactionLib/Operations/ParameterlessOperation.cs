namespace TransactionLib.Operations
{
    public abstract class ParameterlessOperation<TOutput, TErrorInfo>
        where TErrorInfo : ErrorInfo
    {
        public abstract TOutput Execute(out TErrorInfo errorInfo);

        public abstract void Rollback(TErrorInfo errorInfo);
    }
}
