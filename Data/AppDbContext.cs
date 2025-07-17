// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Đúng rồi
using Microsoft.AspNetCore.Identity; // Đúng rồi
using UserManagerDemo.Models; // Đúng rồi

namespace UserManagerDemo.Data
{
    // Cần thay đổi từ DbContext thành IdentityDbContext<IdentityUser>
    // HOẶC IdentityDbContext<ApplicationUser> nếu bạn định nghĩa lớp người dùng tùy chỉnh
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Định nghĩa các DbSet cho các bảng tùy chỉnh của bạn
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }

        // RẤT QUAN TRỌNG: Bạn PHẢI override OnModelCreating và gọi base.OnModelCreating(builder);
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Dòng này là bắt buộc để Identity tạo các bảng của nó

            // Cấu hình thêm cho các bảng của bạn nếu cần (ví dụ: đổi tên bảng, thêm ràng buộc...)
            // builder.Entity<User>().ToTable("Users");
            // builder.Entity<Student>().ToTable("Students");
        }
    }
}