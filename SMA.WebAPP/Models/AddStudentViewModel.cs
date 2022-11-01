namespace SMA.WebAPP.Models
{
    public class AddStudentViewModel
    {
        public string Name { get; set; }
        public int MathsMarks { get; set; }
        public int ScienceMarks { get; set; }
        public int EnglishMarks { get; set; }
        public IEnumerable<Student> students { get; set; }
    }
}
