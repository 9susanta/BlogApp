using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlogApp.Models
{
    public partial class blogdbContext : DbContext
    {
        
        public blogdbContext(DbContextOptions<blogdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Homepagedatum> Homepagedata { get; set; }
        public virtual DbSet<Latestnews> Latestnews { get; set; }
        public virtual DbSet<Latestphoto> Latestphotos { get; set; }
        public virtual DbSet<Newspost> Newsposts { get; set; }
        public virtual DbSet<Newstagmap> Newstagmaps { get; set; }
        public virtual DbSet<Popularnews> Popularnews { get; set; }
        public virtual DbSet<Relatednews> Relatednews { get; set; }
        public virtual DbSet<Schdulepostconfig> Schdulepostconfigs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Tblcontact> Tblcontacts { get; set; }
        public virtual DbSet<Tblnewstype> Tblnewstypes { get; set; }
        public virtual DbSet<Tblright> Tblrights { get; set; }
        public virtual DbSet<Tblrole> Tblroles { get; set; }
        public virtual DbSet<Tbluser> Tblusers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Homepagedatum>(entity =>
            {
                entity.ToTable("homepagedata");

                entity.Property(e => e.EngShortDesc).HasMaxLength(500);

                entity.Property(e => e.EnglishTitle).HasMaxLength(150);

                entity.Property(e => e.HeaderImageName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Odcontent)
                    .HasMaxLength(0)
                    .HasColumnName("ODContent");

                entity.Property(e => e.OdiaTitle).HasMaxLength(150);

                entity.Property(e => e.OdshortDesc)
                    .HasMaxLength(500)
                    .HasColumnName("ODShortDesc");

                entity.Property(e => e.PostedDate).HasColumnType("date");

                entity.Property(e => e.PostedOn).HasMaxLength(6);

                entity.Property(e => e.SeoMeta).HasMaxLength(500);

                entity.Property(e => e.SlugUrl).HasMaxLength(200);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Thumbnail210).HasMaxLength(50);

                entity.Property(e => e.Thumbnail279).HasMaxLength(50);

                entity.Property(e => e.Thumbnail86).HasMaxLength(50);
            });

            modelBuilder.Entity<Latestnews>(entity =>
            {
                entity.ToTable("latestnews");

                entity.Property(e => e.EngShortDesc).HasMaxLength(500);

                entity.Property(e => e.EnglishTitle).HasMaxLength(150);

                entity.Property(e => e.HeaderImageName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Odcontent)
                    .HasMaxLength(0)
                    .HasColumnName("ODContent");

                entity.Property(e => e.OdiaTitle).HasMaxLength(150);

                entity.Property(e => e.OdshortDesc)
                    .HasMaxLength(500)
                    .HasColumnName("ODShortDesc");

                entity.Property(e => e.PostedDate).HasColumnType("date");

                entity.Property(e => e.PostedOn).HasMaxLength(6);

                entity.Property(e => e.SeoMeta).HasMaxLength(500);

                entity.Property(e => e.SlugUrl).HasMaxLength(200);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Thumbnail210).HasMaxLength(50);

                entity.Property(e => e.Thumbnail279).HasMaxLength(50);

                entity.Property(e => e.Thumbnail86).HasMaxLength(50);
            });

            modelBuilder.Entity<Latestphoto>(entity =>
            {
                entity.ToTable("latestphoto");

                entity.Property(e => e.EngShortDesc).HasMaxLength(500);

                entity.Property(e => e.EnglishTitle).HasMaxLength(150);

                entity.Property(e => e.HeaderImageName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Odcontent)
                    .HasMaxLength(0)
                    .HasColumnName("ODContent");

                entity.Property(e => e.OdiaTitle).HasMaxLength(150);

                entity.Property(e => e.OdshortDesc)
                    .HasMaxLength(500)
                    .HasColumnName("ODShortDesc");

                entity.Property(e => e.PostedDate).HasColumnType("date");

                entity.Property(e => e.PostedOn).HasMaxLength(6);

                entity.Property(e => e.SeoMeta).HasMaxLength(500);

                entity.Property(e => e.SlugUrl).HasMaxLength(200);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Thumbnail210).HasMaxLength(50);

                entity.Property(e => e.Thumbnail279).HasMaxLength(50);

                entity.Property(e => e.Thumbnail86).HasMaxLength(50);
            });

            modelBuilder.Entity<Newspost>(entity =>
            {
                entity.ToTable("newspost");

                entity.Property(e => e.EngShortDesc).HasMaxLength(500);

                entity.Property(e => e.EnglishTitle).HasMaxLength(150);

                entity.Property(e => e.FacebookImage).HasMaxLength(200);

                entity.Property(e => e.HeaderImageName).HasMaxLength(50);

                entity.Property(e => e.ImageCopyRight).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Odcontent)
                    .HasMaxLength(0)
                    .HasColumnName("ODContent");

                entity.Property(e => e.OdiaTitle).HasMaxLength(150);

                entity.Property(e => e.OdshortDesc)
                    .HasMaxLength(500)
                    .HasColumnName("ODShortDesc");

                entity.Property(e => e.PostedDate).HasColumnType("date");

                entity.Property(e => e.PostedOn).HasMaxLength(6);

                entity.Property(e => e.SeoMeta).HasMaxLength(500);

                entity.Property(e => e.SlugUrl).HasMaxLength(200);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Thumbnail210).HasMaxLength(50);

                entity.Property(e => e.Thumbnail279).HasMaxLength(50);

                entity.Property(e => e.Thumbnail86).HasMaxLength(50);
            });

            modelBuilder.Entity<Newstagmap>(entity =>
            {
                entity.ToTable("newstagmap");

                entity.Property(e => e.PostId).HasColumnName("Post_Id");

                entity.Property(e => e.TagId).HasColumnName("Tag_Id");
            });

            modelBuilder.Entity<Popularnews>(entity =>
            {
                entity.ToTable("popularnews");

                entity.Property(e => e.EngShortDesc).HasMaxLength(500);

                entity.Property(e => e.EnglishTitle).HasMaxLength(150);

                entity.Property(e => e.HeaderImageName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Odcontent)
                    .HasMaxLength(0)
                    .HasColumnName("ODContent");

                entity.Property(e => e.OdiaTitle).HasMaxLength(150);

                entity.Property(e => e.OdshortDesc)
                    .HasMaxLength(500)
                    .HasColumnName("ODShortDesc");

                entity.Property(e => e.PostedDate).HasColumnType("date");

                entity.Property(e => e.PostedOn).HasMaxLength(6);

                entity.Property(e => e.SeoMeta).HasMaxLength(500);

                entity.Property(e => e.SlugUrl).HasMaxLength(200);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Thumbnail210).HasMaxLength(50);

                entity.Property(e => e.Thumbnail279).HasMaxLength(50);

                entity.Property(e => e.Thumbnail86).HasMaxLength(50);
            });

            modelBuilder.Entity<Relatednews>(entity =>
            {
                entity.ToTable("relatednews");

                entity.Property(e => e.EngShortDesc).HasMaxLength(500);

                entity.Property(e => e.EnglishTitle).HasMaxLength(150);

                entity.Property(e => e.HeaderImageName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Odcontent)
                    .HasMaxLength(0)
                    .HasColumnName("ODContent");

                entity.Property(e => e.OdiaTitle).HasMaxLength(150);

                entity.Property(e => e.OdshortDesc)
                    .HasMaxLength(500)
                    .HasColumnName("ODShortDesc");

                entity.Property(e => e.PostedDate).HasColumnType("date");

                entity.Property(e => e.PostedOn).HasMaxLength(6);

                entity.Property(e => e.SeoMeta).HasMaxLength(500);

                entity.Property(e => e.SlugUrl).HasMaxLength(200);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Thumbnail210).HasMaxLength(50);

                entity.Property(e => e.Thumbnail279).HasMaxLength(50);

                entity.Property(e => e.Thumbnail86).HasMaxLength(50);
            });

            modelBuilder.Entity<Schdulepostconfig>(entity =>
            {
                entity.ToTable("schdulepostconfig");

                entity.Property(e => e.PostedOn).HasMaxLength(6);

                entity.Property(e => e.ScheduleTime).HasMaxLength(6);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UrlSlug).HasMaxLength(50);
            });

            modelBuilder.Entity<Tblcontact>(entity =>
            {
                entity.ToTable("tblcontact");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(350);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PostedOn).HasMaxLength(6);
            });

            modelBuilder.Entity<Tblnewstype>(entity =>
            {
                entity.ToTable("tblnewstype");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.NewsType).HasMaxLength(50);

                entity.Property(e => e.OdiaName).HasMaxLength(50);
            });

            modelBuilder.Entity<Tblright>(entity =>
            {
                entity.HasKey(e => e.RightsId)
                    .HasName("PRIMARY");

                entity.ToTable("tblrights");

                entity.Property(e => e.RightsName).HasMaxLength(150);
            });

            modelBuilder.Entity<Tblrole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("tblrole");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<Tbluser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("tbluser");

                entity.Property(e => e.DateCreate).HasMaxLength(6);

                entity.Property(e => e.DateUpdate).HasMaxLength(6);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.ImageName).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
