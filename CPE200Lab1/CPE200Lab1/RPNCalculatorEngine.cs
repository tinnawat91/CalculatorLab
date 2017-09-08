using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class RPNCalculatorEngine:CalculatorEngine
    {
        public string Process(string str)
        {
             string secondOperand;
             string firstOperand;
             string operand;
             string result;
            Stack myStack = new Stack();
            List<string> parts = str.Split(' ').ToList<string>();
            for (int i = 0; i < parts.Count; i++)
            {
                if (isNumber(parts[i]))
                {
                    myStack.Push(parts[i]);
                }
                else if (isOperator(parts[i]))
                {
                    operand = parts[i];
                    secondOperand=myStack.Pop().ToString();
                    firstOperand = myStack.Pop().ToString();
                    result = calculate(operand, firstOperand, secondOperand, 4);
                    myStack.Push(result);
                    
                }
            }
            return myStack.Pop().ToString();

        }
    }
}
