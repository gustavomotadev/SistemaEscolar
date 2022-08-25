using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Modelo
{
    public class Professor
    {
        public int ID { get; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string Qualificacao { get; set; }
        public string Email { get; set; }

        public List<Curso> Cursos { get; }

        public Professor(int id, string nome, string especialidade,
            string qualificacao, string email, List<Curso> cursos)
        {
            ID = id;
            Nome = nome;
            Especialidade = especialidade;
            Qualificacao = qualificacao;
            Email = email;
            Cursos = cursos;
        }

        public Professor(int id, string nome, string especialidade,
            string qualificacao, string email)
        {
            ID = id;
            Nome = nome;
            Especialidade = especialidade;
            Qualificacao = qualificacao;
            Email = email;
            Cursos = null;
        }

        public Professor(string nome, string especialidade,
            string qualificacao, string email)
        {
            ID = 0;
            Nome = nome;
            Especialidade = especialidade;
            Qualificacao = qualificacao;
            Email = email;
            Cursos = null;
        }
    }
}