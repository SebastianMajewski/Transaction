namespace TransactionLib.Transaction
{
    using System;
    using Operations;

    public class TransactionNode<TInput, TOutput, TParentOutput, TErrorInfo>
        where TErrorInfo : ErrorInfo, new()
    {
        protected TErrorInfo errorInfo = null;

        protected TransactionNode(Operation<TParentOutput, TOutput, TErrorInfo> operation)
        {
            this.Operation = operation;
        }

        protected Operation<TParentOutput, TOutput, TErrorInfo> Operation { get; set; }

        protected dynamic Parent { get; private set; }

        public TransactionNode<TInput, TNewOutput, TOutput, TNewErrorInfo> Then<TNewOutput, TNewErrorInfo>(Operation<TOutput, TNewOutput, TNewErrorInfo> operation)
            where TNewErrorInfo : ErrorInfo, new()
        {
            var tn = new TransactionNode<TInput, TNewOutput, TOutput, TNewErrorInfo>(operation);
            tn.SetParent(this);
            return tn;
        }

        public virtual TOutput Execute(TInput input)
        {
            try
            {
                return this.Operation.Execute(this.Parent.Execute(input), out this.errorInfo);
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

        protected void Rollback()
        {
            try
            {
                this.Operation.Rollback(this.errorInfo);
            }
            catch (Exception e)
            {
                throw new TransactionException(e) { RollbackException = true };
            }

            this.Parent?.Rollback();
        }

        protected void SetParent<TAny, TAny2>(TransactionNode<TInput, TParentOutput, TAny, TAny2> parent)
            where TAny2 : ErrorInfo, new()
        {
            this.Parent = parent;
        }
    }
}
