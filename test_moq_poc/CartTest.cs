using Moq;
using Moq_POC.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace test_moq_poc
{
	public class CartTest
	{
		private ICartService cartService;
		private Mock<PaymentService> paymentServicePMoq;
		private Mock<IShipmentService> shipmentServiceMock;

		[SetUp]
		public void Setup()
		{
			// Shipment is considered to be a third party service which cannot be tested so it has been fully mocked 
			shipmentServiceMock = new Mock<IShipmentService>();

			// for partial moq, concreate implementation of the class needs to be mocked
			// PaymentService is partial mocked as MakePayment() is mocked and not the others
			paymentServicePMoq = new Mock<PaymentService>(shipmentServiceMock.Object);
			
			// Not mocked, actual service is used as we want to test the implementation
			cartService = new CartService(paymentServicePMoq.Object);
		}

		[Test]
		[TestCase(-1,"4041000011114567",true,1,true, true, "Amount Not Valid")] // fail as amount in less than 0
		[TestCase(10, "404100001111456", true,2, true, true, "CardNumber Not Valid")] // fail as card number is invalid
		[TestCase(12, "4041000011114561", false,3, true, true, "Card Expired")] // fail as card number expiry date  is not valid
		[TestCase(11, "40410000111145610", true, 11, true, true, "Invalid Product Quantity")] // fail as quantity is more than 10
		[TestCase(5, "40410000111145610", true, 9,false, true, "Payment Failed")] // fail as payment failed on third party
		[TestCase(8, "40410000111145610", true, 9, true, false, "Something went wrong with the shipment!!!")] // fail as shipment failed
		[TestCase(4, "40410000111145610", true,9,true, true, "Item Shipped")] // pass
		public void CartService_Validated_ShipsProduct(double amount, string cNumber, bool validDate, int quantity, 
			bool paymentSuccess, bool shipmentSuccess, string expectedResult)
		{
			

			var cardObj = new Card()
			{
				CardNumber = cNumber,
				ValidTo = validDate ? DateTime.Now.AddDays(10): DateTime.Now.AddDays(-10),
				Name = "Random User",
				amount = amount
			};

			var address = new AddressInfo();

			var cartItems = new List<CartItem>();
			cartItems.Add(new CartItem()
			{
				ProductId = "1001",
				Quantity = quantity,
				Price = 100
			});

			var order = new Order()
			{
				Address = address,
				CartItems = cartItems,
				Card = cardObj
			};

			var shipmentAck = new ShipmentDetails();
			using (StreamReader r = new StreamReader("C:\\Users\\rasrivas\\source\\repos\\Moq_POC\\test_moq_poc\\Mocked_Response\\MockedShipmentAcknowledgement.json"))
			{
				string json = r.ReadToEnd();
				shipmentAck = JsonSerializer.Deserialize<ShipmentDetails>(json);
			}

			//creating response of the mocked ShipmentService
			shipmentServiceMock.Setup(x => x.Ship(It.IsAny<AddressInfo>())).Returns(shipmentSuccess ? shipmentAck : null);

			paymentServicePMoq.CallBase = true;
			paymentServicePMoq.Setup(z => z.MakePayment(It.IsAny<Card>())).Returns(paymentSuccess);


			var result = cartService.ValidateCart(order);
			paymentServicePMoq.Verify();
			shipmentServiceMock.Verify();

			Assert.AreEqual(expectedResult, result);

			Assert.Pass();
		}
	}
}