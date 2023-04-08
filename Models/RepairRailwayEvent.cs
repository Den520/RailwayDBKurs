namespace RailwayDBKurs.Models
{
    public class RepairRailwayEvent
    {
        public int ID { get; set; }
        public int RegionID { get; set; }
        public int? BeginPoint { get; set; }
        public int? EndPoint { get; set; }
    }
}
