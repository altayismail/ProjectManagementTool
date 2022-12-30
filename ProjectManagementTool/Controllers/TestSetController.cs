using Business_Layer.Concrete;
using Data_Access_Layer.Concrete;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class TestSetController : Controller
    {
        TestSetManager testSetManager = new TestSetManager(new EFTestSetRepo());
        TestCaseManager testCaseManager = new TestCaseManager(new EFTestCaseRepo());
        public IActionResult GetTestSets()
        {
            ViewData["CheckAddTestSetButton"] = "true";
            var testSets = testSetManager.GetAllQuery();
            return View(testSets);
        }

        public IActionResult GetTestSet(int id)
        {
            var testSet = testSetManager.GetQueryById(id);
            ViewData["TestCases"] = testCaseManager.GetAllQueryWithTicket().Where(x => x.TestSetId == id).ToList();
            return View(testSet);
        }

        public IActionResult CreateTestSet()
        {
            ViewBag.TestCases = GetTestCases();
            return View();
        }
        [HttpPost]
        public IActionResult CreateTestSet(TestSet testSet)
        {
            ViewBag.TestCases = GetTestCases();
            if (testSet == null )
            {
                return View();
            }
            else
            {
                testSet.CreatedDate = DateTime.Now;
                testSetManager.AddT(testSet);
                return RedirectToAction("GetTestSets");
            }
        }

        public IActionResult AddTestCaseforTestSet(int id)
        {
            var testSet = testSetManager.GetQueryById(id);
            ViewBag.TestCases = GetTestCases();
            return View(testSet);
        }
        [HttpPost]
        public IActionResult AddTestCaseforTestSet(TestSet testSet)
        {
            if(testSet == null)
            {
                return View();
            }
            else
            {
                using(var context = new Context())
                {
                    var testCase = context.TestCases.Where(x => x.TestCaseId == testSet.TestCaseId).FirstOrDefault();
                    testCase.TestSetId = testSet.TestSetId;
                    context.SaveChanges();
                }
                return RedirectToAction("GetTestSet", "TestSet", new { id = testSet.TestSetId });
            }
        }

        public IActionResult UpdateTestSet(int id)
        {
            var testSet = testSetManager.GetQueryById(id);
            return View(testSet);
        }
        [HttpPost]
        public IActionResult UpdateTestSet(TestSet testSet)
        {
            if (testSet == null)
            {
                return View();
            }
            else
            {
                testSet.UpdatedDate = DateTime.Now;
                testSetManager.UpdateT(testSet);
                return RedirectToAction("GetTestSet");
            }
        }

        public IActionResult AddTestResult(int id)
        {
            var testSet = testSetManager.GetQueryById(id);
            ViewBag.TestCases = testCaseManager.GetAllQueryWithTicket().Where(x => x.TestSetId == id).ToList();
            return View(testSet);
        }
        [HttpPost]
        public IActionResult AddTestResult(TestSet testSet)
        {
            if(testSet == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("GetTestSets");
            }
        }

        private List<SelectListItem> GetTestCases()
        {
            List<SelectListItem> testCases = testCaseManager.GetAllQueryWithTicket().Where(x => x.TestSetId == null).
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.TestCaseIdentifier + " | " + x.Name,
                                                    Value = x.TestCaseId.ToString()
                                                }).ToList();
            return testCases;
        }
    }
}
