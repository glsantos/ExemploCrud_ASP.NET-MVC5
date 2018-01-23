using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Projeto.DAO;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class FuncionarioController : Controller {

        public ActionResult SelecionarFuncionarios() {

            var funcDAO = new FuncionarioDAO();
            ModelState.Clear();

            return View(funcDAO.SelecionarFuncionarios());
        }
        
        
        // Validação para ADD Funcionario via GET
            // Caso ocorra, o usuário retorna a View principal e a operação não é feita
        public ActionResult AdicionarFuncionario() {

            return View();
        } 

        // Action para ADD Funcionario
            // Tenta verificar o ModelState Inválido
                // Instancia-se a DAO do Funcionário e em seguida é chamado o método DAO
        [HttpPost]
        public ActionResult AdicionarFuncionario(Funcionario func) {

            try {

                if (ModelState.IsValid) {

                    var funcDAO = new FuncionarioDAO();

                    if (funcDAO.AdicionarFuncionario(func)) {

                        ViewBag.Message = "Funcionario criado com sucesso!";
                    }
                }

                return ViewBag.Message("Retornando");
            }catch{
                return View("SelecionarFuncionarios");
            }
        }

        // Action para 1ª Etapa do Update de Funcionario
             // Instancia-se a DAO para Procura dos dados do Funcionário
                // Chama-se o método de Busca passando o ID do Funcionario
        public ActionResult AtualizarFuncionario(int id) {

            var funDAO = new FuncionarioDAO();

            return View(funDAO.SelecionarFuncionarios()
                                        .Find(func => func.IdFuncionario == id));
        }

        // Action para 2ª Etapa do Update de Funcionario
            // Com os dados resgatados, chama-se o método para Update passando o ID do Func
                // Após a ação, redireciona-se para o Action De Seleção dos Funcionários
        [HttpPost]
        public ActionResult AtualizarFuncionario(int id, Funcionario funcionario) {

            try {

                var funcDAO = new FuncionarioDAO();
                funcDAO.AtualizarFuncionario(funcionario);

                return RedirectToAction("SelecionarFuncionarios");
            } catch {

                return View();
            }
        }

        // Action para Delete de Funcionário
            // Resgata-se o ID do Funcionário e passa o mesmo para o Método de Exclusão
                // Após a ação, redireciona-se para o Actiond e Seleção dos Funcionários
        public ActionResult ExcluirFuncionario(int id) {

            try {

                var funcDAO = new FuncionarioDAO();

                if (funcDAO.ExcluirFuncionario(id)) {

                    ViewBag.AlertMsg = "Funcionário excluído com sucesso.";
                }

                return RedirectToAction("SelecionarFuncionarios");
            }catch {

                return View();
            }
        }
    }
}
