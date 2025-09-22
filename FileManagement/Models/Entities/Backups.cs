using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;

[Table("Backups", Schema = "dbo")]
public class Backups
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public DateTime BackupTime { get; set; } = DateTime.UtcNow;
    
    [MaxLength(100)]
    public required string BackupLocation { get; set; }
    public bool Restored { get; set; } = false;
    
    
    //Foreign Keys
    [ForeignKey("FileId")]
    public virtual Files File { get; set; }
}