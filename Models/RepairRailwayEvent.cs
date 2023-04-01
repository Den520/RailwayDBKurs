namespace RailwayDBKurs.Models
{
    public class RepairRailwayEvent
    {
        public int ID { get; set; }
        public int RegionID { get; set; }
        public double? BeginPoint { get; set; }
        public double? EndPoint { get; set; }
    }
}
