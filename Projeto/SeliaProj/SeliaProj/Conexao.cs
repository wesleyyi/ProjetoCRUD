using System.Data.SqlClient;


namespace SeliaProj
{
    public class Conexao
    {
        //Objeto conexão
        SqlConnection con = new SqlConnection();

        public Conexao() 
        {
            //DATA SOURCE = Nome do servidor, Initial Catalog = Nome do banco de dados, no meu caso não é necessário login e senha
            con.ConnectionString = @"Data Source=NBSNSP-5X23VW2\SQLEXPRESS;Initial Catalog=Selia_BD;Integrated Security=True";
        }

        public SqlConnection Conectar() 
        {
            //Verifica se a conexão está fechada
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public void Desconectar()
        {
            //Verifica se a conexão está aberta
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
