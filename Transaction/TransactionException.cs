namespace Transaction
{
    using System;

    public class TransactionException : Exception
    {
        public TransactionException(Exception e)
            : base("Exception occured while execute transaction." ,e)
        {
        }

        public bool Handled { get; set; }

        public bool RollbackException { get; set; }
    }
}
