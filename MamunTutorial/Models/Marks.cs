using System.ComponentModel.DataAnnotations;

namespace MamunTutorial.Models
{
    public class Marks
    {
        [Key]
        public Guid MarksId { get; set; }
        public string Class { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public Guid StudentId { get; set; }

        public string TeacherName { get; set; }
    }
}
