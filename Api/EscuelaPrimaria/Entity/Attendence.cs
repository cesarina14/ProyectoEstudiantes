namespace EscuelaPrimaria.Entity
{
    public partial class Attendence :Entity
    {

        public long StudentId { get; set; }
        public bool Present { get; set; }

        public DateTime Date { get; set; }

    
        public  Student Student { get; set; }
    }
}
