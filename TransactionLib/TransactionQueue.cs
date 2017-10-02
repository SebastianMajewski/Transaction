namespace TransactionLib
{
    using Operations;

    public class TransactionQueue
    {
        public static TransactionRoot<TInput, TOutput, TErrorInfo> First<TInput, TOutput, TErrorInfo>(
            Operation<TInput, TOutput, TErrorInfo> operation) where TErrorInfo : ErrorInfo, new()
        {
            return TransactionRoot<TInput, TOutput, TErrorInfo>.ForOperation(operation);
        }
    }
}
