using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentadorDotNet.Models;

[Table("publications_network_postcontent")]
public class PostContent
{
    [Key]
    [Column("id")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [Column("file")]
    [JsonPropertyName("file")]
    public string File { get; set; } = string.Empty;

    [Column("caption")]
    [JsonPropertyName("caption")]
    public string? Caption { get; set; }

    [Column("post_type")]
    [JsonPropertyName("post_type")]
    public string PostType { get; set; } = "post";

    [Column("created_at")]
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
