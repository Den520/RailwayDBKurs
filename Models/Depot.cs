namespace RailwayDBKurs.Models
{
    public class Depot
    {
        public int ID { get; set; }
        public int RegionID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int ValueOfEmployees { get; set; }
        public int ValueOfVans { get; set; }
        public string? Director { get; set; } = string.Empty;
    }
}
