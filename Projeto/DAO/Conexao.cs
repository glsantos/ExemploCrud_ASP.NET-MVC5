using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Projeto.DAO {
    public class Conexao {
        // Var de Conexao, de Execução e de ResultSet
        protected SqlConnection Con;
        protected SqlCommand SqlQuery;
        protected SqlDataReader Rs;

        // Método para abrir conexão, o caminho do Banco é pego segundo instância do WebConfig
        protected void AbrirConexao() {
             try {
                // Instância-se a variável Con e em seguida tenta-se abrir Conexão com a Base de Dados
                Con = new SqlConnection(WebConfigurationManager.ConnectionStrings["SQLProjCRUD"].ConnectionString);
                Con.Open();
            } catch(Exception error) {
                // Caso não seja possível a conexão, exibe a mensagem de erro
                throw new Exception("Erro ao abrir a conexão: " + error.Message);
            }
        }

        // Método para fechar conexão
        protected void FecharConexao() {
            // Tenta-se executar o fechamento da Conexão
            try {
                if(Con != null) {

                    Con.Close();
                }
            }catch(Exception error) {
                // Caso não seja possível, exibe a mensagem de erro
                throw new Exception("Erro ao fechar a conexão: " + error.Message);
            }
        }
    }
}