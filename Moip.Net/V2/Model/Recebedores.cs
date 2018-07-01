namespace Moip.Net.V2.Model
{
    /// <summary>
    /// Estrutura de recebedores dos pagamentos.
    /// </summary>
    public class Recebedores
    {
        public ReceiverType? Type { get; set; }
        public ContaMoip moipAccount { get; set; }
        public ValoresRecebedor Amount { get; set; }
        public bool FeePayor { get; set; }        
    }
}