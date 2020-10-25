using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrInCalcTest.BO.Interface;
using BrInCalcTest.Models;

namespace BrInCalcTest.BO
{
    public class ProcessFile : IProcessFile
    {


        public string[] GetLinesFromFile(string content)
        {

            return content?.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            )?.ToArray();

        }


        public List<FileVariables> SplitOperatorsandValues(string[] sLines)
        {
            List<FileVariables> fileVariable = new List<FileVariables>();
            foreach (string sLine in sLines)
            {
                var sOpeDevalue = sLine.Split(new[] { ' ' }, 2);
                var strOne=String.Empty;
                var strTwo=string.Empty;
                if (sOpeDevalue.Length > 0)
                    strOne = sOpeDevalue[0];
                if(sOpeDevalue.Length > 1)
                    strTwo = sOpeDevalue[1];
                var rw = new FileVariables(strOne, strTwo);
                    bool val = rw.IsSecondVariableInt;
                    fileVariable.Add(rw);
              
            }

            return fileVariable;
        }
    }
}