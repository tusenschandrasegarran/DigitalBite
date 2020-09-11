using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    }

    


}
