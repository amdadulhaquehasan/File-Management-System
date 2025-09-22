using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace FileManagement.Models.Entities;

[Table("Users", Schema = "dbo")]
public class Users
{
    [Key]
    [Column("Id")]
    public required Guid Id { get; set; }
    
    [MaxLength(100)]
    public required string UserName { get; set; }
    
    [MaxLength(100)]
    public required string Email { get; set; }
    
    public required string PasswordHash { get; set; }
    
    [MaxLength(50)]
    public required string Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; } = true;
    
    
    //Relationship One to Many Navigation
    public virtual ICollection<Files> Files { get; set; } = new List<Files>();
    
    public virtual ICollection<Folders> Folders { get; set; } = new List<Folders>();
    
    public virtual ICollection<FileSecurity>  FileSecuritys { get; set; } = new List<FileSecurity>();
    
    public virtual ICollection<TrashBin> TrashBins { get; set; } = new List<TrashBin>();
    
    public virtual ICollection<SyncLogs> SyncLogs { get; set; } = new List<SyncLogs>();
    
    public virtual ICollection<FileVersions> FileVersions { get; set; } = new List<FileVersions>();
}