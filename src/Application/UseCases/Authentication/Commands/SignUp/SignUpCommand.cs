using Application.Common.Interfaces;
using Application.Common.Models;
using Application.UseCases.Holder.Commands.CreateHolder;
using Application.UseCases.Holder.Commands.DeleteHolder;
using Application.UseCases.User.Commands.CreateUser;
using Domain.Constants;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Authentication.Commands.SignUp;

public record SignUpCommand : IRequest<SignUpResponse>
{
    public required string HolderName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignUpResponse : BaseResponse
{
    public UserDto? CreatedUser { get; set; }
    public HolderDto? CreatedHolder { get; set; }
}

public class SignUpHandler(ILogger<SignUpHandler> logger, IDataContext context, IMediator mediator) : IRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<SignUpHandler> _logger = logger;
    private readonly IMediator _mediator = mediator;

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var response = new SignUpResponse();

        try
        {
            _logger.LogInformation("CreateHolderAndUserCommand: {@Request}", request);

            var createHolderCommand = new CreateHolderCommand
            {
                Name = request.HolderName
            };

            var createdHolder = await _mediator.Send(createHolderCommand, cancellationToken) ?? throw new Exception("Error creating holder");

            if (createdHolder is null || createdHolder.Holder is null)
            {
                _logger.LogWarning("CreateHolderAndUserCommand: Error creating holder");
                response.Message = "Error creating holder";
                return response;
            }

            var createUserCommand = new CreateUserCommand
            {
                Name = request.FullName,
                Email = request.Email,
                Password = request.Password,
                Role = UserRoles.Administrator,
                HolderId = createdHolder.Holder.Id,
            };

            var createdUser = await _mediator.Send(createUserCommand, cancellationToken);

            if (createdUser is null || createdUser.User is null)
            {
                var deleteHolderCommand = new DeleteHolderCommand
                {
                    Id = createdHolder.Holder.Id
                };

                _ = await _mediator.Send(deleteHolderCommand, cancellationToken);
                _logger.LogWarning("CreateHolderAndUserCommand: Error creating user");
                response.Message = "Error creating user";
                return response;
            }

            response.Success = true;
            response.Message = "User and holder created";
            response.CreatedUser = new UserDto
            {
                Id = createdUser.User.Id,
                Name = createdUser.User.Name,
                Email = createdUser.User.Email,
                Role = createdUser.User.Role
            };

            response.CreatedHolder = new HolderDto
            {
                Name = createdHolder.Holder.Name
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateHolderAndUserCommand: {@Request}", request);
            response.Message = "Error creating holder and user";
        }

        return response;
    }
}
