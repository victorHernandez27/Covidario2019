using Covid_19.Entities;
using System;
using System.IO;

namespace Covid_19
{
    internal class ListaPacientes
    {
        public Paciente Inicio { get; set; }
        public Paciente Fim { get; set; }
        private string NomeArquivo;
        public ListaPacientes(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            Inicio = Fim = null;
        }

        public void LerArquivo()
        {
            if (File.Exists(NomeArquivo))
                using (StreamReader sr = new StreamReader(NomeArquivo))
                {
                    int count = 0;
                    string line = sr.ReadLine();

                    while (line != null && line != "")
                    {
                        string[] values = line.Split(';');

                        Paciente novoPaciente = new Paciente(values);

                        Paciente paciente = Inicio;
                        if (EstaVazio())
                        {
                            Inicio = Fim = novoPaciente;
                        }
                        else
                        {
                            novoPaciente.Anterior = Fim;
                            Fim.Proximo = novoPaciente;
                            Fim = novoPaciente;
                        }

                        count++;
                        line = sr.ReadLine();
                    }
                }

        }

        public void SalvarCSV()
        {
            StreamWriter sw = new StreamWriter(NomeArquivo);

            if (EstaVazio())
            {
                sw.WriteLine();
            }
            else
            {
                Paciente paciente = Inicio;
                while (paciente != null)
                {
                    sw.WriteLine(paciente.ConverterParaCSV());

                    paciente = paciente.Proximo;
                }
            }
            sw.Close();
        }

        public Paciente[] ObterTodos()
        {
            Paciente[] pacientes = new Paciente[Count()];
            int count = 0;

            if (EstaVazio()) return null;
            else
            {
                Paciente paciente = Inicio;
                do
                {
                    pacientes[count++] = paciente;

                    paciente = paciente.Proximo;

                } while (paciente != null);
            }

            return pacientes;
        }

        public int Count()
        {
            if (EstaVazio()) return 0;
            else
            {
                Paciente paciente = Inicio;
                int count = 0;
                do
                {
                    count++;
                    paciente = paciente.Proximo;

                } while (paciente != null);
                return count;
            }

        }
        public Paciente[] BuscaPeloCPFNome(string busca)
        {
            Paciente[] buscaPaciente = new Paciente[Count()];
            int countSearch = 0;

            if (EstaVazio()) return null;
            else
            {
                Paciente paciente = Inicio;
                do
                {
                    if (paciente.Nome.ToLower().Contains(busca.ToLower()) || paciente.CPF.Contains(busca))
                    {
                        buscaPaciente[countSearch++] = paciente;
                    }
                    paciente = paciente.Proximo;
                } while (paciente != null);
            }
            return buscaPaciente;
        }

        public Paciente ObterPeloCPF(string cpf)
        {
            Paciente paciente = Inicio;

            if (EstaVazio()) return null;
            else
            {

                while (paciente.Proximo != null)
                {

                    if (cpf == paciente.CPF)
                    {
                        return paciente;
                    }
                    paciente = paciente.Proximo;
                }
            }
            return null;
        }

        public bool Editar(Paciente paciente)
        {
            Paciente buscaPaciente = Inicio;

            if (EstaVazio()) return false;
            else
            {

                while (paciente.Proximo != null)
                {

                    if (buscaPaciente.CPF == paciente.CPF)
                    {
                        buscaPaciente = paciente;
                        return true;
                    }
                    paciente = paciente.Proximo;
                }
            }
            SalvarCSV();
            return false;
        }

        public Paciente Push(Paciente novoPaciente)
        {
            novoPaciente.Proximo = null;

            if (EstaVazio())
            {
                novoPaciente.Senha = 1;
                Inicio = Fim = novoPaciente;
            }
            else
            {
                novoPaciente.Senha = Fim.Senha + 1;
                novoPaciente.Anterior = Fim;
                Fim.Proximo = novoPaciente;
                Fim = novoPaciente;
            }
            SalvarCSV();
            return novoPaciente;
        }


        public bool Pop(string CPF)
        {
            if (EstaVazio()) return false;
            else if (CPF == Inicio.CPF)
            {
                if (Inicio.Proximo == null)
                    Fim = Inicio = null;
                else
                {
                    Inicio = Inicio.Proximo;
                    Inicio.Anterior = null;
                }
            }
            else
            {
                Paciente paciente = Inicio;
                do
                {
                    if (paciente.Proximo.CPF == CPF)
                        if (paciente.Proximo.Proximo == null)
                        {
                            Fim = paciente;
                            paciente.Proximo = null;
                        }
                        else
                        {
                            paciente.Anterior = paciente.Anterior;

                            paciente.Proximo = paciente.Proximo.Proximo;

                            paciente.Proximo.Anterior = paciente;
                            break;
                        }

                    paciente = paciente.Proximo;

                } while (paciente != null);

            }
            SalvarCSV();
            return true;
        }


        public bool EstaVazio()
        {
            return (Inicio == null) && (Fim == null) ? true : false;
        }
    }
}