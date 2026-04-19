namespace ChesedEntities
{
    public class Call
    {
        public int Id { get; set; }
        public Category category { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public int VolunteerId { get; set; }
        public Call()
        {
            Id = Configuration.NextCalld++;
            OpenDate = Configuration.Clock;
        }
        public override string ToString()
        {
            return $"idCall: {Id} Category: {ReverseString(category.Name)}, " +
           $"Description: {Description}, " +
           $"Address: {ReverseString(Address)}, " +
           $"OpenDate: {OpenDate}, TargetDate: {TargetDate}";
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            if (s[0] >= 'א' && s[0] <= 'ת')
            {
                Array.Reverse(arr);
            }
            return new string(arr);
        }
    }
}