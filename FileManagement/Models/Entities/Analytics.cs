using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FileManagement.Models.Entities;

[Table("Analytics", Schema = "dbo")]
public class Analytics
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public int TotalStorageUsed { get; set; } = 0;
    public int FileCount { get; set; } = 0;
    public DateTime LastAnalyzed { get; set; } = DateTime.UtcNow;
    public Guid FolderId { get; set; }
    
    
    //Foreign Keys
    [ForeignKey("FolderId")]
    public virtual Folders Folder { get; set; }
    
    //Relationship One to One Navigation
    public virtual Files File { get; set; }
}