using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App.Exam.Emite.DataLayer.Entities
{
    public class Agent
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneExtension { get; set; }
        public AgentStatus Status { get; set; }
    }
    public enum AgentStatus
    {
        Available,
        Busy,
        Offline

    }
}