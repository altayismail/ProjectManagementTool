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
            ViewBag.tickets = ticketManager.GetAllTicketWithColumnAndAssignee();
            var columns = columnManager.GetAllQuery().ToList();
            if(columns is null)
            {
                ViewBag.ErrorMessage = "Please add Column to start Project.";
            }
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

        public IActionResult UpdateColumn(int id)
        {
            var column = columnManager.GetQueryById(id);
            return View(column);
        }

        [HttpPost]
        public IActionResult UpdateColumn(Column column)
        {
            ColumnValidator columnValidator = new ColumnValidator();
            ValidationResult validationResult = columnValidator.Validate(column);

            if (validationResult.IsValid)
            {
                columnManager.UpdateT(column);
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
        public IActionResult DeleteColumn(int id)
        {
            var column = columnManager.GetQueryById(id);
            if(column is not null)
            {
                columnManager.DeleteT(column);
                return RedirectToAction("GetColumns", "Column");
            }
            return View();
        }
    }
}
