namespace FinishHim_.Models
{
    public class ManagerModel
    {
        public string TeamName { get; set; }
        public int TeamNumber { get; set; }
        public double TeamPrice { get; set; }
        public int TeamAge { get; set; }
        public DateTime CreateDate { get; set; }
        public int PlayerAge { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public string PlayerEmail { get; set; }
        public int PlayerTeamNumber { get; set; }
        public int TeamId { get; internal set; }
    }
}
