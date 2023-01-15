using Business_Layer.Concrete;
using Business_Layer.ValidationRules;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace ProjectManagementTool.Controllers
{
    public class TicketController : Controller
    {
        TicketManager ticketManager = new TicketManager(new EFTicketRepo());
        UserManager userManager = new UserManager(new EFUserRepo());
        ColumnManager columnManager = new ColumnManager(new EFColumnRepo());
        ProjectManager projetManager = new ProjectManager(new EFProjectRepo());
        NotificationManager notificationManager = new NotificationManager(new EFNotificationRepo());

        public IActionResult GetTickets()
        {
            ViewData["CheckTicketButton"] = "true";
            var tickets = ticketManager.GetAllTicketWithColumnAndAssignee();
            return View(tickets);
        }

        public IActionResult GetTicket(int id)
        {
            var ticket = ticketManager.GetAllTicketWithColumnAndAssignee().Where(x => x.TicketId == id).Single();
            return View(ticket);
        }

        public IActionResult CreateTicket()
        {
            List<SelectListItem> users = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.UserId.ToString()
                                                }).ToList();
            ViewBag.users = users;
            List<SelectListItem> testers = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.Firstname + " " + x.LastName
                                                }).ToList();
            ViewBag.testers = testers;
            List<SelectListItem> columns = columnManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.ColumnName,
                                                    Value = x.ColumnId.ToString()
                                                }).ToList();
            ViewBag.columns = columns;
            return View();
        }

        [HttpPost]
        public IActionResult CreateTicket(Ticket ticket)
        {
            List<SelectListItem> users = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.UserId.ToString()
                                                }).ToList();
            ViewBag.users = users;
            List<SelectListItem> testers = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.Firstname + " " + x.LastName
                                                }).ToList();
            ViewBag.testers = testers;
            List<SelectListItem> columns = columnManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.ColumnName,
                                                    Value = x.ColumnId.ToString()
                                                }).ToList();
            ViewBag.columns = columns;

            TicketValidator ticketValidator = new TicketValidator();
            ValidationResult validationResult = ticketValidator.Validate(ticket);

            if (validationResult.IsValid)
            {
                try
                {
                    ticket.TicketIdentifier = getFirstLetters(projetManager.GetAllQuery().Single().ProjectName) + (ticketManager.GetAllQuery().OrderBy(x => x.TicketId).Last().TicketId + 1).ToString();
                }
                catch (Exception)
                {
                    ticket.TicketIdentifier = getFirstLetters(projetManager.GetAllQuery().Single().ProjectName) + (1).ToString();
                }
       
                
                ticket.CreatedTime = DateTime.Now;
                ticket.Reporter = userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().Firstname + " " +
                    userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().LastName;
                ticketManager.AddT(ticket);
                Notification notification = new Notification()
                {
                    isRead = false,
                    isUpdated = false,
                    UserId = ticket.UserId,
                    NotificationShortut = ticket.TicketIdentifier,
                    NotificationTarget = $"/Ticket/GetTicket/{int.Parse(Regex.Match(ticket.TicketIdentifier, @"\d+").Value)}"
                };
                notificationManager.AddT(notification);
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

        public IActionResult DeleteTicket(int id)
        {
            var ticket = ticketManager.GetQueryById(id);
            ticketManager.DeleteT(ticket);
            return RedirectToAction("GetTickets");
        }

        [HttpGet]
        public IActionResult UpdateTicket(int id)
        {
            List<SelectListItem> users = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.UserId.ToString()
                                                }).ToList();
            ViewBag.users = users;
            List<SelectListItem> testers = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.Firstname + " " + x.LastName
                                                }).ToList();
            ViewBag.testers = testers;
            List<SelectListItem> columns = columnManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.ColumnName,
                                                    Value = x.ColumnId.ToString()
                                                }).ToList();
            ViewBag.columns = columns;
            var ticket = ticketManager.GetQueryById(id);
            return View(ticket);
        }
        [HttpPost]
        public IActionResult UpdateTicket(Ticket ticket)
        {
            List<SelectListItem> users = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.UserId.ToString()
                                                }).ToList();
            ViewBag.users = users;
            List<SelectListItem> testers = userManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Firstname + " " + x.LastName,
                                                    Value = x.Firstname + " " + x.LastName
                                                }).ToList();
            ViewBag.testers = testers;
            List<SelectListItem> columns = columnManager.GetAllQuery().
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.ColumnName,
                                                    Value = x.ColumnId.ToString()
                                                }).ToList();
            ViewBag.columns = columns;
            TicketValidator validator = new TicketValidator();
            ValidationResult validationResult = validator.Validate(ticket);

            if (validationResult.IsValid)
            {
                ticket.UpdatedTime = DateTime.Now;
                ticketManager.UpdateT(ticket);
                Notification notification = new Notification()
                {
                    isRead = false,
                    UserId = ticket.UserId,
                    NotificationShortut = ticket.TicketIdentifier,
                    NotificationTarget = $"/Ticket/GetTicket/{int.Parse(Regex.Match(ticket.TicketIdentifier, @"\d+").Value)}",
                    isUpdated = true
                };
                notificationManager.UpdateT(notification);
                return RedirectToAction("GetTickets");
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

        public string getFirstLetters(string name)
        {
            string[] letters = name.Split(" ");
            string result = null;
            foreach (var words in letters)
            {
                result += words.Substring(0, 1).ToUpper();
            }
            return result;
        }
    }
}
