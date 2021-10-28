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
        public String mensagem = "";

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

            #region CEP

            cmd.CommandText = "select cep from endereco where replace(cep,'-','') = replace(@Cep,'-','')";
            //Parametros
            cmd.Parameters.AddWithValue("@Cep", Arquivo.cep.ToString());

            try
            {
                //Conexão com banco
                cmd.Connection = conexao.Conectar();
                //Chamada do banco
                if (cmd.ExecuteScalar() == null)
                {
                    cmd.CommandText = "insert endereco(cep,rua,bairro,cidade,estado) values(@CepViaCep,@RuaViaCep,@BairroViaCep,@CidadeViaCep,@EstadoViaCep)";
                    //Parametros
                    
                    cmd.Parameters.AddWithValue("@CepViaCep", endereco.cep.ToString());
                    cmd.Parameters.AddWithValue("@RuaViaCep", endereco.logradouro.ToString());
                    cmd.Parameters.AddWithValue("@BairroViaCep", endereco.bairro.ToString());
                    cmd.Parameters.AddWithValue("@CidadeViaCep", endereco.localidade.ToString());
                    cmd.Parameters.AddWithValue("@EstadoViaCep", endereco.uf.ToString());

                    try
                    {
                        cmd.Connection = conexao.Conectar();

                        //Salva o novo endereço no banco
                        cmd.ExecuteNonQuery();

                        this.id_endereco = endereco.cep.ToString();
                    }
                    catch
                    {
                        this.erro = "Falha na Conexão";
                    }
                }
                else
                {
                    //Caso ja tenha o cnpj apenas pega o id da empresa
                    this.id_endereco = endereco.cep.ToString();
                }
                //Desconecta do banco
                conexao.Desconectar();

            }
            catch
            {
                this.erro = "Falha na Conexão";
            }
            #endregion

            #region Funcionário
            cmd.CommandText = "select id from funcionario where REPLACE(REPLACE(cpf,'.',''),'-','') = REPLACE(REPLACE(@CpfJson,'.',''),'-','')";
            //Parametros
            cmd.Parameters.AddWithValue("@CpfJson", Arquivo.cpf.ToString());

            try
            {
                //Conexão com banco
                cmd.Connection = conexao.Conectar();
                //Chamada do banco
                if (cmd.ExecuteScalar() == null)
                {
                    cmd.CommandText = "insert funcionario(nome,cpf,salario,empresa,endereco) values(@NomeJson,@CpfJson,@SalarioJson,@EmpresaJson, @EnderecoJson)";
                    //Parametros
                    cmd.Parameters.AddWithValue("@NomeJson", Arquivo.nome.ToString());
                    cmd.Parameters.AddWithValue("@SalarioJson", Arquivo.salario.ToString());
                    cmd.Parameters.AddWithValue("@EmpresaJson", Int32.Parse(this.id_empresa));
                    cmd.Parameters.AddWithValue("@EnderecoJson", this.id_endereco.ToString());

                    try
                    {
                        cmd.Connection = conexao.Conectar();

                        //Salva  o funcionário
                        cmd.ExecuteNonQuery();
                        this.mensagem = "code 201";
                        
                    }
                    catch
                    {
                        this.erro = "Falha na Conexão";
                    }
                }
                else
                {
                    //Caso ja tenha o cnpj apenas pega o id da empresa
                    this.erro = "CPF Duplicado";
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
