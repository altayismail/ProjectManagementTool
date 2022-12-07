﻿using Business_Layer.Concrete;
using Business_Layer.ValidationRules;
using Data_Access_Layer.EntityFramework;
using Entity_Layer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class TicketController : Controller
    {
        TicketManager ticketManager = new TicketManager(new EFTicketRepo());
        UserManager userManager = new UserManager(new EFUserRepo());
        ColumnManager columnManager = new ColumnManager(new EFColumnRepo());

        public IActionResult GetTickets()
        {
            var tickets = ticketManager.GetAllQuery();
            return View(tickets);
        }

        public IActionResult GetTicket(int id)
        {
            var ticket = ticketManager.GetQueryById(id);
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
            ViewBag.testers = users;
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
            ViewBag.testers = users;
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
                ticket.CreatedTime = DateTime.Now;
                ticket.Reporter = userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().Firstname + " " +
                    userManager.GetAllQuery().Where(x => x.Email == User.Identity.Name).Single().LastName;
                ticketManager.AddT(ticket);
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
            var ticket = ticketManager.GetQueryById(id);
            return View(ticket);
        }
        [HttpPost]
        public IActionResult UpdateTicket(Ticket ticket)
        {
            TicketValidator validator = new TicketValidator();
            ValidationResult validationResult = validator.Validate(ticket);

            if (validationResult.IsValid)
            {
                ticketManager.UpdateT(ticket);
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
    }
}