namespace RailwayDBKurs.Models
{
    public class Region
    {
        public int ID { get; set; }
        public double Length { get; set; }
        public DateTime? DateOfExploitation { get; set; }
        public int ValueOfTrains { get; set; } = 0;
    }
}
