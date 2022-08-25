using SistemaEscolar.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Dado
{
    public class DadoCurso : DadoBase<Curso>
    {
        public override int Alterar(Curso objeto)
        {
            string query = "UPDATE [dbo].[Curso] SET [Nome] = @Nome, [IDProfessor] = @IDProfessor, [Descricao] = @Descricao, [Carga] = @Carga WHERE ID = @ID; SELECT @@ROWCOUNT;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", objeto.Nome));
            parametros.Add(new SqlParameter("IDProfessor", objeto.Professor.ID));
            parametros.Add(new SqlParameter("Descricao", objeto.Descricao));
            parametros.Add(new SqlParameter("Carga", objeto.Carga));
            parametros.Add(new SqlParameter("ID", objeto.ID));

            var resultado = ExecuteScalarQuery(query, parametros);

            return (resultado == null) ? 0 : (int)resultado;
        }

        public override Curso Consultar(int chave)
        {
            string query = "SELECT [C].ID AS [CID],[C].Nome AS [CNome],[C].[Descricao] AS [CDescricao],[C].[Carga] AS [CCarga],[P].ID AS [PID],[P].[Nome] AS [PNome],[P].[Especialidade] AS [PEspecialidade],[P].[Qualificacao] AS [PQualificacao],[P].[Email] AS [PEmail] FROM [dbo].[Curso] AS [C] INNER JOIN [dbo].[Professor] AS [P] ON [C].IDProfessor = [P].ID WHERE [C].ID = @ID;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chave));

            var dataReader = ExecuteReaderQuery(query, parametros);

            Curso cConsultado = null;
            Professor pConsultado = null;

            if (dataReader.HasRows)
            {
                dataReader.Read();
                pConsultado = new Professor
                (
                    id: Convert.ToInt32(dataReader["PID"]),
                    nome: dataReader["PNome"].ToString(),
                    especialidade: dataReader["PEspecialidade"].ToString(),
                    qualificacao: dataReader["PQualificacao"].ToString(),
                    email: dataReader["PEmail"].ToString(),
                    cursos: null
                );
                cConsultado = new Curso
                (
                    id: Convert.ToInt32(dataReader["CID"]),
                    nome: dataReader["CNome"].ToString(),
                    professor: pConsultado,
                    descricao: dataReader["CDescricao"].ToString(),
                    carga: Convert.ToInt32(dataReader["CCarga"]),
                    alunos: null
                );
            }

            dataReader.Dispose();

            return cConsultado;
        }

        public override Curso ConsultarPreenchido(int chave)
        {
            var consultado = Consultar(chave);

            if (consultado != null)
            {
                var alunos = ConsultarAlunosCurso(chave);

                consultado = new Curso(consultado.ID, consultado.Nome, consultado.Professor,
                    consultado.Descricao, consultado.Carga, alunos);
            }

            return consultado;
        }

        public override List<Curso> ConsultarTodos()
        {
            string query = "SELECT [C].ID AS [CID],[C].Nome AS [CNome],[C].[Descricao] AS [CDescricao],[C].[Carga] AS [CCarga],[P].ID AS [PID],[P].[Nome] AS [PNome],[P].[Especialidade] AS [PEspecialidade],[P].[Qualificacao] AS [PQualificacao],[P].[Email] AS [PEmail] FROM [dbo].[Curso] AS [C] INNER JOIN [dbo].[Professor] AS [P] ON [C].IDProfessor = [P].ID;";

            var dataReader = ExecuteReaderQuery(query, new List<SqlParameter>());

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

        public override int Excluir(int chave)
        {
            string query = "DELETE FROM [dbo].[Curso] WHERE ID = @ID; SELECT @@ROWCOUNT;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chave));

            var resultado = ExecuteScalarQuery(query, parametros);

            return (resultado == null) ? 0 : (int)resultado;
        }

        public override int? Inserir(Curso objeto)
        {
            string query = "INSERT INTO [dbo].[Curso] ([Nome],[IDProfessor],[Descricao],[Carga]) OUTPUT Inserted.ID VALUES (@Nome, @IDProfessor, @Descricao, @Carga);";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", objeto.Nome));
            parametros.Add(new SqlParameter("IDProfessor", objeto.Professor.ID));
            parametros.Add(new SqlParameter("Descricao", objeto.Descricao));
            parametros.Add(new SqlParameter("Carga", objeto.Carga));

            return (int?) ExecuteScalarQuery(query, parametros);
        }

        public List<Aluno> ConsultarAlunosCurso(int chaveCurso)
        {
            string query = "SELECT [A].[ID],[A].[Nome],[A].[Email] FROM [dbo].[Aluno] AS [A] INNER JOIN [dbo].[AlunoCurso] AS [AC] ON [A].[ID] = [AC].[IDAluno] WHERE [AC].[IDCurso] = @ID;";

            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("ID", chaveCurso));

            var dataReader = ExecuteReaderQuery(query, parametros);

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
    }
}