namespace Transaction
{
    using System;

    using TransactionLib;
    using TransactionLib.Operations;

    public class Operation1 : Operation<int, int, ErrorInfoImpl>
    {
        private static int operation = 0;

        public override int Execute(int input, out ErrorInfoImpl errorInfo)
        {
            errorInfo = new ErrorInfoImpl();
            ++operation;
            if (operation == 10)
            {
                //throw new Exception();
            }

            Console.WriteLine("Execute: " + operation);
            return operation;
        }

        public override void Rollback(ErrorInfoImpl errorInfo)
        {
            --operation;
            if (operation == 4)
            {
                //throw new Exception();
            }

            Console.WriteLine("Rollback: " + operation);
        }
    }
}