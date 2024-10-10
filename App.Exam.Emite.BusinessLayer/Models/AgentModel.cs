using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.BusinessLayer.Models
{
    public class AgentModel
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
