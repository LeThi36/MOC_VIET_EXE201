using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Workshop
{
    public class CreateWorkshopDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? LocationId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MaxParticipants { get; set; }
        public decimal? Fee { get; set; }
    }
}
