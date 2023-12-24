namespace CryptoBank.Features.News.Domain;

public class News
{
    public ulong Id { get; private set; }

    public string Author { get; private set; }

    public string Header { get; private set; }

    public DateTime Date { get; private set; }

    public string Body { get; private set; }

    public News(string author, string header, DateTime date, string body)
    {
        Author = author;
        Header = header;
        Date = date;
        Body = body;
    }
}
