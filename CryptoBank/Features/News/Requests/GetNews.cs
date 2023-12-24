using CryptoBank.Database;
using CryptoBank.Features.News.Dto;
using CryptoBank.Features.News.Options;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CryptoBank.Features.News.Requests;

public static class GetNews
{
    [HttpGet("/news")]
    [AllowAnonymous]
    public class Endpoint : Endpoint<Request, NewsDto[]>
    {
        private readonly IMediator _mediator;

        public Endpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<NewsDto[]> ExecuteAsync(Request request, CancellationToken cancellationToken) =>
            await _mediator.Send(request, cancellationToken);
    }

    public record Request : IRequest<NewsDto[]>;

    public class RequestHandler : IRequestHandler<Request, NewsDto[]>
    {
        private readonly CryptobankContext _context;
        private readonly NewsOptions _options;

        public RequestHandler(CryptobankContext context, IOptions<NewsOptions> options)
        {
            _options = options.Value;
            _context = context;
        }

        public async Task<NewsDto[]> Handle(Request request, CancellationToken cancellationToken) =>
            await _context.News
                .OrderByDescending(x => x.Date)
                .Take(_options.LastCount)
                .Select(n => new NewsDto(n.Id, n.Author, n.Header, n.Date, n.Body))
                .ToArrayAsync(cancellationToken);
    }
}