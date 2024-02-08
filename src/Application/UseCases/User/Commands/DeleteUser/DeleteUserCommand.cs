using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.DeleteUser;

public record DeleteUserCommand : IRequest<BaseResponse>
{
    public Guid Id { get; set; }
}

public class DeleteUserResponse : BaseResponse
{

}

public class DeleteUserHandler(ILogger<DeleteUserHandler> logger, IDataContext context) : IRequestHandler<DeleteUserCommand, BaseResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteUserHandler> _logger = logger;

    public async Task<BaseResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        try
        {
            _logger.LogInformation("DeleteUserCommand: {@Request}", request);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("DeleteUserCommand: User not found");
                response.Message = "User not found";

                return response;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "User deleted successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteUserCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}