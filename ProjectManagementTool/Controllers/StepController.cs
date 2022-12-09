using Business_Layer.Concrete;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class StepController : Controller
    {
        StepManager stepManager = new StepManager(new EFStepRepo());
        public IActionResult GetSteps()
        {
            var steps = stepManager.GetAllQuery();
            return View(steps);
        }

        public IActionResult GetStep(int id)
        {
            var step = stepManager.GetQueryById(id);
            return View(step);
        }

        public IActionResult CreateStep()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateStep(Step step)
        {
            if(step is not null)
            {
                stepManager.AddT(step);
                return RedirectToAction("GetTestCase", "TestCase", new { id = step.TestCaseId });
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
