using System;

namespace Moq_POC.Services
{
	public class PaymentService : IPaymentService
	{
		IShipmentService _shipment;
		public PaymentService(IShipmentService shipment)
		{
			_shipment = shipment;
		}
		public string ChargeAndShip(Order order)
		{
			if(order.Card.amount <= 0)
			{
				return "Amount Not Valid";
			}
			if(order.Card != null)
			{
				if(order.Card.ValidTo < DateTime.Now)
					return "Card Expired";
				if (order.Card.CardNumber.Length < 16)
					return "CardNumber Not Valid";
			}

			bool paymentSuccess = MakePayment(order.Card);

			if (paymentSuccess)
			{
				var shipment = _shipment.Ship(order.Address);
				if (shipment != null)
					return "Item Shipped";
				else
					return "Something went wrong with the shipment!!!";
			}
			else
			{
				return "Payment Failed";
			}

		}

		public virtual bool MakePayment(Card card)
		{
			//Third party call to payment service provider.
			return true;
		}
	}
}
