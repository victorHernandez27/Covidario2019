using Covid_19.Entidades.Enums;
using Covid_19.Entities;
using System;
using System.IO;

namespace Covid_19
{
    internal class FilaPaciente
    {
        public Paciente Inicio { get; set; }
        public Paciente Fim { get; set; }

        private string NomeArquivo;

        public FilaPaciente(string nomeArquivo)
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

        public Paciente Push(Paciente novoPaciente)
        {
            novoPaciente.Proximo = null;

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
            SalvarCSV();
            return paciente;
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

        public Paciente Pop()
        {
            Paciente antigoPaciente = Inicio;
            if (EstaVazio()) return null;
            else if (Inicio.Proximo == null)
                Fim = Inicio = null;
            else
            {
                Inicio = Inicio.Proximo;
            }
            antigoPaciente.Proximo = null;

            SalvarCSV();
            return antigoPaciente;
        }

        public bool EstaVazio()
        {
            return (Inicio == null) && (Fim == null) ? true : false;
        }
    }


}