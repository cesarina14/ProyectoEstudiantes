namespace EscuelaPrimaria.Entity
{
    public class Grade : Entity
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public long Score { get; set; } 
        public DateTime Date { get; set; }

        public Student Student { get; set; } = new();
    }
}
