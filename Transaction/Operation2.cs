namespace Transaction
{
    using System;

    using TransactionLib;
    using TransactionLib.Operations;

    public class Operation2 : Operation<int, int, ErrorInfoImpl>
    {
        public override int Execute(int input, out ErrorInfoImpl errorInfo)
        {
            errorInfo = new ErrorInfoImpl();
            Console.WriteLine("Operation2 Execute.");
            throw new Exception("Operation2 Exception");
        }

        public override void Rollback(ErrorInfoImpl errorInfo)
        {
            Console.WriteLine("Operation2 Rollback.");
        }
    }
}