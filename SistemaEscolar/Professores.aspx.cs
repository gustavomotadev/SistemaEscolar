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
    public partial class Professores : System.Web.UI.Page
    {
        protected DadoProfessor dadoProfessor = new DadoProfessor();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                preencherGridProfessores();
            }
        }

        protected void grdProfessores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                string arg = e.CommandArgument.ToString();

                if (Int32.TryParse(arg, out int id))
                {
                    var professor = dadoProfessor.Consultar(id);

                    txtID.Text = professor.ID.ToString();
                    txtNome.Text = professor.Nome;
                    txtEspecialidade.Text = professor.Especialidade;
                    txtQualificacao.Text = professor.Qualificacao;
                    txtEmail.Text = professor.Email;
                }
            }
        }

        protected void FormBtn_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("Limpar"))
            {
                txtID.Text = "";
                txtNome.Text = "";
                txtEspecialidade.Text = "";
                txtQualificacao.Text = "";
                txtEmail.Text = "";
            }
            else if (e.CommandName.Equals("Remover"))
            {
                if (Int32.TryParse(txtID.Text, out int id))
                {
                    var res = dadoProfessor.Excluir(id);

                    txtID.Text = "";
                    txtNome.Text = "";
                    txtEspecialidade.Text = "";
                    txtQualificacao.Text = "";
                    txtEmail.Text = "";

                    preencherGridProfessores();
                }
            }
            else if (e.CommandName.Equals("Salvar"))
            {
                if (Int32.TryParse(txtID.Text, out int id))
                {
                    var res = dadoProfessor.Alterar(new Professor(id, txtNome.Text, txtEspecialidade.Text, txtQualificacao.Text, txtEmail.Text));

                    if (res > 0)
                    {
                        preencherGridProfessores();
                    }
                }
                else
                {
                    var res = dadoProfessor.Inserir(new Professor(txtNome.Text, txtEspecialidade.Text, txtQualificacao.Text, txtEmail.Text));

                    if (res != null)
                    {
                        txtID.Text = res.ToString();

                        preencherGridProfessores();
                    }
                }
            }
        }

        protected void preencherGridProfessores()
        {
            var listaProfessores = dadoProfessor.ConsultarTodos();
            if (listaProfessores != null && listaProfessores.Count > 0)
            {
                this.grdProfessores.DataSource = listaProfessores;
                this.grdProfessores.DataBind();
            }
        }
    }
}