using Application.Common.Interfaces;
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

public record SignUpResponse
{
    public Guid CreatedUser { get; init; }
    public Guid CreatedHolder { get; init; }
}

public class SignUpHandler(ILogger<SignUpHandler> logger, IDataContext context, IMediator mediator) : IRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<SignUpHandler> _logger = logger;
    private readonly IMediator _mediator = mediator;

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreateHolderAndUserCommand: {@Request}", request);

            var createHolderCommand = new CreateHolderCommand
            {
                Name = request.HolderName
            };

            var createdHolderGuid = await _mediator.Send(createHolderCommand, cancellationToken) ??
                                    throw new Exception("Error creating holder");

            var createUserCommand = new CreateUserCommand
            {
                Name = request.FullName,
                Email = request.Email,
                Password = request.Password,
                Role = UserRoles.Administrator,
                HolderId = (Guid)createdHolderGuid,
            };

            var createdUserGuid = await _mediator.Send(createUserCommand, cancellationToken);

            if (createdUserGuid is null)
            {
                var deleteHolderCommand = new DeleteHolderCommand
                {
                    Id = (Guid)createdHolderGuid
                };

                _ = await _mediator.Send(deleteHolderCommand, cancellationToken);
                throw new Exception("Error creating user or holder");
            }

            return new SignUpResponse
            {
                CreatedUser = (Guid)createdUserGuid,
                CreatedHolder = (Guid)createdHolderGuid
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateHolderAndUserCommand: {@Request}", request);
            throw;
        }
    }
}
