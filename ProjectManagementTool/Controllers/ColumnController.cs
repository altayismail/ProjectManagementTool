using Business_Layer.Concrete;
using Business_Layer.ValidationRules;
using Data_Access_Layer.Concrete;
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
        private readonly Context _context;
        public ColumnController(Context context)
        {
            _context = context;
        }
        public IActionResult GetColumns()
        {
            ViewData["CheckColumnButton"] = "true";
            ViewData["CheckTicketButton"] = "true";
            ViewBag.tickets = ticketManager.GetAllTicketWithColumnAndAssignee();
            var columns = columnManager.GetAllQuery().ToList().OrderBy(x => x.ColumnOrder).ToList<Column>();
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
                try
                {
                    column.ColumnOrder = columnManager.GetAllQuery().OrderBy(x => x.ColumnOrder).Last().ColumnOrder + 1;
                }
                catch (Exception)
                {
                    column.ColumnOrder = 1;
                }

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
            column.OldColumnOrder = column.ColumnOrder;
            return View(column);
        }

        [HttpPost]
        public IActionResult UpdateColumn(Column column)
        {
            ColumnValidator columnValidator = new ColumnValidator();
            ValidationResult validationResult = columnValidator.Validate(column);

            if (validationResult.IsValid)
            {
                if (column.ColumnOrder > column.OldColumnOrder)
                {
                    var columns = columnManager.GetAllQuery().Where(x => x.ColumnOrder <= column.ColumnOrder
                                            && x.ColumnOrder > column.OldColumnOrder)
                                                .ToList();
                    var idList = new List<int>();
                    idList.AddRange(columns.Select(x => x.ColumnId));
                    var columnsId = _context.Columns.Where(f => idList.Contains(f.ColumnId)).ToList();
                    columnsId.ForEach(a => a.ColumnOrder--);
                    _context.SaveChanges();
                }
                else if (column.OldColumnOrder > column.ColumnOrder)
                {
                    var columns = columnManager.GetAllQuery().Where(x => x.ColumnOrder < column.OldColumnOrder
                                            && x.ColumnOrder >= column.ColumnOrder)
                                                .ToList();
                    var idList = new List<int>();
                    idList.AddRange(columns.Select(x => x.ColumnId));
                    var columnsId = _context.Columns.Where(f => idList.Contains(f.ColumnId)).ToList();
                    columnsId.ForEach(a => a.ColumnOrder++);
                    _context.SaveChanges();
                }
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
