using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Modelo
{
    public class Curso
    {
        public int ID { get; }
        public string Nome { get; set; }
        public Professor Professor { get; set; }
        public string Descricao { get; set; }
        public int Carga { get; set; }

        public List<Aluno> Alunos { get; }

        public Curso(int id, string nome, Professor professor,
            string descricao, int carga, List<Aluno> alunos)
        {
            ID = id;
            Nome = nome;
            Professor = professor;
            Descricao = descricao;
            Carga = carga;
            Alunos = alunos;
        }

        public Curso(string nome, Professor professor,
            string descricao, int carga)
        {
            ID = 0;
            Nome = nome;
            Professor = professor;
            Descricao = descricao;
            Carga = carga;
            Alunos = null;
        }
    }
}