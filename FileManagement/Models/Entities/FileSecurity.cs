using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;

[Table("FileSecurity", Schema = "dbo")]
public class FileSecurity
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public Guid FileId { get; set; }

    [MaxLength(50)] 
    public required string PermissionLevel { get; set; } = "read";
    
    [MaxLength(50)]
    public Guid SharedWithUserId { get; set; }
    
    
    //Foreign Keys
    [ForeignKey("FileId")]
    public virtual Files File { get; set; }
    
    [ForeignKey("SharedWithUserId")]
    public virtual Users SharedWithUser { get; set; }
    
}