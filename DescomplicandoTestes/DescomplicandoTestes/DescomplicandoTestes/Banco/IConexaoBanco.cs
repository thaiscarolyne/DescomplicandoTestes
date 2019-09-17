using System;
using System.Collections.Generic;
using System.Text;

namespace DescomplicandoTestes.Banco
{
    public interface IConexaoBanco
    {
        //Esse método irá retornar se esse CPF com essa senha existe no Banco de Dados
        bool ConsultaProfessor(string CPF, string Senha);

    }
}
