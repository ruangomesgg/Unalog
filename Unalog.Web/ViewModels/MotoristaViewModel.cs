using System.ComponentModel.DataAnnotations;

namespace Unalog.Web.ViewModels;

public class MotoristaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o telefone.")]
    [Phone(ErrorMessage = "Informe um telefone válido.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o modelo do veículo.")]
    [Display(Name = "Modelo do veículo")]
    public string ModeloVeiculo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a matrícula.")]
    [Display(Name = "Matrícula")]
    public string Matricula { get; set; } = string.Empty;

    [Display(Name = "Tem treinamento para cargas especiais?")]
    public bool PossuiTreinamentoCargaEspecial { get; set; }

    [Display(Name = "O veículo possui rastreador?")]
    public bool PossuiRastreador { get; set; }
}
