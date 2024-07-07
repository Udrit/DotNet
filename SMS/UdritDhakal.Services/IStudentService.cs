using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.UtiIities;
using UdritDhakal.ViewModels;

namespace UdritDhakal.Services
{
    public interface IStudentService
    {
        Task AddStudent(CreateStudentViewModel student);
        PagedResult<StudentViewModel> GetAll(int pageNumber, int pageSize);
        int GetAllStudents();
    }
}
