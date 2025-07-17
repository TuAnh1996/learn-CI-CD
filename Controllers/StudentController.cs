// Controllers/StudentController.cs
using Microsoft.AspNetCore.Mvc;
using UserManagerDemo.Models; // Đảm bảo đúng namespace của Model
using UserManagerDemo.Data;

using Microsoft.EntityFrameworkCore; // Cần thêm dòng này cho ToListAsync()
using System.Threading.Tasks;
using System.Collections.Generic; // Cần thêm dòng này cho IEnumerable<Student>

namespace UserManagerDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Student/Create
        // Hiển thị form để thêm sinh viên mới
        public IActionResult Create()
        {
            return View(); // Trả về View có form thêm sinh viên
        }

        // POST: Student/Create
        // Xử lý dữ liệu khi form được submit
        [HttpPost]
        [ValidateAntiForgeryToken] // Rất quan trọng để ngăn chặn tấn công CSRF
        public async Task<IActionResult> Create(Student student)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(student));
            // Kiểm tra xem dữ liệu Model có hợp lệ theo Data Annotations không
            if (ModelState.IsValid)
            {
                // TODO: Tại đây, bạn sẽ thực hiện logic để lưu đối tượng student vào cơ sở dữ liệu
                // Ví dụ: _dbContext.Students.Add(student); _dbContext.SaveChanges();
                _context.Add(student); // Thêm sinh viên vào DbContext
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                TempData["SuccessMessage"] = "学生追加が完了しました!";

                // Sau khi lưu thành công, chuyển hướng đến một trang khác (ví dụ: danh sách sinh viên)
                return RedirectToAction("Index"); // Giả sử có một action Index để hiển thị danh sách
            }

            // Nếu dữ liệu không hợp lệ, quay lại View với các lỗi validation
            return View(student);
        }

        // GET: Student/Index (Ví dụ về một action để hiển thị danh sách sinh viên)
        // public async Index()
        // {
        //     // TODO: Lấy danh sách sinh viên từ DB và truyền vào View
        //     var students = new List<Student>
        //     {
        //         new Student { Id = 1, Name = "Nguyen Van A", Age = 20, Email = "a@example.com", Gender = "Male" },
        //         new Student { Id = 2, Name = "Tran Thi B", Age = 21, Email = "b@example.com", Gender = "Female" }
        //     }
        //     ;
        //     var students = await _context.Students.ToListAsync();

        //     Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(student));
        //     return View(students);
        // }

        [HttpGet]
        public async Task<IActionResult> Index() // Kiểu trả về là Task<IActionResult> hoặc Task<ViewResult>
        {
            // Lấy danh sách sinh viên từ DB và truyền vào View
            var students = await _context.Students.ToListAsync(); // Sử dụng _context.Students
            // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(students)); // In ra console, không ảnh hưởng đến View
            return View(students); // Trả về View có tên "Index.cshtml" và truyền danh sách students vào
        }


        // Nếu bạn muốn có một API endpoint để lấy danh sách sinh viên (JSON),
        // bạn có thể giữ phương thức này nhưng trong một API Controller riêng biệt,
        // hoặc đổi tên Action để tránh nhầm lẫn với Action View Index.
        // Tuy nhiên, bạn không thể có hai Action cùng tên 'Index' trong một Controller mà không có Attribute khác biệt (như [HttpGet] và [HttpPost] riêng biệt)
        // Nếu đây là API, kiểu trả về phải là ActionResult<IEnumerable<Student>> hoặc IActionResult
        /*
        [HttpGet("GetStudentsApi")] // Đặt một route khác để phân biệt với Index View
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students); // Trả về JSON
        }
        */
    }
}