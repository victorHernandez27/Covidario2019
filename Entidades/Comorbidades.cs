using Covid_19.Entidades.Enums;

namespace Covid_19.Entities
{
    internal class Comorbidade
    {
        public string NomeComorbidade { get; set; }

        public Comorbidade()
        {

        }

        public Comorbidade(string nomeComorbidade)
        {
            NomeComorbidade = nomeComorbidade;
        }

        public string DadosComorbidade()
        {
            return $@"
                        Nome:{NomeComorbidade}";
        }
        public string ConverterParaCSV()
        {
            return $"{NomeComorbidade}|";
        }
    }
}