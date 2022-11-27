using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {

        [TestMethod()]
        public void TestDepositWithNegativeAmount()
        {
            AccountController controller = new AccountController();

            Account accountReq = new Account();
            accountReq.Id = 1;
            accountReq.Balance = -100;

            IActionResult response = controller.Deposit(accountReq);
    
            var trueResponse = response as BadRequestObjectResult;
            Assert.IsNotNull(trueResponse);
            Assert.AreEqual(400, trueResponse.StatusCode);
        }

        [TestMethod()]
        public void TestDepositWithNonExistingAccount()
        {
            AccountController controller = new AccountController();

            Account accountReq = new Account();
            accountReq.Id = 6;
            accountReq.Balance = 100;

            IActionResult response = controller.Deposit(accountReq);

            var trueResponse = response as NotFoundObjectResult;
            Assert.IsNotNull(trueResponse);
            Assert.AreEqual(404, trueResponse.StatusCode);

        }

        [TestMethod()]
        public void TestDepositWithExistingAccount()
        {
            AccountController controller = new AccountController();

            Account accountReq = new Account();
            accountReq.Id = 1;
            accountReq.Balance = 100;

            IActionResult response = controller.Deposit(accountReq);

            var trueResponse = response as OkObjectResult;
            Assert.IsNotNull(trueResponse);
            Assert.AreEqual(200, trueResponse.StatusCode);
        }

        [TestMethod()]
        public void TestWithdrawInsufficientBalance()
        {
            AccountController controller = new AccountController();

            Account accountReq = new Account();
            accountReq.Id = 5;
            accountReq.Balance = 100;

            IActionResult response = controller.WithDraw(accountReq);

            var trueResponse = response as BadRequestObjectResult;
            Assert.IsNotNull(trueResponse);
            Assert.AreEqual(400, trueResponse.StatusCode);

        }

        [TestMethod()]
        public void TestWithdrawWithNonExistingAccount()
        {
            AccountController controller = new AccountController();

            Account accountReq = new Account();
            accountReq.Id = 6;
            accountReq.Balance = 100;

            IActionResult response = controller.WithDraw(accountReq);

            var trueResponse = response as NotFoundObjectResult;
            Assert.IsNotNull(trueResponse);
            Assert.AreEqual(404, trueResponse.StatusCode);

        }
        [TestMethod()]
        public void TestWithdrawWithExistingAccount()
        {
            AccountController controller = new AccountController();

            Account accountReq = new Account();
            accountReq.Id = 1;
            accountReq.Balance = 100;

            IActionResult response = controller.WithDraw(accountReq);

            var trueResponse = response as OkObjectResult;
            Assert.IsNotNull(trueResponse);
            Assert.AreEqual(200, trueResponse.StatusCode);
        }

        [TestMethod()]
        public void TestWithdrawWithNegativeAmount()
        {
            AccountController controller = new AccountController();

            Account accountReq = new Account();
            accountReq.Id = 1;
            accountReq.Balance = -100;

            IActionResult response = controller.WithDraw(accountReq);

            var trueResponse = response as BadRequestObjectResult;
            Assert.IsNotNull(trueResponse);
            Assert.AreEqual(400, trueResponse.StatusCode);
        }

    }
}