using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication3.Services
{
    public interface ICalculatorServices
    {
        int Calculate(int a, int b, string operation);
    }

    public class CalculatorServices : ICalculatorServices
    {
        private readonly ICalculationEngine _calculationEngine;
        public CalculatorServices(ICalculationEngine calculationEngine)
        {
            _calculationEngine = calculationEngine;
        }

        public int Calculate(int a, int b, string operation)
        {
            switch (operation)
            {
                case "+":
                    return _calculationEngine.Add(a, b);
                case "-":
                    return _calculationEngine.Substract(a, b);
                case "*":
                    return _calculationEngine.Multiply(a, b);
                case "/":
                    return _calculationEngine.Divide(a, b);
                case "add":
                    return _calculationEngine.Add(a, b);
                case "sub":
                    return _calculationEngine.Substract(a, b);
                case "mul":
                    return _calculationEngine.Multiply(a, b);
                case "div":
                    return _calculationEngine.Divide(a, b);
                default:
                    var msg = $"Operation '{operation}' not supported";
                    throw new ArgumentOutOfRangeException(nameof(operation), msg);
            }
        }
    }
}
