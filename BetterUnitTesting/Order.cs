using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterUnitTesting
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime OrderedOn { get; set; }
		public decimal Total { get; set; }
	}
}