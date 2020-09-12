using System;
using System.Collections.Generic;
using System.Text;
using WhereIsMyOrderAPI.Models;

namespace WhereIsMyOrderAPI.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
