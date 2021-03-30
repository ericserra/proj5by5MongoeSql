namespace ProjApiCliente.Models
{
    public class Cliente
    {
        #region Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string NomeMae { get; set; }
        [foreignkey("Endereco")]
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        #endregion
    }
}