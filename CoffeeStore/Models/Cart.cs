using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class Cart
    {
        private List<OrderDetail> orderDetails = new List<OrderDetail>();
        public virtual void AddItem(Item item, int _quantity)
        {
            OrderDetail orderDetail = orderDetails.Where(od => od.Items.itemID == item.itemID).FirstOrDefault();
            if (orderDetail == null)
            {
                orderDetails.Add(new OrderDetail { Items = item, quantity = _quantity });
            }
            else
            {
                orderDetail.quantity += _quantity;
            }
        }
        public virtual void RemoveLine(Item item) => orderDetails.RemoveAll(l => l.Items.itemID == item.itemID);
        public virtual double ComputeTotalValue() => orderDetails.Sum(e => e.Items.price * e.quantity);
        public virtual void Clear() => orderDetails.Clear();
        public virtual IEnumerable<OrderDetail> ReturnOrderDetails => orderDetails;
    }

}