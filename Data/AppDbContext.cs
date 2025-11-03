using AdHoc_SpeechSynthesizer.Models;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TtsModel> TtsModels => Set<TtsModel>();
        public DbSet<TtsVoice> TtsVoices => Set<TtsVoice>();
        public DbSet<MessageTemplate> MessageTemplates => Set<MessageTemplate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TtsModel>(entity =>
            {
                entity.ToTable("TtsModel", schema: "dbo");
                entity.HasKey(x => x.ModelId);
                entity.Property(x => x.Provider).HasMaxLength(32).IsRequired();
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<TtsVoice>(entity =>
            {
                entity.ToTable("TtsVoice", schema: "dbo");
                entity.HasKey(x => x.VoiceId);
                entity.Property(x => x.Provider).HasMaxLength(32).IsRequired();
                entity.Property(x => x.ProviderVoiceId).HasMaxLength(200).IsRequired();
                entity.Property(x => x.DisplayName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Locale).HasMaxLength(20).IsRequired();

                entity.HasOne(x => x.Model)
                      .WithMany(x => x.Voices)
                      .HasForeignKey(x => x.ModelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MessageTemplate>(entity =>
            {
                entity.ToTable("MessageTemplate", schema: "dbo");
                entity.HasKey(x => x.TemplateId);
                entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.Property(x => x.SSMLContent).HasMaxLength(500).IsRequired();
            });
        }
    }
}
