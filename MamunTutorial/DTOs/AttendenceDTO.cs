namespace MamunTutorial.DTOs
{
    public class AttendanceDTO
    {
        public Guid StudentId { get; set; }
        public bool IsPresent { get; set; }
        public string SelectedClass { get; set; }
    }

}
