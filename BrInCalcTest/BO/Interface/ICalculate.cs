﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrInCalcTest.Models;

namespace BrInCalcTest.BO.Interface
{
    public interface ICalculate
    {
        
        double Operator(string operatorType, double dValue, double result);
    }
}
