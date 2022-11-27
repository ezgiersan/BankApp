using BankApplication.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Principal;

namespace BankApplication.Controllers
{

    public class AccountController : Controller
    {
        public IEnumerable Index()
        {
            string json = System.IO.File.ReadAllText("./data.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            return accounts;
        }

        [HttpPost]
        [Route("deposit")]
        public IActionResult Deposit([FromBody] Account account)
        {
            Console.WriteLine("id: " + account.Id);
            Console.WriteLine("balance: " + account.Balance);

            string json = System.IO.File.ReadAllText("./data.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);

            if(account.Balance < 0 )
            {
                return BadRequest(new { message = "Balance can not be negative" });
            } else
            {
                foreach (var item in accounts)
                {
                    
                    if (item.Id == account.Id)
                    {
                        item.Balance += account.Balance;
                        Account temp = item;
                        var updatedAccount = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
                        System.IO.File.WriteAllText("./data.json", updatedAccount);
                        return Ok(temp);                        
                    }
                }
            }
            return NotFound(new { message = "Account not found" });
        }

        [HttpPost]
        [Route("withdraw")]
        public IActionResult WithDraw([FromBody] Account account)
        {
            Console.WriteLine("id: " + account.Id);
            Console.WriteLine("balance: " + account.Balance);

            string json = System.IO.File.ReadAllText("./data.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);

            if (account.Balance < 0)
            {
                return BadRequest(new { message = "Balance can not be negative" });
            }
            else
            {
                foreach (var item in accounts)
                {
                    if (item.Id == account.Id)
                    {
                        if (item.Balance > account.Balance)
                        {
                            item.Balance -= account.Balance;
                            Account temp = item;
                            var updatedAccount = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
                            System.IO.File.WriteAllText("./data.json", updatedAccount);
                            return Ok(temp);
                        }
                        else
                        {
                            return BadRequest(new { message = "Insufficient balance" });

                        }
                    }
                }
            }

            return NotFound(new { message = "Account not found" });
        }


    }
}
