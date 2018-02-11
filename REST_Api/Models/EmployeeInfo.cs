using System.ComponentModel.DataAnnotations;

namespace REST_Api.Models
{
    public class EmployeeInfo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "EmpNo is Must")]
        public int EmpNo { get; set; }
        [Required(ErrorMessage ="EmpName is Must")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Salary is Must")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "DeptName is Must")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Designation is Must")]
        public string Designation { get; set; }
    }
}