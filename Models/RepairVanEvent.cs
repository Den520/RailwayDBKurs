namespace RailwayDBKurs.Models
{
    public class RepairVanEvent
    {
        public int ID { get; set; }
        public int? VanID { get; set; }
        public string ListOfRepairedParts { get; set; } = string.Empty;
    }
}
