using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction
{
    class Program
    {
        static void Main(string[] args)
        {
            var op1 = new Operation1();
            var t = TransactionRoot<double, int, ErrorInfoImpl>.ForFunc(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(op1)
                .Next(new Operation2());
            var r = t.Execute(0);
        }

        public static int Func(double i)
        {
            return (int)Math.Ceiling(++i);
        }

        public static string Func2(int i)
        {
            return i + " Działa";
        }
    }

    public class ErrorInfoImpl : ErrorInfo
    {
    }

    public class Operation1 : Operation<int, int, ErrorInfoImpl>
    {
        static int operation = 0;

        public override int Execute(int input, out ErrorInfoImpl errorInfo)
        {
            errorInfo = new ErrorInfoImpl();
            ++operation;
            if(operation == 10)
            {
                throw new Exception();
            }
            Console.WriteLine("Execute: " + operation);
            return operation;
        }

        public override void Rollback(ErrorInfoImpl errorInfo)
        {
            --operation;
            if (operation == 4)
            {
                throw new Exception();
            }
            Console.WriteLine("Rollback: " + operation);
        }
    }

    public class Operation2 : Operation<int, int, ErrorInfoImpl>
    {
        public override int Execute(int input, out ErrorInfoImpl errorInfo)
        {
            errorInfo = new ErrorInfoImpl();
            Console.WriteLine("Operation2 Execute.");
            throw new Exception();
        }

        public override void Rollback(ErrorInfoImpl errorInfo)
        {
            Console.WriteLine("Operation2 Rollback.");
        }
    }
}
