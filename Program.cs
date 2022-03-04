using Covid_19.Entidades.Enums;
using Covid_19.Entities;
using System;

namespace Covid_19
{
    internal class Program
    {
        static ServicoCovid service = new ServicoCovid();
        static void Main(string[] args)
        {
            
            service.filaPacientePreferencial.LerArquivo();
            service.filaPacienteNormal.LerArquivo();
            service.filaPacienteInternados.LerArquivo();
            service.listaPacientes.LerArquivo();       

            Menu();
        }

        public static void Menu()
        {

            Console.WriteLine(@$"
                                1) Cadastro de um Paciente
                                2) Buscar Paciente
                                3) Triagem
                                4) Mudar configurações do sistema
                                5) Dar alta para paciente
                                6) Mostra lista de espera para internação
                                ------------------------------
                                0) - Sair
Total de Leitos:{service.configuracaoSistema.TotalLeitos}
Disponíveis: {service.configuracaoSistema.LeitosDisponiveis}
");

            string option = Console.ReadLine();

            switch (option)
            {
                case "0": Environment.Exit(0); break;

                case "1":
                    Console.Clear();
                    service.CadastroPaciente();
                    BackMenu();
                    break;

                case "2":
                    Console.Clear();
                    service.ObterPacientesPorNomeCPF();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    service.Triagem();
                    BackMenu();
                    break;

                case "4":
                    Console.Clear();
                    service.ConfiguracoesSistema();
                    BackMenu();
                    break;

                case "5":
                    Console.Clear();
                    service.DarAltaPaciente();
                    BackMenu();
                    break;

                case "6":
                    Console.Clear();
                    service.ListaEspera();
                    BackMenu();
                    break;

                default:
                    Console.WriteLine("Opção inválida! ");
                    BackMenu();
                    break;
            }

        }

        public static void BackMenu()
        {
            Console.WriteLine("\n Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
    }
}