namespace CryptoBank.Features.News.Dto;

public record NewsDto(ulong Id, string Author, string Header, DateTime Date, string Body);