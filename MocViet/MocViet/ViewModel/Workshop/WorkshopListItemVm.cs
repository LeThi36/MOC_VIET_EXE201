namespace MocViet.ViewModel.Workshop
{
    public class WorkshopListItemVm
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Fee { get; set; }
        public string HostName { get; set; } = null!;
        public string? LocationAddress { get; set; }
        public string Status { get; set; } = null!;
    }
}
