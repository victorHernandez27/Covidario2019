namespace Covid_19.Entities
{
    internal class Leitos
    {
        public int TotalLeitos { get; set; }
        public int LeitosOcupados { get; set; }
        public int LeitosDisponiveis => TotalLeitos - LeitosOcupados;
        public string CaminhaArquivosConfiguracao { get; set; }

        public bool PossuiVaga()
        {
            return TotalLeitos - LeitosOcupados > 0;
        }
    }
}