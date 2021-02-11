using System;

namespace WhereIsMyOrderAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Company { get; set; }

        public string Title { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string Link { get; set; }

        public string Status { get; set; }

        public string ZipCode { get; set; }

        public string TrackingNumber { get; set; }

        public string UserId { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
