namespace Unalog.Core.Entities;

public class Motorista
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public bool PossuiTreinamentoCargaEspecial { get; set; }
    public bool PossuiRastreador { get; set; }
    public string ModeloVeiculo { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
}