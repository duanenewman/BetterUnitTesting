using System.Collections.Generic;

namespace BetterUnitTesting
{
	public interface IOrderService
	{
		List<Order> GetOrders();
	}
}