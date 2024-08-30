
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Employee
{
    public int  id { get; set; }
    public int UserId { get; set; }

    public string  firstName  {get; set; }
    public string  lastName { get; set; }
    public string phoneNumber { get; set; }
    public decimal salary { get; set; }
    public string title { get; set; }

    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }

}
