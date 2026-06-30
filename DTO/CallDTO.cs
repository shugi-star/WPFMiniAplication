namespace DTO
{
    public class CallDTO
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public int? IdVolunteer { get; set; }

    }
}
