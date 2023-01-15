using Business_Layer.Concrete;
using Data_Access_Layer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class ReportController : Controller
    {
        TestCaseManager testCaseManager = new TestCaseManager(new EFTestCaseRepo());
        TicketManager ticketManager = new TicketManager(new EFTicketRepo());
        public IActionResult GetReports()
        {
            return View();
        }
        public IActionResult GetDailyReport()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetDailyData()
        {
            var testResults = testCaseManager.GetAllQuery().Where(x => x.CreatedTime.Date == DateTime.Now.Date && x.isTested == true).ToList();
            List<ReportViewModel> report = new List<ReportViewModel>()
            {
                new ReportViewModel
                {
                    Result = "Success",
                    Count = testResults.Where(x => x.CaseResult == true).Count()
                },
                new ReportViewModel
                {
                    Result = "Fail",
                    Count = testResults.Where(x => x.CaseResult == false).Count()
                }
            };
            return Json(report);
        }
        public IActionResult GetMonthlyReport()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetMonthlyData()
        {
            var testResults = testCaseManager.GetAllQuery().Where(x => x.CreatedTime.Month == DateTime.Now.Month && x.isTested == true).ToList();
            List<ReportViewModel> report = new List<ReportViewModel>()
            {
                new ReportViewModel
                {
                    Result = "Success",
                    Count = testResults.Where(x => x.CaseResult == true).Count()
                },
                new ReportViewModel
                {
                    Result = "Fail",
                    Count = testResults.Where(x => x.CaseResult == false).Count()
                }
            };
            return Json(report);
        }
        public IActionResult GetYearlyReport()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetYearlyData()
        {
            var testResults = testCaseManager.GetAllQuery().Where(x => x.CreatedTime.Year == DateTime.Now.Year && x.isTested == true).ToList();
            List<ReportViewModel> report = new List<ReportViewModel>()
            {
                new ReportViewModel
                {
                    Result = "Success",
                    Count = testResults.Where(x => x.CaseResult == true).Count()
                },
                new ReportViewModel
                {
                    Result = "Fail",
                    Count = testResults.Where(x => x.CaseResult == false).Count()
                }
            };
            return Json(report);
        }
        public IActionResult GetTesterReport()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetTesterData()
        {
            var testResults = testCaseManager.GetAllQuery().Where(x => x.CreatedTime.Year == DateTime.Now.Year && x.isTested == true).ToList();
            List<TesterViewModel> report = new List<TesterViewModel>();
            report = testResults.Select(x => new TesterViewModel
            {
                Tester = x.TestCaseWriter,
                NumberofTest = testResults.Where(y => y.TestCaseWriter == x.TestCaseWriter).Count()
            }).ToList();
            report = report.DistinctBy(x => x.Tester).ToList();
            return Json(report);
        }
    }
    public class ReportViewModel
    {
        public int Count { get; set; }
        public string? Result { get; set; }
    }

    public class TesterViewModel
    {
        public int NumberofTest { get; set; }
        public string? Tester { get; set; }
    }
}
