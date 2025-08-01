namespace EscuelaPrimaria.Model
{
    public  abstract class IDto
    {
       public long Id { get; set; } = 0;
       public bool? Active { get; set; }
        public string? CreatedBy { get; set; } = "Test";
        public string? UpdatedBy { get; set; } ="Test";
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
