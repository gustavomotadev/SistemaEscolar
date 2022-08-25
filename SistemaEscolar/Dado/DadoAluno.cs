using SistemaEscolar.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Dado
{
    public class DadoAluno : DadoBase<Aluno>
    {
        public override int Alterar(Aluno objeto)
        {
            string query = "UPDATE [dbo].[Aluno] SET [Nome] = @Nome, [Email] = @Email WHERE ID = @ID; SELECT @@ROWCOUNT;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", objeto.Nome));
            parametros.Add(new SqlParameter("Email", objeto.Email));
            parametros.Add(new SqlParameter("ID", objeto.ID));

            var resultado = ExecuteScalarQuery(query, parametros);

            return (resultado == null) ? 0 : (int)resultado;
        }

        public override Aluno Consultar(int chave)
        {
            string query = "SELECT * FROM [dbo].[Aluno] WHERE ID = @ID;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chave));

            var dataReader = ExecuteReaderQuery(query, parametros);

            Aluno consultado = null;

            if (dataReader.HasRows)
            {
                dataReader.Read();
                consultado = new Aluno
                (
                    id: Convert.ToInt32(dataReader["ID"]),
                    nome: dataReader["Nome"].ToString(),
                    email: dataReader["Email"].ToString(),
                    cursos: null
                );
            }

            dataReader.Dispose();

            return consultado;
        }

        public override Aluno ConsultarPreenchido(int chave)
        {
            var consultado = Consultar(chave);

            if (consultado != null)
            {
                var cursos = ConsultarCursosAluno(chave);

                consultado = new Aluno(consultado.ID, consultado.Nome, consultado.Email,
                    cursos);
            }

            return consultado;
        }

        public override List<Aluno> ConsultarTodos()
        {
            string query = "SELECT * FROM [dbo].[Aluno];";

            var dataReader = ExecuteReaderQuery(query, new List<SqlParameter>());

            var consultados = new List<Aluno>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    consultados.Add(new Aluno
                    (
                        id: Convert.ToInt32(dataReader["ID"]),
                        nome: dataReader["Nome"].ToString(),
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
            string query = "DELETE FROM [dbo].[Aluno] WHERE ID = @ID; SELECT @@ROWCOUNT;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chave));

            var resultado = ExecuteScalarQuery(query, parametros);

            return (resultado == null) ? 0 : (int) resultado;
        }

        public override int? Inserir(Aluno objeto)
        {
            string query = "INSERT INTO [dbo].[Aluno] ([Nome],[Email]) OUTPUT Inserted.ID VALUES (@Nome, @Email);";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", objeto.Nome));
            parametros.Add(new SqlParameter("Email", objeto.Email));

            return (int?) ExecuteScalarQuery(query, parametros);
        }

        public List<Curso> ConsultarCursosAluno(int chaveAluno)
        {
            string query = "SELECT [C].ID AS [CID],[C].Nome AS [CNome],[C].[Descricao] AS [CDescricao],[C].[Carga] AS [CCarga],[P].ID AS [PID],[P].[Nome] AS [PNome],[P].[Especialidade] AS [PEspecialidade],[P].[Qualificacao] AS [PQualificacao],[P].[Email] AS [PEmail] FROM [dbo].[Curso] AS [C] INNER JOIN [dbo].[Professor] AS [P] ON [C].IDProfessor = [P].ID INNER JOIN [dbo].[AlunoCurso] AS [AC] ON [C].[ID] = [AC].IDCurso WHERE [AC].[IDAluno] = @ID;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chaveAluno));

            var dataReader = ExecuteReaderQuery(query, parametros);

            var cConsultados = new List<Curso>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Professor pConsultado = new Professor
                    (
                        id: Convert.ToInt32(dataReader["PID"]),
                        nome: dataReader["PNome"].ToString(),
                        especialidade: dataReader["PEspecialidade"].ToString(),
                        qualificacao: dataReader["PQualificacao"].ToString(),
                        email: dataReader["PEmail"].ToString(),
                        cursos: null
                    );
                    cConsultados.Add(new Curso
                    (
                        id: Convert.ToInt32(dataReader["CID"]),
                        nome: dataReader["CNome"].ToString(),
                        professor: pConsultado,
                        descricao: dataReader["CDescricao"].ToString(),
                        carga: Convert.ToInt32(dataReader["CCarga"]),
                        alunos: null
                    ));
                }
            }

            dataReader.Dispose();

            return cConsultados;
        }
    }
}