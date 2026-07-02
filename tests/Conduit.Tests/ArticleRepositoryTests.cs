using Conduit.Models;
using Conduit.Services;
using Xunit;

namespace Conduit.Tests;

public class ArticleRepositoryTests
{
    [Fact]
    public void Add_Generates_Slug_From_Title()
    {
        var repository = new InMemoryArticleRepository();
        var request = new ArticleCreateRequest("Hello World", "desc", "body");

        var article = repository.Add(request);

        Assert.Equal("hello-world", article.Slug);
    }

    [Fact]
    public void GetBySlug_Returns_Null_When_Not_Found()
    {
        var repository = new InMemoryArticleRepository();

        var result = repository.GetBySlug("does-not-exist");

        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Returns_All_Added_Articles()
    {
        var repository = new InMemoryArticleRepository();
        repository.Add(new ArticleCreateRequest("First", "d1", "b1"));
        repository.Add(new ArticleCreateRequest("Second", "d2", "b2"));

        var all = repository.GetAll();

        Assert.Equal(2, all.Count);
    }
}
