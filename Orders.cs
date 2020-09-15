using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WhereIsMyOrderAPI.Models;
using System.Linq;
using WhereIsMyOrderAPI.Repositories;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WhereIsMyOrderAPI
{
    public class Orders
    {
        IOrderRepository OrderRepository { get; set; }

        public Orders(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        [FunctionName("GetOrdersForUser")]
        public Order[] GetOrdersForUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "GetOrdersForUser")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetOrdersForUser processed a request.");
            string userId = req.Query["userId"];
            var results = OrderRepository.GetAll().Where(x => x.UserId == userId).ToArray();
            return results;
        }

        [FunctionName("CreateOrder")]
        public async Task<Order> CreateOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "CreateOrder")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateOrder function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<OrderRoot>(requestBody);
            var newOrder = data.newOrder;
            OrderRepository.Insert(newOrder);
            OrderRepository.SaveChanges();
            return newOrder;
        }

        [FunctionName("UpdateOrder")]
        public async Task<Order> UpdateOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PUT", Route = "UpdateOrder")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("UpdateOrder function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<OrderRoot>(requestBody);
            var updatedOrder = data.newOrder;
            OrderRepository.Update(updatedOrder);
            OrderRepository.SaveChanges();
            return updatedOrder;
        }

        [FunctionName("DeleteOrder")]
        public ActionResult DeleteOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "DeleteOrder")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("DeleteOrder function processed a request.");
            var orderId = int.Parse(req.Query["orderId"].ToString());
            var order = OrderRepository.GetAll().Where(x => x.Id == orderId).FirstOrDefault();
            OrderRepository.Delete(order);
            OrderRepository.SaveChanges();
            return new OkResult();
        }

        [FunctionName("DeleteOrders")]
        public ActionResult DeleteOrders(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "DeleteOrders")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("DeleteOrders function processed a request.");
            var orderIdsString = req.Query["orderIds"].ToString();
            var orderIdsSplitted = orderIdsString.Split(',');
            int[] orderIds = Array.ConvertAll(orderIdsSplitted, s => int.Parse(s));
            var orders = OrderRepository.GetAll().Where(x => orderIds.Contains(x.Id)).ToList();
            foreach (var order in orders)
            {
                OrderRepository.Delete(order);
            }

            OrderRepository.SaveChanges();
            return new OkResult();
        }
    }

    public class OrderRoot
    {
        public Order newOrder { get; set; }
    }
}
