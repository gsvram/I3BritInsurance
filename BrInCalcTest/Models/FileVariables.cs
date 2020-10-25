using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace BrInCalcTest.Models
{
    public class FileVariables
    {
        public FileVariables()
        {
        }

        public FileVariables(string first, string second)
        {
            First = first?.Trim()?.ToLower()??string.Empty;
            Second = second?.Trim()?.ToLower()??string.Empty;
        }

        public string First { get; set; } = string.Empty;

        private string Second { get; set; } = string.Empty;

        public bool IsLineInValid => (string.IsNullOrEmpty(First) || string.IsNullOrEmpty(Second)) && !IsSecondVariableInt;

        private double _dSecond;

        public double DValue => _dSecond;

        public bool IsSecondVariableInt => double.TryParse(Second, out _dSecond);

        

    }


    public class FileDetails
    {

        private readonly string apply = "apply";
        public List<FileVariables> AllFileVariables = new List<FileVariables>();
        public double DResults { get; set; } = 0;

        public double? DApplyValue => AllFileVariables?.FirstOrDefault(f => f.First.ToLower() == apply)?.DValue;
        public bool IsApplyExists => AllFileVariables.Count(a => a.First.ToLower() == apply) == 1;

        public bool IsFileValuesValid => AllFileVariables.Any(a => a.IsLineInValid);

        public string DisplayMessage { get; set; }

        public string FileContent { get; set; }

        public bool IsValidToCalculate { get; set; } = false;

        public string[] Lines { get; set; }
    }


}