using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;



namespace DigitalBite.Models
{
    public class OrderEntity : TableEntity
    {
        public OrderEntity(string username, string orderID)
            {
            this.PartitionKey = username;
            this.RowKey = orderID;

            }

        public OrderEntity() { }

        public string Food { get; set; }

        public string Price { get; set; }

    }
}
