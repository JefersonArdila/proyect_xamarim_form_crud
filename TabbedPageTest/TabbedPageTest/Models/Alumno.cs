using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TabbedPageTest.Models
{
    public class Alumno
    {
        [PrimaryKey,AutoIncrement]

        public int IdAlumno { get; set; }

        [MaxLength(50)]
        
        public string Nombre { get; set; }

        [MaxLength(50)]

        public string Apellidos { get; set; }
        [MaxLength(50)]

        public int Edad { get; set; }
        [MaxLength(100)]

        public string Email { get; set;}

    }
}
