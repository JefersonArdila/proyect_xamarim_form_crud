using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TabbedPageTest.Models;
using System.Threading.Tasks;

namespace TabbedPageTest.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Alumno>().Wait();
        }

        public Task<int> SaveAlumnoAsync(Alumno alum) 
        {
            if (alum.IdAlumno !=0)
            {
                return db.UpdateAsync(alum);
            }
            else
            {
                return db.InsertAsync(alum);
            }
        }

        public Task<int> DeleteAlumnoAsync(Alumno alumno)
        {
            return db.DeleteAsync(alumno);
        }

        /// <summary>
        /// Recuperar todos los alumnos
        /// </summary>
        /// <returns></returns>
        public Task<List<Alumno>> GetAlumnosAsync()
        {
            return db.Table<Alumno>().ToListAsync();
        }

        /// <summary>
        /// Recupera alumnos por id
        /// </summary>
        /// <param name="idAlumno">id del alumno que se requiere</param>
        /// <returns></returns>



        public Task<Alumno> GetAlumnoByIdAsync(int idAlumno)
        {
            return db.Table<Alumno>().Where(a => a.IdAlumno== idAlumno).FirstOrDefaultAsync();
        }
    }
}
