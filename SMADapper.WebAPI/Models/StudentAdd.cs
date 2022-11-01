namespace SMADapper.WebAPI.Models
{
    public class StudentAdd
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public int MathsMarks { get; set; } = 0;
        public int ScienceMarks { get; set; } = 0;
        public int EnglishMarks { get; set; } = 0;
    }
}
