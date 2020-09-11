using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace DigitalBite.Controllers
{
    public class BlobsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Connect to a storage account and get a container reference.
        public CloudBlobContainer GetCloudBlobContainer()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration["ConnectionStrings:DigitalBiteStorage"]);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("home-blob-container");
            return container;
        }

        // Create blob container
        public ActionResult CreateBlobContainer()
        {
            // Get a CloudBlobContainer object that represents a reference to the desired blob container name.
            CloudBlobContainer container = GetCloudBlobContainer();

            // Check whether the container exist or not
            ViewBag.Success = container.CreateIfNotExistsAsync().Result;

            // Update view bag with the name of blob container
            ViewBag.BlobContainerName = container.Name;
            return View();
        }

        public ActionResult ListBlobs()
        {
            CloudBlobContainer container = GetCloudBlobContainer();

            List<string> blobs = new List<string>();
            BlobResultSegment resultSegment = container.ListBlobsSegmentedAsync(null).Result;
            foreach (IListBlobItem item in resultSegment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Uri.ToString());
                }
                
            }
            return View(blobs);
        }


    }
}
