using System;

namespace Intro1
{
    internal class OrderData
    {
        public OrderData(string title)
        {
            this.Title = title;
        }

        public string Title { get; set; }
    }

    internal class Orders
    {
        public Orders(int i)
        {
            this.Count = i;
            this.OrderData = new OrderData($"Amount: {i}");
        }

        public int Count { get; set; }
        public OrderData OrderData { get; set; }
    }

    internal class Program
    {
        private static void DisplayOrders(OrderData orderData)
        {
           Console.WriteLine(orderData);
        }

        private static void Main(string[] args)
        {
            // Initialize by using default Lazy<T> constructor. The
            // Orders array itself is not created yet.
            // Lazy<Orders> _orders = new Lazy<Orders>();

            // Initialize by invoking a specific constructor on Order when Value
            // property is accessed
            Lazy<Orders> _orders2 = new Lazy<Orders>(() => new Orders(100));

            // We need to create the array only if displayOrders is true
            bool displayOrders = DateTime.Now > DateTime.Now.AddDays(-1);
            if (displayOrders)
            {
                DisplayOrders(_orders2.Value.OrderData);
            }
            else
            {
                // Don't waste resources getting order data.
            }
        }
    }
}