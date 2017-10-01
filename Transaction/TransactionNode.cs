namespace Transaction
{
    using System;

    public class TransactionNode<TInput, TOutput, TParentOutput, TErrorInfo>
        where TErrorInfo : ErrorInfo, new()
    {
        protected TransactionNode(Operation<TParentOutput, TOutput, TErrorInfo> operation)
        {
            this.operation = operation;
        }

        protected void SetParent<TAny, TAny2>(TransactionNode<TInput, TParentOutput, TAny, TAny2> parent)
            where TAny2 : ErrorInfo, new()
        {
            this.parent = parent;
        }

        protected Operation<TParentOutput, TOutput, TErrorInfo> operation;
        protected dynamic parent;
        protected TErrorInfo errorInfo = null;

        public virtual TOutput Execute(TInput input)
        {
            try
            {
                return this.operation.Execute(this.parent.Execute(input), out errorInfo);
            }
            catch (Exception e)
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

        protected void Rollback()
        {
            try
            {
                this.operation.Rollback(errorInfo);
            }
            catch(Exception e)
            {
                throw new TransactionException(e) { RollbackException = true };
            }
            this.parent?.Rollback();
        }

        public TransactionNode<TInput, TNewOutput, TOutput, TNewErrorInfo> Next<TNewOutput, TNewErrorInfo>(Operation<TOutput, TNewOutput, TNewErrorInfo> operation)
            where TNewErrorInfo : ErrorInfo, new()
        {
            var tn = new TransactionNode<TInput, TNewOutput, TOutput, TNewErrorInfo>(operation);
            tn.SetParent(this);
            return tn;
        }
    }
}
