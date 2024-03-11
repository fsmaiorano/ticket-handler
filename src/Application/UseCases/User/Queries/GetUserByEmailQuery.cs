using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Queries;

public record GetUserByEmailQuery : IRequest<GetUserByEmailResponse>
{
    public required string Email { get; set; }
}

public class GetUserByEmailResponse : BaseResponse
{
    public UserEntity? User { get; set; }
}

public class GetUserByEmailHandler(ILogger<GetUserByEmailHandler> logger, IDataContext context) : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetUserByEmailHandler> _logger = logger;

    public async Task<GetUserByEmailResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var response = new GetUserByEmailResponse();

        try
        {
            _logger.LogInformation("GetUserByEmail: {@Request}", request);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("GetUserByEmail: User not found");
                response.Message = "User not found";

                return response;
            }

            response.Success = true;
            response.User = user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetUserByEmail: {@Request}", request);
            response.Message = "Error getting user";
        }

        return response;
    }
}
