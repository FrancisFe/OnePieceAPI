namespace OnePieceAPI.DTOs.Piratas
{
    public class PirataDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Recompensa { get; set; }
    }
}
