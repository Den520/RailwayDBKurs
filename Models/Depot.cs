namespace RailwayDBKurs.Models
{
    public class Depot
    {
        public int ID { get; set; }
        public int RegionID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int ValueOfEmployees { get; set; } = 0;
        public int ValueOfVans { get; set; } = 0;
        public string? Director { get; set; } = string.Empty;
    }
}
