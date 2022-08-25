using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Modelo
{
    public class Aluno
    {
        public int ID { get; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public List<Curso> Cursos { get; }

        public Aluno(int id, string nome, string email, List<Curso> cursos)
        {
            ID = id;
            Nome = nome;
            Email = email;
            Cursos = cursos;
        }

        public Aluno(int id, string nome, string email)
        {
            ID = id;
            Nome = nome;
            Email = email;
            Cursos = null;
        }

        public Aluno(string nome, string email)
        {
            ID = 0;
            Nome = nome;
            Email = email;
            Cursos = null;
        }
    }
}