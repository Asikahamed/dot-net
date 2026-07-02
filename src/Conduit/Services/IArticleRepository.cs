using Conduit.Models;

namespace Conduit.Services;

public interface IArticleRepository
{
    IReadOnlyList<Article> GetAll();

    Article? GetBySlug(string slug);

    Article Add(ArticleCreateRequest request);
}

public class InMemoryArticleRepository : IArticleRepository
{
    private readonly List<Article> _articles = new();
    private int _nextId = 1;

    public IReadOnlyList<Article> GetAll() => _articles.AsReadOnly();

    public Article? GetBySlug(string slug) =>
        _articles.FirstOrDefault(a => a.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

    public Article Add(ArticleCreateRequest request)
    {
        var article = new Article
        {
            Id = _nextId++,
            Title = request.Title,
            Description = request.Description,
            Body = request.Body,
            Slug = Slugify(request.Title)
        };

        _articles.Add(article);
        return article;
    }

    public static string Slugify(string title)
    {
        return title
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-");
    }
}
