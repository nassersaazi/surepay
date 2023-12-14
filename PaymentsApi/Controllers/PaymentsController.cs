using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentsApi.Models;
using PaymentsApi.Data;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http.Headers;

namespace PaymentsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(ApiContext context, ILogger<PaymentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        

        [HttpPost]
        public IActionResult MakePayment(Transaction transaction)
        {
            _logger.LogInformation("Make payment request!");
            if (!IsRequestAuthenticated())
            {
                return Unauthorized();
            }
            
            var transactionInDb = _context.Transactions.Find(transaction.transactionId);
            
            if (transactionInDb == null)
            {
                _context.Transactions.Add(transaction);
            }
            else
            {
                return BadRequest("Payment Failed");
            }
            _context.SaveChanges();

            return Ok(transaction);

        }

        [HttpPost]
        public IActionResult GetTransactionStatus(Transaction transaction)
        {
            if (!IsRequestAuthenticated()) { return Unauthorized(); }

            var transactionInDb = _context.Transactions.Find(transaction.transactionId);

            
            if (transactionInDb == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(transactionInDb);
            }

        }

        [HttpPost]
        public IActionResult ValidateReference(Account account)
        {
            if (!IsRequestAuthenticated()) { return Unauthorized(); }

            var accountInDb = _context.Accounts.Find(account.accountNumber);

            if (accountInDb == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(accountInDb);
            }

        }

        public static void GenerateHmacSha512(string secretKey, string stringToHash)
        {
            try
            {
                byte[] byteKey = Encoding.UTF8.GetBytes(secretKey);
                using (HMACSHA512 sha512Hmac = new HMACSHA512(byteKey))
                {
                    byte[] macData = sha512Hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                    string result = Convert.ToBase64String(macData);
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private bool IsRequestAuthenticated()
        {
            try
            {
                if (AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out var basicAuthCredential))
                {
                    if (basicAuthCredential.Scheme == "Basic" &&
                        !string.IsNullOrWhiteSpace(basicAuthCredential.Parameter))
                    { 
                        var usernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(basicAuthCredential.Parameter));
                        if (!string.IsNullOrWhiteSpace(usernamePassword))
                        {
                            var separatorIndex = usernamePassword.IndexOf(':');

                            var username = usernamePassword[..separatorIndex];
                            var password = usernamePassword[(separatorIndex + 1)..];

                            if (username == "test" &&
                                password == "test123")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }
    }

    
}
