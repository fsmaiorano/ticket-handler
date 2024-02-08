using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Queries;

public record GetUsersByHolderIdQuery : IRequest<GetUsersByHolderIdResponse>
{
    public required Guid HolderId { get; set; }
}

public class GetUsersByHolderIdResponse : BaseResponse
{
    public List<UserEntity>? Users { get; set; }
}

public class GetUsersByHolderIdHandler(ILogger<GetUsersByHolderIdHandler> logger, IDataContext context) : IRequestHandler<GetUsersByHolderIdQuery, GetUsersByHolderIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetUsersByHolderIdHandler> _logger = logger;

    public async Task<GetUsersByHolderIdResponse> Handle(GetUsersByHolderIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetUsersByHolderIdResponse();

        try
        {
            _logger.LogInformation("GetUsersByHolderId: {@Request}", request);

            var users = await _context.Users.Where(x => x.HolderId == request.HolderId).ToListAsync(cancellationToken);

            if (users is null)
            {
                _logger.LogWarning("GetUsersByHolderId: Users not found");
                response.Message = "Users not found";

                return response;
            }

            response.Success = true;
            response.Users = users;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetUsersByHolderId: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
