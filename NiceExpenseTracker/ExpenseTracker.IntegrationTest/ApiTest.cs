using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseTracker.IntegrationTest
{
    [TestClass]
    public class ApiTest
    {
        private const string BaseAddress = "http://localhost:60299/api";

        [TestMethod]
        public void Get_Expense_By_Name_Month_Year_Test()
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{BaseAddress}/expense/someone/5/2000").Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
        [TestMethod]
        public void Get_All_Expenses_Test()
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{BaseAddress}/expense/").Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
       
    }
}
