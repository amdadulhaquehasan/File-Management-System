using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace FileManagement.Models.Entities;

[Table("Files", Schema = "dbo")]
public class Files
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(100)]
    public required string Type { get; set; } ="unknown";
    public int Size { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid OwnerId { get; set; }
    public bool IsArchived { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public bool IsFavourite { get; set; } = false;
    
    [MaxLength(100)]
    public required string StorageLocation { get; set; } ="local";
    public Guid? AnalyticsId { get; set; }
    public Guid? ParentFileId { get; set; }
    public Guid? FolderId { get; set; }
    
    
    //Foreign Keys
    [ForeignKey("OwnerId")]
    public virtual Users Owner { get; set; }
    
    [ForeignKey("AnalyticsId")]
    public virtual Analytics Analytics { get; set; }
    
    [ForeignKey("ParentFileId")]
    public virtual Files ParentFile { get; set; }
    
    [ForeignKey("FolderId")]
    public virtual Folders Folder { get; set; }
    
    
    // Relationship One to Many Navigation
    public virtual ICollection<Files> SubFiles { get; set; } = new List<Files>();
    
    public virtual ICollection<FileSecurity> FileSecuritys { get; set; } = new List<FileSecurity>();
    
    public virtual ICollection<TrashBin> TrashBins { get; set; } = new List<TrashBin>();
    
    public virtual ICollection<FileRecommendations> FileRecommendations { get; set; } = new List<FileRecommendations>();
    
    public virtual ICollection<Backups> Backups { get; set; } = new List<Backups>();
    
    public virtual ICollection<SyncLogs> SyncLogs { get; set; } = new List<SyncLogs>();
    
    public virtual ICollection<FileVersions> FileVersions { get; set; } = new List<FileVersions>();
}