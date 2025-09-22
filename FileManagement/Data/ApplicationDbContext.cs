using FileManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileManagement.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    } 
    
    public DbSet<Analytics> Analytics { get; set; }
    public DbSet<Files> Files { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Folders> Folders { get; set; }
    public DbSet<FileSecurity>  FileSecurity { get; set; }
    public DbSet<FileVersions> FileVersions { get; set; }
    public DbSet<FileRecommendations> FileRecommendations { get; set; }
    public DbSet<Backups> Backups { get; set; }
    public DbSet<SyncLogs> SyncLogs { get; set; }
    public DbSet<TrashBin>  TrashBins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(u => u.IsActive).HasDefaultValue(true);
            entity.Property(u => u.CreatedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<Files>(entity =>
        {
            entity.Property(f => f.Type).HasDefaultValue("unknown");
            entity.Property(f=> f.Size).HasDefaultValue(0);
            entity.Property(f => f.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(f => f.IsArchived).HasDefaultValue(false);
            entity.Property(f => f.IsDeleted).HasDefaultValue(false);
            entity.Property(f => f.IsFavourite).HasDefaultValue(false);
            entity.Property(f => f.StorageLocation).HasDefaultValue("local");
            
            entity.HasOne(f => f.Owner).WithMany(u => u.Files).HasForeignKey(f => 
                f.OwnerId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(f => f.Analytics).WithOne(a => a.File).HasForeignKey<Files>(f =>
                f.AnalyticsId).OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(f=> f.ParentFile).WithMany(f => f.SubFiles).HasForeignKey(f=> 
                f.ParentFileId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(f=> f.Folder).WithMany(fo => fo.Files).HasForeignKey(f=> 
                f.FolderId).OnDelete(DeleteBehavior.Cascade);

        });
        
        modelBuilder.Entity<Folders>(entity =>
        {
            entity.Property(fo=> fo.CreatedAt).HasDefaultValueSql("now()");
            
            entity.HasOne(fo => fo.Owner).WithMany(u=> u.Folders).HasForeignKey(fo=>
                fo.OwnerId).OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(fo => fo.ParentFolder).WithMany(fo=> fo.SubFolders).HasForeignKey(fo=> 
                fo.ParentFolderId).OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Analytics>(entity =>
        {
            entity.Property(a=> a.TotalStorageUsed).HasDefaultValue(0);
            entity.Property(a=> a.FileCount).HasDefaultValue(0);
            entity.Property(a=> a.LastAnalyzed).HasDefaultValueSql("now()");
            
            entity.HasOne(a=> a.Folder).WithMany(fo => fo.Analytics).HasForeignKey(a=> 
                a.FolderId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FileSecurity>(entity =>
        {
            entity.Property(fse => fse.PermissionLevel).HasDefaultValue("read");
            
            entity.HasOne(fse => fse.File).WithMany(f => f.FileSecuritys).HasForeignKey(fse => 
                fse.FileId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(fse => fse.SharedWithUser).WithMany(u=> u.FileSecuritys).HasForeignKey(fse => 
                fse.SharedWithUserId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FileVersions>(entity =>
        {
            entity.Property(fve => fve.CreatedAt).HasDefaultValueSql("now()");
            
            entity.HasOne(fve => fve.File).WithMany(f=> f.FileVersions).HasForeignKey(fve => 
                fve.FileId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(fve => fve.Created).WithMany(u=> u.FileVersions).HasForeignKey(fve => 
                fve.CreatedBy).OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<FileRecommendations>(entity =>
        {
            entity.Property(fre => fre.RecommendationAt).HasDefaultValueSql("now()");
            entity.Property(fre => fre.IsUnused).HasDefaultValue(false);
            
            entity.HasOne(fre=> fre.File).WithMany(f=> f.FileRecommendations).HasForeignKey(fre=> 
                fre.FileId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Backups>(entity =>
        {
            entity.Property(ba=> ba.BackupTime).HasDefaultValueSql("now()");
            entity.Property(ba=> ba.Restored).HasDefaultValue(false);
            
            entity.HasOne(ba=> ba.File).WithMany(f=> f.Backups).HasForeignKey(ba=>
                ba.FileId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<SyncLogs>(entity =>
        {
            entity.Property(sy => sy.SyncTime).HasDefaultValueSql("now()");
            entity.Property(sy => sy.Status).HasDefaultValue("Pending");

            entity.HasOne(sy => sy.File).WithMany(f => f.SyncLogs).HasForeignKey(sy =>
                sy.FileId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(sy => sy.User).WithMany(u => u.SyncLogs).HasForeignKey(sy =>
                sy.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TrashBin>(entity =>
        {
            entity.Property(trb=> trb.DeletedAt).HasDefaultValueSql("now()");
            entity.Property(trb=> trb.Restored).HasDefaultValue(false);
            
            entity.HasOne(trb=> trb.File).WithMany(f=> f.TrashBins).HasForeignKey(trb=> 
                trb.FileId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(trb=> trb.Deleted).WithMany(u=> u.TrashBins).HasForeignKey(trb=> 
                trb.DeletedBy).OnDelete(DeleteBehavior.Restrict);
        });
    }
}