using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Dado
{
    public abstract class DadoBase<T> where T : class
    {
        protected string Conexao = ConfigurationManager.ConnectionStrings["stringconexao"].ConnectionString;

        public abstract int? Inserir(T objeto);
        public abstract T Consultar(int chave);
        public abstract T ConsultarPreenchido(int chave);
        public abstract List<T> ConsultarTodos();
        public abstract int Alterar(T objeto);
        public abstract int Excluir(int chave);

        protected object ExecuteScalarQuery(string query, IEnumerable<SqlParameter> parameters)
        {
            SqlConnection connection = new SqlConnection(Conexao);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters.ToArray());

            var result = command.ExecuteScalar();

            command.Dispose();
            connection.Dispose();

            return result;
        }

        protected SqlDataReader ExecuteReaderQuery(string query, IEnumerable<SqlParameter> parameters)
        {
            SqlConnection connection = new SqlConnection(Conexao);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters.ToArray());

            var result = command.ExecuteReader(CommandBehavior.CloseConnection); //conexão será fechada ao fechar SqlDataReader

            command.Dispose();
            //connection.Dispose(); //conexão será fechada ao fechar SqlDataReader

            return result;
        }
    }
}