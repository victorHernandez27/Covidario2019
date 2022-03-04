namespace Covid_19.Entidades
{
    internal class SintomasCovid
    {
        public bool DificuldadeRespirar { get; set; }
        public bool PerdaMotora { get; set; }
        public bool PerdaPaladar { get; set; }
        public bool PerdaOlfato { get; set; }

        public bool SuspetaCovid()
        {
            if (DificuldadeRespirar) return true;
            if (PerdaMotora) return true;
            if (PerdaPaladar) return true;
            if (PerdaOlfato) return true;

            return false;
        }

        public string ConverterParaCSV()
        {
            return $"{DificuldadeRespirar};{PerdaMotora};{PerdaPaladar};{PerdaOlfato}";
        }

    }
}