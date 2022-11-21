using System;
using System.Collections.Generic;

namespace Moq_POC.Services
{
	public class AddressInfo
	{
		public string Street { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string PhoneNumber { get; set; }
	}

	public class Card
	{
		public string CardNumber { get; set; }
		public string Name { get; set; }
		public DateTime ValidTo { get; set; }

		public double amount { get; set; }
	}

	public class CartItem
	{
		public string ProductId { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
	}

	public class ShipmentDetails
	{
		public string TrackingId { get; set; }

		public DateTime ShippingDate { get; set; }

		public DateTime ExpectedDelivery { get; set; }

		public string DeliveryPartner { get; set; }
	}

	public class Order
	{
		public Card Card { get; set; }
		public AddressInfo Address { get; set; }
		public List<CartItem> CartItems { get; set; }

	}
}
