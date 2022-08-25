using SistemaEscolar.Dado;
using SistemaEscolar.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaEscolar
{
    public partial class Alunos : System.Web.UI.Page
    {
        protected DadoAluno dadoAluno = new DadoAluno();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                preencherGridAlunos();
            }
        }

        protected void grdAlunos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                string arg = e.CommandArgument.ToString();

                if (Int32.TryParse(arg, out int id))
                {
                    var aluno = dadoAluno.Consultar(id);

                    txtID.Text = aluno.ID.ToString();
                    txtNome.Text = aluno.Nome;
                    txtEmail.Text = aluno.Email;
                }
            }
        }

        protected void FormBtn_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("Limpar"))
            {
                txtID.Text = "";
                txtNome.Text = "";
                txtEmail.Text = "";
            }
            else if (e.CommandName.Equals("Remover"))
            {
                if (Int32.TryParse(txtID.Text, out int id))
                {
                    var res = dadoAluno.Excluir(id);

                    txtID.Text = "";
                    txtNome.Text = "";
                    txtEmail.Text = "";

                    preencherGridAlunos();
                }
            }
            else if (e.CommandName.Equals("Salvar"))
            {
                if (Int32.TryParse(txtID.Text, out int id))
                {
                    var res = dadoAluno.Alterar(new Aluno(id, txtNome.Text, txtEmail.Text));

                    if (res > 0)
                    {
                        preencherGridAlunos();
                    }
                }
                else
                {
                    var res = dadoAluno.Inserir(new Aluno(txtNome.Text, txtEmail.Text));

                    if (res != null)
                    {
                        txtID.Text = res.ToString();

                        preencherGridAlunos();
                    }
                }
            }
        }

        protected void preencherGridAlunos()
        {
            var listaAlunos = dadoAluno.ConsultarTodos();
            if (listaAlunos != null && listaAlunos.Count > 0)
            {
                this.grdAlunos.DataSource = listaAlunos;
                this.grdAlunos.DataBind();
            }
        }
    }
}