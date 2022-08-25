﻿<%@ Page Title="Alunos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alunos.aspx.cs" Inherits="SistemaEscolar.Alunos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br/>

    <div class="panel panel-default">
        <div class="panel-heading"><strong>Dados do Aluno</strong></div>

        <div class="panel-body">
            <table>  
                <tr>
                <td style="vertical-align: top"><asp:Label AssociatedControlId="txtID" Text="Matrícula: " runat="server" /></td>
                <td style="padding-left: 4rem"><asp:TextBox ID="txtID" ReadOnly="true" runat="server" /> <br/><br/></td>
                </tr>
                <tr>
                <td style="vertical-align: top"><asp:Label AssociatedControlId="txtNome" Text="Nome: " runat="server" /></td>
                <td style="padding-left: 4rem"><asp:TextBox ID="txtNome" runat="server" /> <br/><br/></td>
                </tr>
                <tr>
                <td style="vertical-align: top"><asp:Label AssociatedControlId="txtEmail" Text="E-mail: " runat="server" /></td>
                <td style="padding-left: 4rem"><asp:TextBox ID="txtEmail" runat="server" /> <br/><br/></td>
                </tr>
            </table>
            <asp:LinkButton ID="btnLimpar" CssClass="btn btn-default" runat="server"
            CommandName="Limpar" OnCommand="FormBtn_Click"> 
                <span class="glyphicon glyphicon-erase" aria-hidden="true"> Limpar </span>
            </asp:LinkButton>
            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server"
            CommandName="Salvar" OnCommand="FormBtn_Click"> 
                <span class="glyphicon glyphicon-ok" aria-hidden="true"> Salvar </span>
            </asp:LinkButton>
            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-danger" runat="server"
            CommandName="Remover" OnCommand="FormBtn_Click"> 
                <span class="glyphicon glyphicon-trash" aria-hidden="true"> Remover </span>
            </asp:LinkButton>
        </div>

    </div>

    <div class="panel panel-default">
        <div class="panel-heading"><strong>Alunos</strong></div>

        <asp:GridView ID="grdAlunos" CssClass="table table-custom" runat="server" AutoGenerateColumns="false"
        OnRowCommand="grdAlunos_RowCommand">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="Matrícula" />
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Email" HeaderText="E-mail" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnEditar" CssClass="btn btn-default" runat="server"
                    CommandName="Editar" 
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' > 
                        <span class="glyphicon glyphicon-pencil" aria-hidden="true"> Editar </span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>

    </div>

</asp:Content>
