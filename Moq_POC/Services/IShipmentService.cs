using System.Collections.Generic;

namespace Moq_POC.Services
{
	public interface IShipmentService
	{
		ShipmentDetails Ship(AddressInfo info);
	}
}
