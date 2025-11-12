namespace NowBoard.Data.NowBoard
{
    public class DepartureInfo
    {
        public DateTime TimetabledTime { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string Line { get; set; } = string.Empty;
        public TimeSpan Hinweis { get; set; }
        public string Station { get; set; } = string.Empty;
    }
}