using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMADapper.WebAPI.Models;
using System.Data.SqlClient;

namespace SMADapper.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentMark>>> GetAllStudents()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("sqlConnection"));
            var studs = await GetStudData(connection);
            return Ok(studs);
        }

        private static Task<IEnumerable<StudentMark>> GetStudData(SqlConnection connection)
        {
            return connection.QueryAsync<StudentMark>("Select Id,Name,MathsMarks,ScienceMarks,EnglishMarks,(MathsMarks + ScienceMarks + EnglishMarks) As Total," +
                            "ROUND((MathsMarks + ScienceMarks + EnglishMarks) * 100.0 / 300, 1) AS percentage from studentMarks");
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<StudentMark>>> GetById(Guid studId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("sqlConnection"));
            var studs = await connection.QueryAsync<StudentMark>("Select Id,Name,MathsMarks,ScienceMarks,EnglishMarks,(MathsMarks + ScienceMarks + EnglishMarks) As Total," +
                "ROUND((MathsMarks + ScienceMarks + EnglishMarks) * 100.0 / 300, 1) AS percentage from studentMarks where id=@Id",
                new {Id= studId });
            return Ok(studs);
        }

        [HttpPost]
        public async Task<ActionResult<List<StudentAdd>>> AddUpdateStudent(StudentAdd mark)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("sqlConnection"));
            await connection.ExecuteAsync("insert into studentMarks (Id,Name,MathsMarks,ScienceMarks,EnglishMarks)" +
                " values (@Id,@Name,@MathsMarks,@ScienceMarks,@EnglishMarks)",mark);
            return Ok(await GetStudData(connection));
        }
        [HttpPut]
        public async Task<ActionResult<List<StudentAdd>>> UpdateStudent(StudentAdd mark)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("sqlConnection"));
            await connection.ExecuteAsync("update studentMarks set Name=@Name,MathsMarks=@MathsMarks," +
                "ScienceMarks=@ScienceMarks,EnglishMarks=@EnglishMarks where Id =@Id ", mark);
            return Ok(await GetStudData(connection));
        }

        private static async Task<IEnumerable<StudentMark>> SelectAllStudents(SqlConnection connection)
        {
            return await connection.QueryAsync<StudentMark>("Select Id,Name,MathsMarks,ScienceMarks,EnglishMarks,(MathsMarks + ScienceMarks + EnglishMarks) As Total," +
                "ROUND((MathsMarks + ScienceMarks + EnglishMarks) * 100.0 / 300, 1) AS percentage from studentMarks");
        }
    }
}
