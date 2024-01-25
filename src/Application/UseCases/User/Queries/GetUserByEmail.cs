using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Queries;

public record GetPokemonByEmailQuery : IRequest<UserEntity?>
{
    public string Email { get; init; } = string.Empty;
}

public class GetPokemonByEmailHandler : IRequestHandler<GetPokemonByEmailQuery, UserEntity?>
{
    private readonly IDataContext _context;
    private readonly ILogger<GetPokemonByEmailHandler> _logger;

    public GetPokemonByEmailHandler(ILogger<GetPokemonByEmailHandler> logger, IDataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<UserEntity?> Handle(GetPokemonByEmailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetPokemonByEmailQuery: {@Request}", request);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        return user;
    }
}
