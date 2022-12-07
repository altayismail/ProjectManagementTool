using Business_Layer.Concrete;
using Business_Layer.ValidationRules;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class ColumnController : Controller
    {
        ColumnManager columnManager = new ColumnManager(new EFColumnRepo());
        TicketManager ticketManager = new TicketManager(new EFTicketRepo());    
        public IActionResult GetColumns()
        {
            ViewData["CheckColumnButton"] = "true";
            ViewData["CheckTicketButton"] = "true";
            ViewBag.tickets = ticketManager.GetAllQuery();
            var columns = columnManager.GetAllQuery().ToList();
            return View(columns);
        }

        public IActionResult CreateColumn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateColumn(Column column)
        {
            ColumnValidator columnValidator = new ColumnValidator();
            ValidationResult validationResult = columnValidator.Validate(column);

            if(validationResult.IsValid)
            {
                columnManager.AddT(column);
                return RedirectToAction("GetColumns", "Column");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
       
            return View();
        }
    }
}
