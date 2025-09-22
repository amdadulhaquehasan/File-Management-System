using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;

[Table("FileRecommendations", Schema = "dbo")]
public class FileRecommendations
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public string? RecommendationReason { get; set; } = null;
    public DateTime RecommendationAt { get; set; } = DateTime.UtcNow;
    public bool IsUnused { get; set; } =  false;
    
    //ForeignKeys
    [ForeignKey("FileId")]
    public virtual Files File { get; set; }
}