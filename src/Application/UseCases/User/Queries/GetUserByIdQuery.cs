using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Queries;

public record GetUserByIdQuery : IRequest<GetUserByIdResponse>
{
    public required Guid Id { get; init; }
}

public class GetUserByIdResponse : BaseResponse
{
    public UserEntity? User { get; set; }
}

public class GetUserByIdHandler(ILogger<GetUserByIdHandler> logger, IDataContext context) : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetUserByIdHandler> _logger = logger;

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetUserByIdResponse();

        try
        {
            _logger.LogInformation("GetUserById: {@Request}", request);

            var user = await _context.Users
                .Include(u => u.Sectors)
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("GetUserById: User not found");
                response.Message = "User not found";

                return response;
            }

            response.Success = true;
            response.User = user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetUserById: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
