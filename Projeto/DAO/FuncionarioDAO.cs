using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Projeto.Models;

namespace Projeto.DAO {
    public class FuncionarioDAO : Conexao {

        public bool AdicionarFuncionario(Funcionario func) {
            
            // 1º O método abre a Conexão com o BD, chama a respectiva Procedure
                // Instancia-se a variavel SqlQuery para o Tipo StoredProcedure
                    // A seguir os Parâmetros são instanciados de acordo com a SP
                        // Por fim é executado o Comando SqlQuery
            // 2º Caso o procedimento não seja efetuado, é exibido uma mensagem de erro
            // 3º Ao Final, independente da situação, é Fechado a Conexão com o BD
                    
            try {

                AbrirConexao();

                SqlQuery = new SqlCommand("AdicionarNovoFuncionario", Con);
                SqlQuery.CommandType = CommandType.StoredProcedure;

                SqlQuery.Parameters.AddWithValue("@Nome", func.Nome);
                SqlQuery.Parameters.AddWithValue("@Sobrenome", func.Sobrenome);
                SqlQuery.Parameters.AddWithValue("@Cidade", func.Cidade);
                SqlQuery.Parameters.AddWithValue("@Endereco", func.Endereco);
                SqlQuery.Parameters.AddWithValue("@Email", func.Email);

                // Tratamento para saber se alguma linha foi alterada no Banco de Dados
                int i = SqlQuery.ExecuteNonQuery();
                if(i >= 1) {

                    return true;
                }else {

                    return false;
                }
            } catch(Exception error) {

                throw new Exception(error.Message);
            } finally {

                FecharConexao();
            }
        }
        
        public List<Funcionario> SelecionarFuncionarios() {

            // 1º O método Abre a Conexão com o BD, insere a Query passando a Con
            // 2º Cria-se uma Lista baseada na respectiva Model
                // É feito um looping para a leitura dos dados
                    // Ao final a lista completa é retornada
            // 3º Caso o Procedimento não seja efetuado, mostra-se uma mensagem de erro
            // 4º Ao Final, independente da situação, é Fechado a Conexão com o BD
            try {

                AbrirConexao();

                SqlQuery = new SqlCommand("SELECT * FROM Funcionario", Con);

                Rs = SqlQuery.ExecuteReader();

                var lista = new List<Funcionario>();

                while (Rs.Read()) {

                    var func = new Funcionario {

                        IdFuncionario = Convert.ToInt32(Rs["Id"]),
                        Nome = Convert.ToString(Rs["Nome"]),
                        Sobrenome = Convert.ToString(Rs["Sobrenome"]),
                        Cidade = Convert.ToString(Rs["Cidade"]),
                        Endereco = Convert.ToString(Rs["Endereco"]),
                        Email = Convert.ToString(Rs["Email"])
                    };

                    lista.Add(func);
                }

                return lista;
            }catch(Exception error) {

                throw new Exception(error.Message);
            } finally {

                FecharConexao();
            }
        }

        public bool AtualizarFuncionario(Funcionario func) {

            // 1º O método abre a Conexão com o BD, chama a respectiva Procedure
                // Instancia-se a variavel SqlQuery para o Tipo StoredProcedure
                    // A seguir os Parâmetros são instanciados de acordo com a SP
                        // Por fim é executado o Comando SqlQuery
            // 2º Caso o procedimento não seja efetuado, é exibido uma mensagem de erro
            // 3º Ao Final, independente da situação, é Fechado a Conexão com o BD

            try {

                AbrirConexao();

                SqlQuery = new SqlCommand("AtualizarFuncionario", Con);
                SqlQuery.CommandType = CommandType.StoredProcedure;

                SqlQuery.Parameters.AddWithValue("@IdFuncionario", func.IdFuncionario);
                SqlQuery.Parameters.AddWithValue("@Nome", func.Nome);
                SqlQuery.Parameters.AddWithValue("@Sobrenome", func.Sobrenome);
                SqlQuery.Parameters.AddWithValue("@Endereco", func.Endereco);
                SqlQuery.Parameters.AddWithValue("@Cidade", func.Cidade);
                SqlQuery.Parameters.AddWithValue("@Email", func.Email);

                // Validação para saber se a linha foi alterada no BD
                int i = SqlQuery.ExecuteNonQuery();
                if(i >= 1) {

                    return true;
                }else {

                    return false;
                }

            }catch(Exception error) {

                throw new Exception(error.Message);
            } finally {

                FecharConexao();
            }
        }

        public bool ExcluirFuncionario(int Id) {

            // 1º O método abre a Conexão com o BD, chama a respectiva Procedure
                // Instancia-se a variavel SqlQuery para o Tipo StoredProcedure
                    // A seguir o Parâmetro ID é instanciado de acordo com a SP
                        // Por fim é executado o Comando SqlQuery
            // 2º Caso o procedimento não seja efetuado, é exibido uma mensagem de erro
            // 3º Ao Final, independente da situação, é Fechado a Conexão com o BD

            try {

                AbrirConexao();

                SqlQuery = new SqlCommand("DeletarFuncionario", Con);
                SqlQuery.CommandType = CommandType.StoredProcedure;
                SqlQuery.Parameters.AddWithValue("@IdFuncionario", Id);

                // Validação para saber se a linha foi alterada no BD
                int i = SqlQuery.ExecuteNonQuery();
                if(i >= 1) {

                    return true;
                }else {

                    return false;
                }
            }catch(Exception error) {

                throw new Exception(error.Message);
            } finally {

                FecharConexao();
            }
        }
    }
}