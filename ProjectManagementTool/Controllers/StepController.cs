using Business_Layer.Concrete;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class StepController : Controller
    {
        StepManager stepManager = new StepManager(new EFStepRepo());
        TestCaseManager testCaseManager = new TestCaseManager(new EFTestCaseRepo());
        public IActionResult GetSteps(int testCaseId)
        {
            ViewData["CheckAddStepButton"] = "true";
            TestCaseId.testCaseID = testCaseId;
            ViewBag.testCase = testCaseManager.GetQueryById(testCaseId);
            var steps = stepManager.GetStepsWithTestCase(testCaseId);
            return View(steps);
        }

        public IActionResult GetStep(int id)
        {
            var step = stepManager.GetQueryById(id);
            return View(step);
        }

        public IActionResult CreateStep()
        {
            ViewData["Steps"] = stepManager.GetStepsWithTestCase(TestCaseId.testCaseID);
            ViewBag.testCase = testCaseManager.GetQueryById(TestCaseId.testCaseID);
            return View();
        }
        [HttpPost]
        public IActionResult CreateStep(Step step)
        {
            ViewData["Steps"] = stepManager.GetStepsWithTestCase(TestCaseId.testCaseID);
            ViewBag.testCase = testCaseManager.GetQueryById(TestCaseId.testCaseID);
            if (step is not null)
            {
                try
                {
                    step.StepOrder = stepManager.GetStepsWithTestCase(TestCaseId.testCaseID).OrderBy(x => x.StepId).Last().StepOrder + 1;
                }
                catch (Exception)
                {
                    step.StepOrder = 1;
                }
                    
                step.TestCaseId = TestCaseId.testCaseID;
                stepManager.AddT(step);
                return RedirectToAction("GetTestCase", "TestCase", new { id = TestCaseId.testCaseID });
            }
            return View();
        }

        public IActionResult UpdateStep(int id)
        {
            var step = stepManager.GetQueryById(id);
            return View(step);
        }
        [HttpPost]
        public IActionResult UpdateStep(Step step)
        {
            if (step is not null)
            {
                stepManager.UpdateT(step);
                return RedirectToAction("GetTestCase", "TestCase", new { id = step.TestCaseId });
            }
            return View();
        }
        public IActionResult DeleteStep(int id)
        {
            var step = stepManager.GetQueryById(id);
            if(step is not null)
            {
                stepManager.DeleteT(step);
                return RedirectToAction("GetTestCase", "TestCase", new { id = step.TestCaseId });
            }
            return View();
        }
    }
}
