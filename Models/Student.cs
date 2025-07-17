// Models/Student.cs
using System.ComponentModel.DataAnnotations; // Để dùng các Data Annotations cho validation

namespace UserManagerDemo.Models
{
    public class Student
    {
        public int Id { get; set; } // ID sẽ được tự động tăng (nếu cấu hình DB) hoặc không cần hiển thị trên form thêm mới

        [Required(ErrorMessage = "入力は必須です。")]
        [StringLength(100, ErrorMessage = "100文字以下で入力してください。")]
        [Display(Name = "学生名")] // Tên hiển thị trên UI
        public string Name { get; set; }

        [Required(ErrorMessage = "入力は必須です。")]
        [Range(18, 60, ErrorMessage = "18~60歳を入力してください.")]
        [Display(Name = "年齢")]
        public int Age { get; set; }

        [Required(ErrorMessage = "入力は必須です。")]
        [EmailAddress(ErrorMessage = "Emailの形式が正しくありません")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "性別")]
        public string Gender { get; set; } // Ví dụ: "Male", "Female", "Other"
    }
}