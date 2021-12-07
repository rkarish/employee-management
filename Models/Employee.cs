namespace employee_management.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Details { get; set; }
        public List<EmployeeSkill> EmployeeSkills { get; set; } = null!;
        public DateTime? HiringDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }

    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get { return FirstName + " " + LastName; } }
        public string? Details { get; set; }
        public List<GetSkillDto>? Skills { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }

    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get { return FirstName + " " + LastName; } }
        public string? Details { get; set; }
        public DateTime? HiringDate { get; set; }
    }

    public class AddEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Details { get; set; }
        public List<int> SkillIds { get; set; } = null!;
        public DateTime? HiringDate { get; set; }
    }
}