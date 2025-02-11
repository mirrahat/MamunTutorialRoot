using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MamunTutorial.Models
{
    public class Attendance
    {
        [Key]
        public Guid AttendenceId { get; set; }
        public string Class { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }

        public Guid StudentId { get; set; }


    }

}
