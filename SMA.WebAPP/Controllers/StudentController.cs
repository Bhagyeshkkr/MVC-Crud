using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMA.WebAPP.Models;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace SMA.WebAPP.Controllers
{
    public class StudentController : Controller
    {
        string baseUrl = "https://localhost:7158/";

        HttpClientHandler _clientHandler = new HttpClientHandler();

        Student _oStudent = new Student();
        List<Student> _oStudents = new List<Student>();

        //public StudentController()
        //{
        //    _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //}

        //public async Task<IActionResult> Index()
        //{
        //    DataTable dt = new DataTable();
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage getdata = await client.GetAsync("api/Student");
        //        if (getdata.IsSuccessStatusCode)
        //        {

        //            string result = getdata.Content.ReadAsStringAsync().Result;
        //            dt = JsonConvert.DeserializeObject<DataTable>(result);

        //        }
        //        else
        //        {
        //            Console.WriteLine("Error");
        //        }
        //        ViewData.Model = dt;
        //    }
        //    return View(dt);
        //}

        public async Task<IActionResult> Index()
        {
            DataTable dt = new DataTable();
            IList<Student> students = new List<Student>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getdata = await client.GetAsync("api/Student");
                if (getdata.IsSuccessStatusCode)
                {

                    string result = getdata.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<List<Student>>(result);

                }
                else
                {
                    Console.WriteLine("Error");
                }
                ViewData.Model = students;
            }
            return View(students);
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IEnumerable<Student>> GetAllStudents()
        //{
        //    _oStudents = new List<Student>();

        //    using(var httpClient = new HttpClient(_clientHandler))
        //    {
        //        using(var response = await httpClient.GetAsync("https://localhost:7158/api/Student"))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            _oStudents = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
        //        }
        //    }

        //    return _oStudents;
        //}

        //[HttpGet]
        //public async Task<Student> GetById( int studentId)
        //{
        //    _oStudent = new Student();

        //    using (var httpClient = new HttpClient(_clientHandler))
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:7158/api/Student" + studentId))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
        //        }
        //    }

        //    return _oStudent;
        //}

        //[HttpPost]
        //public async Task<Student> AddUpdateStudent(Student student)
        //{
        //    _oStudent = new Student();

        //    using (var httpClient = new HttpClient(_clientHandler))
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(student),Encoding.UTF8,"application/json");
        //        using (var response = await httpClient.PostAsync("https://localhost:7158/api/Student" , content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
        //        }
        //    }

        //    return _oStudent;
        //}
        //[HttpDelete]
        //public async Task<string> Delete(int studentId)
        //{

        //    string message = "";
        //    using (var httpClient = new HttpClient(_clientHandler))
        //    {
        //        using (var response = await httpClient.DeleteAsync("https://localhost:7158/api/Student" + studentId))
        //        {
        //            message = await response.Content.ReadAsStringAsync();
        //        }
        //    }

        //    return message;
        //}

        [HttpPost]
        public async Task<IActionResult> Add(Student addEmployeeRequest)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                MathsMarks = addEmployeeRequest.MathsMarks,
                ScienceMarks = addEmployeeRequest.ScienceMarks,
                EnglishMarks = addEmployeeRequest.EnglishMarks

            };
            if(student!=null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl + "AddUpdate/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage getdata = await client.PostAsJsonAsync<Student>("api/Student/AddUpdate",student);
                    if (getdata.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index","Student");

                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }

            }
            
            return View();
        }



    }
}
