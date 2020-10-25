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
        public HomeController(ICalculate calc, IProcessFile file)
        {
            _clac = calc;
            _file = file;
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
                if (fileCalc.ContentLength <= 0) ViewBag.Message = "Sorry failed to upload file";

                string fileName = Path.GetFileName(fileCalc.FileName);
                string filepath = Path.Combine(Server.MapPath("~/BritUpload"), fileName);
                fileCalc.SaveAs(filepath);
                string fileContent;
                using (StreamReader sr = System.IO.File.OpenText(filepath))
                {
                    fileContent = sr.ReadToEnd();
                }
                var fileLines = _file.GetLinesFromFile(fileContent);
                var listFV = _file.SplitOperatorsandValues(fileLines);
                fileDetails = new FileDetails
                {
                    AllFileVariables = listFV
                };

               
              if(Validate(fileDetails))
                _clac.CalculateResults(fileDetails);

               
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
            bool bvalidate = true;
            if (fileDetails.AllFileVariables.Count < 2)
            {
                fileDetails.DisplayMessage += $"\r\n Number of Inputs are less than 1";
                bvalidate = false;
            }

            if (!fileDetails.IsApplyExists)
            {
                fileDetails.DisplayMessage += $"\r\n Apply not exits.";
                bvalidate = false;
            }

            if (fileDetails.AllFileVariables.Count(x => x.IsSecondVariableInt) != fileDetails.AllFileVariables.Count())
            {
                fileDetails.DisplayMessage += $"\r\n Not able to identify number.";
                bvalidate = false;
            }

            return bvalidate;

        }
    }
}