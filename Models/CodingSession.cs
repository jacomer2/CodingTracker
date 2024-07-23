using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Models
{
    internal class CodingSession
    {
        public int Id { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime {  get; set; }
        public int Duration { get; set; }

        public CodingSession(int id, string? startTime, string? endTime, int duration)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
        }

        public CodingSession(string? startTime, string? endTime, int duration)
        {
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
        }

        public CodingSession() { }
    }
}
