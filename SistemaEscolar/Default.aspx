<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaEscolar._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Sistema Escolar</h1>
        <p class="lead">Sistema de uma escola fictícia que fornece cursos diversos. Cursos tem apenas um professor mas vários estudantes, estudantes podem fazer vários cursos ao mesmo tempo.</p>
        <p><a href="~/Cursos" class="btn btn-primary btn-lg">Ver cursos &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Professores</h2>
            <p>
                Os melhores professores do mercado.
            </p>
            <p>
                <a class="btn btn-default" href="~/Professores">Ver professores &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Alunos</h2>
            <p>
                Matricule-se já!
            </p>
            <p>
                <a class="btn btn-default" href="~/Alunos">Ver alunos &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Cursos</h2>
            <p>
                Veja nossos cursos disponíveis.
            </p>
            <p>
                <a class="btn btn-default" href="~/Cursos">Ver cursos &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
