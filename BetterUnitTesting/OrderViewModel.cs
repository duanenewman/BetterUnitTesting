using System.Collections.Generic;

namespace BetterUnitTesting
{
	public class OrderViewModel
	{
		private IOrderService OrderService { get; }
		public List<Order> Orders { get; set; }

		public OrderViewModel(IOrderService orderService)
		{
			OrderService = orderService;
			Orders = OrderService.GetOrders();
		}
	}
}