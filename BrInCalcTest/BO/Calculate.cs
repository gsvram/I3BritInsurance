using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrInCalcTest.BO.Interface;
using BrInCalcTest.Models;

namespace BrInCalcTest.BO
{
    public class Calculate : ICalculate
    {
        
       ///Operator Method

        public double Operator(string operatorType, double dValue,double result)
        {
            switch (operatorType?.ToLower())
            {
                case "add":
                    result = Add(result, dValue);
                    break;
                case "subtract":
                    result = Sub(result, dValue);
                    break;
                case "multiply":
                    result = Mul(result, dValue);
                    break;
                case "divide":
                    result = Div(result, dValue);
                    break;
                default:
                    break;
            }

            return result;

        }

        private double Add(double dOne, double dTwo)
        {
            return dOne + dTwo;
        }

        private double Sub(double dOne, double dTwo)
        {
            return dOne - dTwo;
        }

        private double Mul(double dOne, double dTwo)
        {
            return dOne * dTwo;
        }

        private double Div(double dOne, double dTwo)
        {
            return dOne / dTwo;
        }
    }


}
