namespace kidsffw.Application.Service;

using Common.DTO;
using Common.Interfaces.Repository;
using Domain.Entity;
using Interfaces.Service;
using Specifications;

public class RazorPayPaymentService : IRazorPayPaymentService
{
    private readonly IUnitOfWork _unitOfWork;


    public RazorPayPaymentService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<string?> FindEventId(string eventId)
    {
        var spec = Specifications.GetEventByIdPayment(eventId);
        var eventDto = await _unitOfWork.Repository<RazorPayPaymentEntity>().FirstOrDefaultAsync(spec);
        return eventDto?.EventId;
    }

    public async Task<int> SavePaymentInformation(RazorPayPaymentDto payment)
    {
        var result = await _unitOfWork.Repository<RazorPayPaymentEntity>().AddAsync(
            new RazorPayPaymentEntity()
            {
                AmountPaid = payment.AmountPaid,
                EventId = payment.EventId,
                PaymentId = payment.PaymentId,
                MobileNumber = payment.MobileNumber,
                OrderId = payment.OrderId
            });
        await _unitOfWork.SaveChangesAsync();
        return result.Id;
    }
}