using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrInCalcTest.BO.Interface;
using BrInCalcTest.Models;

namespace BrInCalcTest.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICalculate _clac;
        private readonly IProcessFile _file;
        private readonly IFileContentValidator _fileContentValidator;
        public HomeController(ICalculate calc, IProcessFile file, IFileContentValidator fileContentValidator)
        {
            _clac = calc;
            _file = file;
            _fileContentValidator = fileContentValidator;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult UploadCalcFile(HttpPostedFileBase fileCalc)
        {
            FileDetails fileDetails = new FileDetails();
            try
            {
                if (fileCalc.ContentLength <= 0) fileDetails.DisplayMessage = "Sorry failed to upload file";

                string fileName = Path.GetFileName(fileCalc.FileName);
                string filepath = Path.Combine(Server.MapPath("~/BritUpload"), fileName);
                fileCalc.SaveAs(filepath);
                string fileContent;
                using (StreamReader sr = System.IO.File.OpenText(filepath))
                {
                    fileContent = sr.ReadToEnd();
                }
                var fileLines = _file.GetLinesFromFile(fileContent);
                var listFv = _file.SplitOperatorsandValues(fileLines);
                fileDetails = new FileDetails
                {
                    Lines = fileLines,
                    AllFileVariables = listFv,
                    FileContent = fileContent
                };
               
              if(Validate(fileDetails))
              {
                  var result = fileDetails?.DApplyValue ?? 0;
                  var listRw = fileDetails?.AllFileVariables?.Where(x => x.First.ToLower() != "apply");
                  if (listRw == null) fileDetails.DResults = 0;
                  if (listRw != null)
                  {
                      foreach (var rw in listRw)
                      {
                          result = _clac.Operator(rw.First, rw.DValue, result);
                      }

                      fileDetails.DResults = result;
                  }
                }

               
                return View("Index", fileDetails);
            }
            catch (Exception ex)
            {
                fileDetails.DisplayMessage += $"File upload failed!! {System.Environment.NewLine} System Error :{ex.Message}";
                return View("Index", fileDetails);
            }
        }


        public bool Validate(FileDetails fileDetails)
        {
            fileDetails.IsValidToCalculate = true;
            if (!_fileContentValidator.ValidateTotalLinesGreaterThanTwo(fileDetails.AllFileVariables))
            {
                fileDetails.DisplayMessage += $"\r\n Number of Inputs are less than 1";
                fileDetails.IsValidToCalculate = false;
            }

            if (!_fileContentValidator.ValidateIsApplyExists(fileDetails))
            {
                fileDetails.DisplayMessage += $"\r\n Apply not exists.";
                fileDetails.IsValidToCalculate  = false;
            }

            if (!_fileContentValidator.ValidateAllSecondPartCanConvertToInt(fileDetails.AllFileVariables))
            {
                fileDetails.DisplayMessage += $"\r\n Not able to identify number.";
                fileDetails.IsValidToCalculate  = false;
            }

            if (!_fileContentValidator.ValidateOperatorsValid(fileDetails.AllFileVariables))
            {
                fileDetails.DisplayMessage += $"\r\n Operators passed are not valid.";
                fileDetails.IsValidToCalculate  = false;
            }

            if (!_fileContentValidator.ValidateApplyShouldBeLast(fileDetails.Lines))
            {
                fileDetails.DisplayMessage += $"\r\n apply should be last statement.";
                fileDetails.IsValidToCalculate = false;
            }
            return fileDetails.IsValidToCalculate;

        }
    }
}