namespace EscuelaPrimaria
{
    using System.Collections.Generic;
    using EscuelaPrimaria.Entity;
    using Microsoft.EntityFrameworkCore;

    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<SubjectStudent> SubjectStudent { get; set; }
        public DbSet<Attendence> Attendence { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // StudentSubject: muchos a muchos
            modelBuilder.Entity<Subject>().HasKey(e => e.Id);
            modelBuilder.Entity<Subject>().Property(a => a.Id).ValueGeneratedOnAdd();  
            modelBuilder.Entity<Subject>().Property(s => s.Name);
            modelBuilder.Entity<Subject>().Property(s => s.CreatedAt);
            modelBuilder.Entity<Subject>().Property(s => s.CreatedBy);
            modelBuilder.Entity<Subject>().Property(s => s.UpdatedAt);
            modelBuilder.Entity<Subject>().Property(s => s.UpdatedBy);



            modelBuilder.Entity<Student>().HasKey(e => e.Id);
            modelBuilder.Entity<Student>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Student>().Property(s => s.Name);
            modelBuilder.Entity<Student>().Property(s => s.Gender);
            modelBuilder.Entity<Student>().Property(s => s.Age);
            modelBuilder.Entity<Student>().Property(s => s.Active);
            modelBuilder.Entity<Student>().Property(s => s.LastName);
            modelBuilder.Entity<Student>().Property(s => s.Phone);
            modelBuilder.Entity<Student>().Property(s => s.Tutor);
            modelBuilder.Entity<Student>().Property(s => s.TutorRelationShip);
            modelBuilder.Entity<Student>().Property(s => s.TutorPhone);
            modelBuilder.Entity<Student>().Property(s => s.CreatedAt);
            modelBuilder.Entity<Student>().Property(s => s.CreatedBy);
            modelBuilder.Entity<Student>().Property(s => s.UpdatedAt);
            modelBuilder.Entity<Student>().Property(s => s.UpdatedBy);

            modelBuilder.Entity<SubjectStudent>().HasKey(e => e.Id);
            modelBuilder.Entity<SubjectStudent>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SubjectStudent>()
                .HasKey(e => new { e.StudentId, e.SubjectId });
            modelBuilder.Entity<SubjectStudent>().Property(ss => ss.Score).HasPrecision(5, 2);
            modelBuilder.Entity<SubjectStudent>().Property(s => s.Year);
            modelBuilder.Entity<SubjectStudent>().Property(s => s.Trimestre);
            modelBuilder.Entity<SubjectStudent>().Property(s => s.CreatedAt);
            modelBuilder.Entity<SubjectStudent>().Property(s => s.CreatedBy);
            modelBuilder.Entity<SubjectStudent>().Property(s => s.UpdatedAt);
            modelBuilder.Entity<SubjectStudent>().Property(s => s.UpdatedBy);
            modelBuilder.Entity<SubjectStudent>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.SubjectStudents)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<SubjectStudent>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.SubjectStudents)
                .HasForeignKey(ss => ss.SubjectId);

            modelBuilder.Entity<Attendence>().HasKey(e => e.Id);
            modelBuilder.Entity<Attendence>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Attendence>();
            modelBuilder.Entity<Attendence>()
                        .HasOne(a => a.Student)
                        .WithMany(s => s.Attendences)
                        .HasForeignKey(a => a.StudentId);
            modelBuilder.Entity<Attendence>().Property(s => s.CreatedAt);
            modelBuilder.Entity<Attendence>().Property(s => s.CreatedBy);
            modelBuilder.Entity<Attendence>().Property(s => s.UpdatedAt);
            modelBuilder.Entity<Attendence>().Property(s => s.UpdatedBy);


        }

    }
}
