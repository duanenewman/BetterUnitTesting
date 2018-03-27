using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterUnitTesting;
using Moq;
using NUnit.Framework;
using Microsoft.Practices.Unity;

namespace BetterUnitTestingNUnitTests
{
	[TestFixture]
	public class OrderViewModelTest : BaseTest
	{
		[SetUp]
		public void TestSetup()
		{
			//setup the IOrderService for all tests
			RegisterResettableType<IOrderService>(() => mock => 
			{
				mock.Setup(s => s.GetOrders())
					.Returns(new List<Order>());
			});
		}

		[Test]
		public void CreatingOrderViewModelCallsOrderServiceGetOrdersOnce()
		{
			var viewModel = Container.Resolve<OrderViewModel>();

			var service = Mock.Get(Container.Resolve<IOrderService>());
			service.Verify(s => s.GetOrders(), Times.Once);
		}
	}
}
