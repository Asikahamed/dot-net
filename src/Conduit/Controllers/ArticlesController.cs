using Conduit.Models;
using Conduit.Services;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleRepository _repository;

    public ArticlesController(IArticleRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Article>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{slug}")]
    public ActionResult<Article> GetBySlug(string slug)
    {
        var article = _repository.GetBySlug(slug);

        if (article is null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    [HttpPost]
    public ActionResult<Article> Create([FromBody] ArticleCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required.");
        }

        var article = _repository.Add(request);
        return CreatedAtAction(nameof(GetBySlug), new { slug = article.Slug }, article);
    }
}
