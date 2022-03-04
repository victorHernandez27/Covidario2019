namespace Covid_19.Entidades
{
    internal class Triagem
    {
        public double Pressao { get; set; }
        public int BatimentosCardiacos { get; set; }
        public int Saturacao { get; set; }
        public double Temperatura { get; set; }
        public int DiasSintomas { get; set; }
        public bool PossuiComorbidade { get; set; }
        public bool ResultadoTesteCovid { get; set; }
        public SintomasCovid SintomasCovid { get; set; }

        public Triagem()
        {
            SintomasCovid = new SintomasCovid();
        }

        public bool NecessitaFazerExameCovid()
        {
            return DiasSintomas >= 3 && SintomasCovid.SuspetaCovid() ? true : false;
        }

        public string DadosTriagem()
        {
            return $@"Sintomas Covid:
                       Pressão: {Pressao}
                       Batimentos Cardiacos: {BatimentosCardiacos}
                       Saturacao: {Saturacao}
                       Temperatura: {Temperatura}
                       Dias Sintomas: {DiasSintomas}";
        }

        public string ConverterParaCSV()
        {
            return $"{Pressao};{BatimentosCardiacos};{Saturacao};{Temperatura};{DiasSintomas};{PossuiComorbidade};{ResultadoTesteCovid};{SintomasCovid.ConverterParaCSV()}";
        }
    }
}