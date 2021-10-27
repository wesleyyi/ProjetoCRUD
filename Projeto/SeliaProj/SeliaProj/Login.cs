using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeliaProj
{
    public class Login
    {
        //Instanciação da conexão
        Conexao conexao = new Conexao();
        SqlCommand cmd = new SqlCommand();

        //Mensagem retorno
        public String mensagem = "";

        public Login(String login, String senha)
        {
            cmd.CommandText = "SELECT ID FROM FOL_LOGIN WHERE NM_LOGIN = @Login AND DC_SENHA = @Senha";

            //Parametros
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@Senha", senha);

            try
            {
                //Conexão com banco
                cmd.Connection = conexao.Conectar();

                //Chamada do banco
                if (cmd.ExecuteScalar() == null) 
                {
                    this.mensagem = "Code 403";
                }
                else 
                {
                    this.mensagem = "Login Efetuado";
                }
                

                //Desconecta do banco
                conexao.Desconectar();

               
            }
            catch
            {
                this.mensagem = "Falha na Conexão";
            }
        }

    }
}
