namespace Moq_POC.Services
{
	public interface IPaymentService
	{
		string ChargeAndShip(Order order);
	}
}
