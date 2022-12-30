using Business_Layer.Concrete;
using Data_Access_Layer.Concrete;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class StepController : Controller
    {
        StepManager stepManager = new StepManager(new EFStepRepo());
        TestCaseManager testCaseManager = new TestCaseManager(new EFTestCaseRepo());
        private readonly Context _context;
        public StepController(Context context)
        {
            _context= context;
        }
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
            ViewData["Steps"] = stepManager.GetStepsWithTestCase(TestCaseId.testCaseID).OrderBy(x => x.StepOrder).ToList<Step>();
            ViewBag.testCase = testCaseManager.GetQueryById(TestCaseId.testCaseID);
            return View();
        }
        [HttpPost]
        public IActionResult CreateStep(Step step)
        {
            ViewData["Steps"] = stepManager.GetStepsWithTestCase(TestCaseId.testCaseID).OrderBy(x => x.StepOrder).ToList<Step>();
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
                return RedirectToAction("CreateStep", "Step");
            }
            return View();
        }

        public IActionResult UpdateStep(int id)
        {
            var step = stepManager.GetQueryById(id);
            step.OldStepOrder = step.StepOrder;
            return View(step);
        }
        [HttpPost]
        public IActionResult UpdateStep(Step step)
        {
            if (step is not null)
            {
                if(step.StepOrder > step.OldStepOrder)
                {
                    var steps = stepManager.GetAllQuery().Where(x => x.TestCaseId == step.TestCaseId 
                                            && x.StepOrder <= step.StepOrder 
                                            && x.StepOrder > step.OldStepOrder)
                                                .ToList();
                    var idList = new List<int>();
                    idList.AddRange(steps.Select(x => x.StepId));
                    var stepsId = _context.Steps.Where(f => idList.Contains(f.StepId)).ToList();
                    stepsId.ForEach(a => a.StepOrder--);
                    _context.SaveChanges();
                }
                else if(step.OldStepOrder > step.StepOrder)
                {
                    var steps = stepManager.GetAllQuery().Where(x => x.TestCaseId == step.TestCaseId 
                                            && x.StepOrder < step.OldStepOrder 
                                            && x.StepOrder >= step.StepOrder)
                                                .ToList();
                    var idList = new List<int>();
                    idList.AddRange(steps.Select(x => x.StepId));
                    var stepsId = _context.Steps.Where(f => idList.Contains(f.StepId)).ToList();
                    stepsId.ForEach(a => a.StepOrder++);
                    _context.SaveChanges();
                }
                stepManager.UpdateT(step);
                return RedirectToAction("CreateStep", "Step");
            }
            return View();
        }
        public IActionResult DeleteStep(int id)
        {
            var step = stepManager.GetQueryById(id);
            if(step is not null)
            {
                stepManager.DeleteT(step);
                return RedirectToAction("CreateStep", "Step");
            }
            return View();
        }
    }
}
