using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentadorDotNet.Data;
using PresentadorDotNet.Models;

namespace PresentadorDotNet.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _db;

    public PostsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _db.PostContents.OrderByDescending(p => p.Id).ToListAsync();
        return Ok(posts);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var post = await _db.PostContents.FindAsync(id);
        return post != null ? Ok(post) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostContent newPost)
    {
        if (string.IsNullOrWhiteSpace(newPost.File))
        {
            newPost.File = "publications_media/sample.png";
        }
        if (newPost.CreatedAt == default)
        {
            newPost.CreatedAt = DateTime.UtcNow;
        }

        _db.PostContents.Add(newPost);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = newPost.Id }, newPost);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] PostContent updatedPost)
    {
        var post = await _db.PostContents.FindAsync(id);
        if (post == null) return NotFound();

        if (!string.IsNullOrWhiteSpace(updatedPost.Caption))
        {
            post.Caption = updatedPost.Caption;
        }
        if (!string.IsNullOrWhiteSpace(updatedPost.PostType))
        {
            post.PostType = updatedPost.PostType;
        }
        if (!string.IsNullOrWhiteSpace(updatedPost.File))
        {
            post.File = updatedPost.File;
        }

        await _db.SaveChangesAsync();
        return Ok(post);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var post = await _db.PostContents.FindAsync(id);
        if (post == null) return NotFound();

        _db.PostContents.Remove(post);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
