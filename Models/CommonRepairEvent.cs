namespace RailwayDBKurs.Models
{
    public class CommonRepairEvent
    {
        public int ID { get; set; }
        public int? DepotID { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = "В очереди";
        public string? Description { get; set; } = string.Empty;
    }
}
