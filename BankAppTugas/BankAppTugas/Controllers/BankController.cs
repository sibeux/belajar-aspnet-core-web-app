using BankAppTugas.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAppTugas.Controllers
{
    [Controller]
    public class BankController : Controller
    {
        [Route("/")]
        public IActionResult Home()
        {
            return Content("<h1>Welcome to the Bank</h1>", "text/html");
        }

        [Route("/account-details")]
        public IActionResult AccountDetail()
        {
            Account account = new Account()
            {
                AccountNumber = 1001,
                AccountHolderName = "John Smith",
                CurrentBalance = 10000,
            };

            Response.StatusCode = 200;
            return Json(account);
        }

        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            return File("/bank-statement.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber?}")]
        public IActionResult GetCurrentNumber()
        {
            if (Request.RouteValues["accountNumber"] == null)
            {
                return RedirectToAction("Home", "Bank", new {});
            } else
            {
                if (Convert.ToInt16(Request.RouteValues["accountNumber"]) == 1001)
                {
                    return Content("<h1>5000</h1>", "text/html");
                }
                else
                {
                    return NotFound("user not found");
                }
            }
        }
    }
}
