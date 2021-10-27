using Correios;
using SeliaProj.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeliaProj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Envia os dados de login para validação
            Login login = new Login(txtbLogin.Text, txtbSenha.Text);

            //Verifica se o login existe, se deu erro na conexão ou se logou com sucesso
            if (login.mensagem == "Code 403")
            {
                MessageBox.Show("Usuário e/ou senha inválidos");
            }
            else if (login.mensagem == "Falha na Conexão")
            {
                MessageBox.Show(login.mensagem);
            }
            else 
            {
                //Pega o json anexado
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\ArqExemplo.json");
                
                //Lê o json e transforma em objeto
                var js = new DataContractJsonSerializer(typeof(List<CadastroRecebido>));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                var recebido = (List<CadastroRecebido>)js.ReadObject(ms);

                //Instancia classe dos correis
                CorreiosApi correiosapi = new CorreiosApi();

                //Percorre todos cadastro enviados
                foreach (CadastroRecebido cad in recebido) 
                {
                    //Busca as informações através do CEP
                    EnderecoViaCep endereco = new EnderecoViaCep();
                    var retornoViaCep = correiosapi.consultaCEP(cad.cep);

                    //Alimenta nossa classe endereco
                    endereco.bairro = retornoViaCep.bairro.ToString();
                    endereco.cep = retornoViaCep.cep.ToString();
                    endereco.logradouro = retornoViaCep.end.ToString();
                    endereco.localidade = retornoViaCep.cidade.ToString();
                    endereco.uf = retornoViaCep.uf.ToString();

                    //Chama o método que fazer as validações e girar o mecanismo do sistema
                    FuncionalidadesJson funcionalidades = new FuncionalidadesJson(cad, endereco);

                }
                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
