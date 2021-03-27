using Microsoft.EntityFrameworkCore;
using Orbit.DAL;
using Orbit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orbit.BL
{
    public interface IStudentBL
    {
        IEnumerable<Student> GetStudent();
        Task<Student> GetStudent(int id);
        Task<Student> PutStudentAsync(int id, Student student);
        bool PostStudentAsync(Student student);
        bool DeleteStudentAsync(Student student);

    }
    public class StudentBL : IStudentBL
    {
        private readonly MyDbContext _context;
        public StudentBL(MyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetStudent()
        {
            return _context.Students;
        }
        public async Task<Student> GetStudent(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> PutStudentAsync(int id, Student student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return student;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

        }

        public bool PostStudentAsync(Student student)
        {
            if (!_context.Students.Any(x => x.Username == student.Username))
                try
                {
                    _context.Students.Add(student);
                    _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }
            else
                return false;
        }
        public bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public bool DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
