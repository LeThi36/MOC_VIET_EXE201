namespace MocViet.ViewModel.Workshop
{
    public class WorkshopDetailsVm
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MaxParticipants { get; set; }
        public decimal? Fee { get; set; }
        public string HostName { get; set; } = null!;
        public string? LocationAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = null!;
    }
}
