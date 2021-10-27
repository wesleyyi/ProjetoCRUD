using SeliaProj.Serialization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeliaProj
{
    public class FuncionalidadesJson
    {
        //Instanciação da conexão
        Conexao conexao = new Conexao();
        SqlCommand cmd = new SqlCommand();

        //Retornos
        public String id_empresa = "";
        public String id_endereco = "";
        public String erro = "";

        public FuncionalidadesJson(CadastroRecebido Arquivo, EnderecoViaCep endereco)
        {
            #region Empresa
            cmd.CommandText = "select id from empresa where replace(replace(replace(cnpj,'.',''),'-',''),'/','') = replace(replace(replace(@Cnpj,'.',''),'-',''),'/','')";
            //Parametros
            cmd.Parameters.AddWithValue("@Cnpj", Arquivo.cnpj.ToString());

            try
            {
                //Conexão com banco
                cmd.Connection = conexao.Conectar();
                //Chamada do banco
                if (cmd.ExecuteScalar() == null)
                {
                    cmd.CommandText = "INSERT empresa(razao_social, cnpj) VALUES(@dc_razao, @dc_cnpj) SELECT top 1 id FROM empresa order by id desc";
                    //Parametros
                    cmd.Parameters.AddWithValue("@dc_razao", Arquivo.razao_social.ToString());
                    cmd.Parameters.AddWithValue("@dc_cnpj", Arquivo.cnpj.ToString());
                    
                    try
                    {
                        cmd.Connection = conexao.Conectar();

                        //Salva a nova empresa e pega o id da empresa
                        this.id_empresa = cmd.ExecuteScalar().ToString();


                    }
                    catch
                    {
                        this.erro = "Falha na Conexão";
                    }
                }
                else
                {
                    //Caso ja tenha o cnpj apenas pega o id da empresa
                    this.id_empresa = cmd.ExecuteScalar().ToString();
                }
                //Desconecta do banco
                conexao.Desconectar();

            }
            catch
            {
                this.erro = "Falha na Conexão";
            }
            #endregion
        }
    }
}
