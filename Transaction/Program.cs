namespace Transaction
{
    using System;

    using TransactionLib;

    public class Program
    {
        public static void Main()
        {
            var op1 = new Operation1();
            var t = TransactionQueue.First(op1)
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
}
