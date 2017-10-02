namespace TransactionLib
{
    using System;
    using Operations;

    public class TransactionRoot<TInput, TOutput, TErrorInfo> : TransactionNode<TInput, TOutput, TInput, TErrorInfo>
        where TErrorInfo : ErrorInfo, new()
    {
        protected TransactionRoot(Operation<TInput, TOutput, TErrorInfo> operation) : base(operation)
        {
        }

        public override TOutput Execute(TInput input)
        {
            try
            {
                return this.Operation.Execute(input, out this.errorInfo);
            }
            catch (Exception e)
            {
                var transExc = e as TransactionException;
                if (transExc != null && (transExc.Handled || transExc.RollbackException))
                {
                    throw;
                }
                else
                {
                    if (this.errorInfo == null)
                    {
                        this.errorInfo = new TErrorInfo();
                    }
                    this.errorInfo.Exception = e;
                    this.Rollback();
                    throw new TransactionException(e) { Handled = true };
                }
            }
        }

        internal static TransactionRoot<TI, TO, TEInfo> ForOperation<TI, TO, TEInfo>(Operation<TI, TO, TEInfo> operation)
            where TEInfo : ErrorInfo, new()
        {
            return new TransactionRoot<TI, TO, TEInfo>(operation);
        }
    }
}
