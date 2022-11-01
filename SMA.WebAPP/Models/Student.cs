namespace SMA.WebAPP.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MathsMarks { get; set; }
        public int ScienceMarks { get; set; }
        public int EnglishMarks { get; set; }
        public int Total { get; set; }
        public float Percentage { get; set; }


    }
}
