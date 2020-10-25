namespace AlgorithmicThinking.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FeedbackModel : DbContext
    {
        public FeedbackModel()
            : base("name=FeedbackModel")
        {
        }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>()
                .Property(e => e.Title)
                .IsUnicode(false);
        }
    }
}
