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
        private double _result = 0;
        public FileDetails CalculateResults(FileDetails fileDetails)
        {
            if (fileDetails == null) return null;
            _result = fileDetails?.DApplyValue ?? 0;
            var listRw = fileDetails?.AllFileVariables?.Where(x => x.First.ToLower() != "apply");
            if (listRw == null) fileDetails.DResults = 0;
            if (listRw != null)
            {
                foreach (var rw in listRw)
                {
                    fileDetails.DResults = Operator(rw.First, rw.DValue);
                }
            }

            return fileDetails;
        }

        public double Operator(string operatorType, double dValue)
        {
            switch (operatorType)
            {
                case "add":
                    _result = Add(_result, dValue);
                    break;
                case "subtract":
                    _result = Sub(_result, dValue);
                    break;
                case "multiply":
                    _result = Mul(_result, dValue);
                    break;
                case "divide":
                    _result = Div(_result, dValue);
                    break;
                default:
                    break;
            }

            return _result;

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