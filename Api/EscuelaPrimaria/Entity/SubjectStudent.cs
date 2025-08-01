namespace EscuelaPrimaria.Entity
{
    public partial class SubjectStudent :  Entity
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }
        public string? Trimestre { get; set; }
        public string? Year { get; set; }
        public bool? Active { get; set; }
        public decimal? Score { get; set; }
        public DateTime Date { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
