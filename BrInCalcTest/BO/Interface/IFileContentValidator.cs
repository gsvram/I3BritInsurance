using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrInCalcTest.Models;

namespace BrInCalcTest.BO.Interface
{
    public interface IFileContentValidator
    {

        bool ValidateIsApplyExists(FileDetails fd);
        bool ValidateOperatorsValid(List<FileVariables> fv);
        bool ValidateTotalLinesGreaterThanTwo(List<FileVariables> fv);
        bool ValidateAllSecondPartCanConvertToInt(List<FileVariables> fv);

        bool ValidateApplyShouldBeLast(string[] lines);
    }
} 