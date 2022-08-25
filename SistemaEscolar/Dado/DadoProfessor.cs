using SistemaEscolar.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Dado
{
    public class DadoProfessor : DadoBase<Professor>
    {
        public override int Alterar(Professor objeto)
        {
            string query = "UPDATE [dbo].[Professor] SET [Nome] = @Nome, [Especialidade] = @Especialidade, [Qualificacao] = @Qualificacao, [Email] = @Email WHERE ID = @ID; SELECT @@ROWCOUNT;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", objeto.Nome));
            parametros.Add(new SqlParameter("Especialidade", objeto.Especialidade));
            parametros.Add(new SqlParameter("Qualificacao", objeto.Qualificacao));
            parametros.Add(new SqlParameter("Email", objeto.Email));
            parametros.Add(new SqlParameter("ID", objeto.ID));

            var resultado = ExecuteScalarQuery(query, parametros);

            return (resultado == null) ? 0 : (int)resultado;
        }

        public override Professor Consultar(int chave)
        {
            string query = "SELECT * FROM [dbo].[Professor] WHERE ID = @ID;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chave));

            var dataReader = ExecuteReaderQuery(query, parametros);

            Professor consultado = null;

            if (dataReader.HasRows)
            {
                dataReader.Read();
                consultado = new Professor
                (
                    id: Convert.ToInt32(dataReader["ID"]),
                    nome: dataReader["Nome"].ToString(),
                    especialidade: dataReader["Especialidade"].ToString(),
                    qualificacao: dataReader["Qualificacao"].ToString(),
                    email: dataReader["Email"].ToString(),
                    cursos: null
                );
            }

            dataReader.Dispose();

            return consultado;
        }

        public override Professor ConsultarPreenchido(int chave)
        {
            var consultado = Consultar(chave);

            if (consultado != null)
            {
                var cursos = ConsultarCursosProfessor(chave);

                consultado = new Professor(consultado.ID, consultado.Nome, 
                    consultado.Especialidade, consultado.Qualificacao, consultado.Email,
                    cursos);
            }

            return consultado;
        }

        public override List<Professor> ConsultarTodos()
        {
            string query = "SELECT * FROM [dbo].[Professor];";

            var dataReader = ExecuteReaderQuery(query, new List<SqlParameter>());

            var consultados = new List<Professor>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    consultados.Add(new Professor
                    (
                        id: Convert.ToInt32(dataReader["ID"]),
                        nome: dataReader["Nome"].ToString(),
                        especialidade: dataReader["Especialidade"].ToString(),
                        qualificacao: dataReader["Qualificacao"].ToString(),
                        email: dataReader["Email"].ToString(),
                        cursos: null
                    ));
                }
            }

            dataReader.Dispose();

            return consultados;
        }

        public override int Excluir(int chave)
        {
            string query = "DELETE FROM [dbo].[Professor] WHERE ID = @ID; SELECT @@ROWCOUNT;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chave));

            var resultado = ExecuteScalarQuery(query, parametros);

            return (resultado == null) ? 0 : (int)resultado;
        }

        public override int? Inserir(Professor objeto)
        {
            string query = "INSERT INTO [dbo].[Professor] ([Nome],[Especialidade],[Qualificacao],[Email]) OUTPUT Inserted.ID VALUES (@Nome, @Especialidade, @Qualificacao, @Email);";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", objeto.Nome));
            parametros.Add(new SqlParameter("Especialidade", objeto.Especialidade));
            parametros.Add(new SqlParameter("Qualificacao", objeto.Qualificacao));
            parametros.Add(new SqlParameter("Email", objeto.Email));

            return (int?) ExecuteScalarQuery(query, parametros);
        }

        public List<Curso> ConsultarCursosProfessor(int chaveProfessor)
        {
            string query = "SELECT * FROM [dbo].[Curso] WHERE [IDProfessor] = @ID;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chaveProfessor));

            var dataReader = ExecuteReaderQuery(query, parametros);

            var consultados = new List<Curso>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    consultados.Add(new Curso
                    (
                        id: Convert.ToInt32(dataReader["ID"]),
                        nome: dataReader["Nome"].ToString(),
                        professor: null,
                        descricao: dataReader["Descricao"].ToString(),
                        carga: Convert.ToInt32(dataReader["Carga"]),
                        alunos: null
                    ));
                }
            }

            dataReader.Dispose();

            return consultados;
        }
    }
}