using Business_Layer.Concrete;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class TestCaseController : Controller
    {
        TestCaseManager testCaseManager = new TestCaseManager(new EFTestCaseRepo());
        TicketManager ticketManager = new TicketManager(new EFTicketRepo());   
        UserManager userManager = new UserManager(new EFUserRepo());    
        public IActionResult GetTestCases()
        {
            ViewData["CheckTestCaseButton"] = "true";
            var testCases = testCaseManager.GetAllQueryWithTicket();
            return View(testCases);
        }
        public IActionResult GetTestCase(int id)
        {
            var testCase = testCaseManager.GetAllQueryWithTicket().Where(x => x.TestCaseId == id).Single();
            return View(testCase);
        }
        public IActionResult CreateTestCase()
        {
            List<SelectListItem> tickets = ticketManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.TicketIdentifier,
                                                    Value = x.TicketId.ToString()
                                                }).ToList();
            ViewBag.tickets = tickets;
            return View();
        }
        [HttpPost]
        public IActionResult CreateTestCase(TestCase testCase)
        {
            List<SelectListItem> tickets = ticketManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.TicketIdentifier,
                                                    Value = x.TicketId.ToString()
                                                }).ToList();
            ViewBag.tickets = tickets;
            if (testCase is not null)
            {
                testCase.TestCaseWriter = userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().Firstname + " " +
                    userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().LastName;
                testCase.TestCaseIdentifier = $"TC{testCaseManager.GetAllQuery().OrderBy(x => x.TestCaseId).Last().TestCaseId + 1}";
                testCase.CreatedTime = DateTime.Now;
                testCaseManager.AddT(testCase);
                return RedirectToAction("GetTestCases","TestCase");
            }
            return View();
        }
        public IActionResult UpdateTestCase(int id)
        {
            var testCase = testCaseManager.GetQueryById(id);
            return View(testCase);
        }
        [HttpPost]
        public IActionResult UpdateTestCase(TestCase testCase)
        {
            if (testCase is not null)
            {
                testCaseManager.UpdateT(testCase);
                return RedirectToAction("GetTestCase", "TestCase", new { id = testCase.TestCaseId });
            }
            return View();
        }
        public IActionResult DeleteTestCase(int id)
        {
            var testCase = testCaseManager.GetQueryById(id);
            if(testCase is not null)
            {
                testCase.UpdatedTime = DateTime.Now;
                testCase.TestCaseWriter = userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().Firstname + " " +
                    userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().LastName;
                testCaseManager.DeleteT(testCase);
                return RedirectToAction("GetTestCases", "TestCase");
            }
            return View();
        }
    }
}
