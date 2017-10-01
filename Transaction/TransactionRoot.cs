using System;

namespace Transaction
{
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
                return this.operation.Execute(input, out errorInfo);
            }
            catch(Exception e)
            {
                if (e is TransactionException trExc && (trExc.Handled || trExc.RollbackException))
                {
                    throw;
                }
                else
                {
                    if (errorInfo == null)
                    {
                        errorInfo = new TErrorInfo();
                    }
                    errorInfo.Exception = e;
                    this.Rollback();
                    throw new TransactionException(e) { Handled = true };
                }
            }
        }

        public static TransactionRoot<TI, TO, TEI> ForFunc<TI, TO, TEI>(Operation<TI, TO, TEI> operation)
            where TEI : ErrorInfo, new()
        {
            return new TransactionRoot<TI, TO, TEI>(operation);
        }
    }
}
