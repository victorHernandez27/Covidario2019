using Covid_19.Entidades;
using Covid_19.Entidades.Enums;
using System;

namespace Covid_19.Entities
{
    internal class Paciente
    {
        public int Senha { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        //public Sexo Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAlta { get; set; }
        public StatusPaciente StatusPaciente { get; set; }
        public bool TesteCovidPositivo { get; set; }
        public Triagem Triagem { get; set; }
        public Comorbidade[] Comorbidades { get; set; }

        public int Idade => (DateTime.Now - DataNascimento).Days / 365;
        public bool Preferencial => Idade >= 60;

        public Paciente Proximo { get; set; }
        public Paciente Anterior { get; set; }

        public Paciente()
        {
            Triagem = new Triagem();
        }



        public Paciente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Triagem = new Triagem();
        }

        public Paciente(string nome, string cpf, DateTime dataNascimento, int diasSintomas, bool possuiComorbidade)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Triagem = new Triagem();
            Triagem.DiasSintomas = diasSintomas;
            Triagem.PossuiComorbidade = possuiComorbidade;
        }

        public bool PossuiNecessidadeInternacao()
        {
            if (Triagem.Saturacao < 90) return true;

            return false;
        }

        public string DadosCompletosPaciente()
        {
            string comorbidades = "";
            if (Comorbidades != null)
                foreach (var comorbidade in Comorbidades)
                    comorbidades += comorbidade.DadosComorbidade();

            return $@"
                      Nome: {Nome}
                      CPF: {CPF}
                      Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}
                      Idade: {Idade}
                      Status Paciente?: {StatusPaciente}
                      Comorbidades: { comorbidades ?? "sem registros"}
                      Possui Comorbidades?: {(Triagem.PossuiComorbidade ? "Sim" : "Não")}
                      {Triagem.DadosTriagem()}";
        }

        public string DadosMinimosPaciente()
        {
            return $@"
                      Senha: {Senha}
                      Nome: {Nome}
                      CPF: {CPF}
                      Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}
                      Idade: {Idade} anos
                      {(Preferencial ? "Fila preferencial!" : "Fila normal!")}";
        }

        public string ConverterParaCSV()
        {
            string comorbidades = "";
            if (Comorbidades != null)
                foreach (var comorbidade in Comorbidades)
                    comorbidades += comorbidade.ConverterParaCSV();

            return $"{Nome};{CPF};{DataNascimento};{DataAlta};{StatusPaciente};{TesteCovidPositivo};{Triagem.ConverterParaCSV()};{comorbidades}";
        }

        public Paciente(string[] valores)
        {
            if (valores[0] != "") Nome = valores[0];
            if (valores[1] != "") CPF = valores[1];
            if (DateTime.TryParse(valores[2], out DateTime dataNascimento)) DataNascimento = dataNascimento;
            if (DateTime.TryParse(valores[3], out DateTime dataAlta)) DataAlta = dataAlta;
            if (valores[4] != "0") StatusPaciente = (StatusPaciente)Enum.Parse(typeof(StatusPaciente), valores[4]);
            if (valores[5] == "True") TesteCovidPositivo = true;

            Triagem = new Triagem();
            //Triagem
            if (double.TryParse(valores[6], out double pressao)) Triagem.Pressao = pressao;
            if (int.TryParse(valores[7], out int batimentosCardiacos)) Triagem.BatimentosCardiacos = batimentosCardiacos;
            if (int.TryParse(valores[8], out int saturacao)) Triagem.Saturacao = saturacao;
            if (double.TryParse(valores[9], out double temperatura)) Triagem.Temperatura = temperatura;
            if (int.TryParse(valores[10], out int diasSintomas)) Triagem.DiasSintomas = diasSintomas;
            if (valores[11] == "True") Triagem.PossuiComorbidade = true;
            if (valores[12] == "True") Triagem.ResultadoTesteCovid = true;

            //Sintomas
            if (valores[13] == "True") Triagem.SintomasCovid.DificuldadeRespirar = true;
            if (valores[14] == "True") Triagem.SintomasCovid.PerdaMotora = true;
            if (valores[15] == "True") Triagem.SintomasCovid.PerdaPaladar = true;
            if (valores[16] == "True") Triagem.SintomasCovid.PerdaOlfato = true;
            if (valores[17] != "")
            {
                Comorbidade[] comorbidades = new Comorbidade[valores[17].Split("|").Length - 1];

                string[] valoresComobidade = valores[17].Split("|");

                for (int i = 0; i < valoresComobidade.Length - 1; i++)
                {
                    comorbidades[i] = new Comorbidade(valoresComobidade[i]);
                }

                Comorbidades = comorbidades;
            }

        }
    }
}