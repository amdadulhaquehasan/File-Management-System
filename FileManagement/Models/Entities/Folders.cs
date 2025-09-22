using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;

[Table("Folders", Schema = "dbo")]
public class Folders
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? ParentFolderId { get; set; } = null;
    public Guid OwnerId { get; set; }
    
    
    //Foreign Keys
    [ForeignKey("OwnerId")]
    public virtual Users Owner{ get; set; }
    
    [ForeignKey("ParentFolderId")]
    public virtual Folders? ParentFolder { get; set; }
    
    
    //Relationship One to Many Navigation
    public virtual ICollection<Files> Files { get; set; } = new List<Files>();
    
    public virtual ICollection<Folders> SubFolders { get; set; } = new List<Folders>();
    
    public virtual ICollection<Analytics> Analytics { get; set; } = new List<Analytics>();
}