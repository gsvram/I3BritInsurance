using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrInCalcTest.BO.Interface;
using BrInCalcTest.Models;
using Microsoft.Ajax.Utilities;

namespace BrInCalcTest.BO
{
    public class FileContentValidator :IFileContentValidator
    {

        public string[] opertorsList = { "add","subtract","multiply","divide","apply"};
        public bool ValidateIsApplyExists(FileDetails fd)
        {
            return fd.IsApplyExists;
        }

        public bool ValidateOperatorsValid(List<FileVariables> fv)
        {
            return fv.Count(x => opertorsList.Contains(x.First)) == fv.Count();
        }

        public bool ValidateTotalLinesGreaterThanTwo(List<FileVariables> fv)
        {
           return  fv.Count()>1;
        }

        public bool ValidateAllSecondPartCanConvertToInt(List<FileVariables> fv)
        {
            return fv.Count(x => x.IsSecondVariableInt) == fv.Count();
        }

        public bool ValidateApplyShouldBeLast(string[] lines)
        {
            var removeEmpty= lines.Where(x => !string.IsNullOrEmpty(x)).Select(s=> s.ToLower()).ToArray();
            return removeEmpty[removeEmpty.Length - 1].Contains("apply ") || removeEmpty[removeEmpty.Length - 1].Contains("apply\t");
        }
    }
}