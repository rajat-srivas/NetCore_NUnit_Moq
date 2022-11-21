using System.Collections.Generic;
using System.Linq;

namespace Moq_POC.Services
{
	public class ShipmentService : IShipmentService
	{

		public ShipmentDetails Ship(AddressInfo info)
		{
			//I am a third party service whose result cannot be predicted for now so I will always be mocked
			return new ShipmentDetails();
		}
	}
}
