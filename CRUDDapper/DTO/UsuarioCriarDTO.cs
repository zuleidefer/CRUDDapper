namespace CRUDDapper.DTO
{
    public class UsuarioCriarDTO
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; } // 1 - Ativo ;  - Inativo
        public string Senha { get; set; }

    }
}
