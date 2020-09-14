using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DigitalBite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace DigitalBite.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private CloudTable getTableStorageInformation()
        {
            //step 1: read json
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();

            //to get key access
            //once link, time to read the content for connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configure["ConnectionStrings:DigitalBiteStorage"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            //make new table

            CloudTable table = tableClient.GetTableReference("OrderTable");

            return table;
        }



        public ActionResult CreateTable()
        {
            CloudTable table = getTableStorageInformation();
            ViewBag.Success = table.CreateIfNotExistsAsync().Result;
            ViewBag.TableName = table.Name;
            return View();
        }


        public ActionResult AddEntity(string email, string food, string price)
        {
            CloudTable table = getTableStorageInformation();
            DateTime now = DateTime.Now;
            string asString = now.ToString("dd MMMM yyyy hh:mm:ss tt");

            OrderEntity customer1 = new OrderEntity(email, asString);
            
            customer1.Food = food;
            customer1.Price = price;


            TableOperation insertOperation = TableOperation.Insert(customer1);
            TableResult result = table.ExecuteAsync(insertOperation).Result;

            ViewBag.TableName = food; 
            ViewBag.Result = result.HttpStatusCode;

            return View();
        }

        public ActionResult Orders()
        {
            CloudTable table = getTableStorageInformation();

            TableQuery<OrderEntity> query = new TableQuery<OrderEntity>();


            List<OrderEntity> orders = new List<OrderEntity>();
            TableContinuationToken token = null;

            do
            {
                TableQuerySegment<OrderEntity> resultsSegment = table.ExecuteQuerySegmentedAsync(query, token).Result;

                foreach (OrderEntity orderEntity in resultsSegment.Results)
                {
                    orders.Add(orderEntity);
                }
            }
            while (token != null);

            return View(orders);
        }

        public ActionResult OrderHistory(string email)
        {
            CloudTable table = getTableStorageInformation();

            TableQuery<OrderEntity> query = new TableQuery<OrderEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, email));


            List<OrderEntity> orders = new List<OrderEntity>();
            TableContinuationToken token = null;

            do
            {
                TableQuerySegment<OrderEntity> resultsSegment = table.ExecuteQuerySegmentedAsync(query, token).Result;

                foreach (OrderEntity orderEntity in resultsSegment.Results)
                {
                    orders.Add(orderEntity);
                }
            }
            while (token != null);

            return View(orders);
        }
    }

    


}
