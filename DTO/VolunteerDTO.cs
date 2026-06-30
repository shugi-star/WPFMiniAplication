namespace DTO
{
    public class VolunteerDTO
    {
        public bool Role { get; set; }
        //{ volunteer = 1, manager = 0 }
        public int Id { get; set; }
        public string Tz { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double MaxDistance { get; set; }
       //האם חייב רשימת קריאות?
        //public List<CallDTO> ListCalls { get; set; }
    }
}
