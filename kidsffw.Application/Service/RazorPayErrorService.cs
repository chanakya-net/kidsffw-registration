namespace kidsffw.Application.Service;

using Common.DTO;
using Common.Interfaces.Repository;
using Domain.Entity;
using Interfaces.Service;
using Mapster;
using Specifications;

public class RazorPayErrorService : IRazorPayErrorService
{
    private readonly IUnitOfWork _unitOfWork;

    public RazorPayErrorService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<string?> FindEventId(string eventId)
    {
        var spec = Specifications.GetEventByIdError(eventId);
        var eventDto = await _unitOfWork.Repository<RazorPayErrorEntity>().FirstOrDefaultAsync(spec);
        return eventDto?.EventId;
    }

    public async Task<int> SaveErrorInformation(RazorPayErrorDto payment)
    {
        var result = await _unitOfWork.Repository<RazorPayErrorEntity>().AddAsync(
            payment.Adapt<RazorPayErrorEntity>()
        );
        await _unitOfWork.SaveChangesAsync();
        return result.Id;
    }
}