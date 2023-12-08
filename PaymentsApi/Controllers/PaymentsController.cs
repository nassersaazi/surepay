using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentsApi.Models;
using PaymentsApi.Data;

namespace PaymentsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApiContext _context;

        public PaymentsController(ApiContext context)
        {
            _context = context;
        }

        

        [HttpPost]
        public JsonResult MakePayment(Transaction transaction)
        {
            var transactionInDb = _context.Transactions.Find(transaction.transactionId);
            if (transactionInDb == null)
            {
                _context.Transactions.Add(transaction);
            }
            else
            {
                return new JsonResult(new
                {
                    Message = "Payment failed."
                });
            }
            _context.SaveChanges();

            return new JsonResult(Ok(transaction));

        }

        [HttpPost]
        public JsonResult GetTransactionStatus(Transaction transaction)
        {
            var transactionInDb = _context.Transactions.Find(transaction.transactionId);
            if (transactionInDb == null)
            {
                return new JsonResult(NotFound());
            }
            else
            {
                return new JsonResult(Ok(transactionInDb));
            }
        
        }

        [HttpPost]
        public JsonResult ValidateReference(Account account)
        {
            var accountInDb = _context.Accounts.Find(account.accountNumber);

            if (accountInDb == null)
            {
                return new JsonResult(NotFound());
            }
            else
            {
                return new JsonResult(Ok(accountInDb));
            }

        }
    }
}
