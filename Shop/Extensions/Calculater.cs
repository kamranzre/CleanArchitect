using Azure;
using Core.Enums;
using System.Reflection;

namespace Shop.Extensions
{
    public static class Calculater
    {
        public static long Add(long a , long b) => a + b;

        public static long Subtract(long a , long b) => a - b;

        public static long Multiply(long a , long b) => a * b;

        public static double Divide(long a, long b) => b != 0 ? (double)a / b : throw new DivideByZeroException("Cannot divide by zero");

    }

    public class OperationManager
    {
        public object? ExecuteOperation(byte operation,params object[] parameters)
        {
            OperationType operationType = (OperationType)operation;
            MethodInfo? methodInfo = typeof(Calculater).GetMethod(operationType.ToString());

            return methodInfo?.Invoke(null, parameters);
        }
    }
}
