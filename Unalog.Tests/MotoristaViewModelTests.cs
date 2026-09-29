using System.ComponentModel.DataAnnotations;
using Unalog.Web.ViewModels;

namespace Unalog.Tests;

public class MotoristaViewModelTests
{
    [Fact]
    public void CamposObrigatorios_Vazios_SaoInvalidos()
    {
        var model = new MotoristaViewModel();
        var resultados = new List<ValidationResult>();

        var valido = Validator.TryValidateObject(
            model,
            new ValidationContext(model),
            resultados,
            validateAllProperties: true);

        Assert.False(valido);
        Assert.Contains(resultados, erro => erro.MemberNames.Contains(nameof(model.Nome)));
        Assert.Contains(resultados, erro => erro.MemberNames.Contains(nameof(model.Email)));
        Assert.Contains(resultados, erro => erro.MemberNames.Contains(nameof(model.Telefone)));
        Assert.Contains(resultados, erro => erro.MemberNames.Contains(nameof(model.ModeloVeiculo)));
        Assert.Contains(resultados, erro => erro.MemberNames.Contains(nameof(model.Matricula)));
    }

    [Fact]
    public void DadosValidos_PassamNaValidacao()
    {
        var model = CriarModelValido();
        var resultados = new List<ValidationResult>();

        var valido = Validator.TryValidateObject(
            model,
            new ValidationContext(model),
            resultados,
            validateAllProperties: true);

        Assert.True(valido);
        Assert.Empty(resultados);
    }

    [Fact]
    public void EmailInvalido_FalhaNaValidacao()
    {
        var model = CriarModelValido();
        model.Email = "email-invalido";
        var resultados = new List<ValidationResult>();

        var valido = Validator.TryValidateObject(
            model,
            new ValidationContext(model),
            resultados,
            validateAllProperties: true);

        Assert.False(valido);
        Assert.Contains(resultados, erro => erro.MemberNames.Contains(nameof(model.Email)));
    }

    private static MotoristaViewModel CriarModelValido() => new()
    {
        Nome = "Motorista Teste",
        Email = "motorista@exemplo.com",
        Telefone = "11987654321",
        ModeloVeiculo = "Caminhão",
        Matricula = "ABC1234"
    };
}
