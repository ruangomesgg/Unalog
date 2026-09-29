using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Unalog.Core.Entities;
using Unalog.Core.Interfaces;
using Unalog.Web.ViewModels;

namespace Unalog.Web.Controllers;

public class MotoristaController : Controller
{
    private readonly IMotoristaRepository _repository;
    private readonly ILogger<MotoristaController> _logger;

    public MotoristaController(
        IMotoristaRepository repository,
        ILogger<MotoristaController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var motoristas = await _repository.ObterTodosAsync();

        var model = motoristas.Select(m => new MotoristaViewModel
        {
            Id = m.Id,
            Nome = m.Nome,
            Email = m.Email,
            Telefone = m.Telefone,
            ModeloVeiculo = m.ModeloVeiculo,
            Matricula = m.Matricula,
            PossuiTreinamentoCargaEspecial = m.PossuiTreinamentoCargaEspecial,
            PossuiRastreador = m.PossuiRastreador
        }).ToList();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var motorista = await _repository.ObterPorIdAsync(id);
        if (motorista is null)
        {
            return NotFound();
        }

        var model = new MotoristaViewModel
        {
            Id = motorista.Id,
            Nome = motorista.Nome,
            Email = motorista.Email,
            Telefone = motorista.Telefone,
            ModeloVeiculo = motorista.ModeloVeiculo,
            Matricula = motorista.Matricula,
            PossuiTreinamentoCargaEspecial = motorista.PossuiTreinamentoCargaEspecial,
            PossuiRastreador = motorista.PossuiRastreador
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, MotoristaViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var motorista = await _repository.ObterPorIdAsync(id);
        if (motorista is null)
        {
            return NotFound();
        }

        motorista.Nome = model.Nome;
        motorista.Email = model.Email;
        motorista.Telefone = model.Telefone;
        motorista.ModeloVeiculo = model.ModeloVeiculo;
        motorista.Matricula = model.Matricula;
        motorista.PossuiTreinamentoCargaEspecial = model.PossuiTreinamentoCargaEspecial;
        motorista.PossuiRastreador = model.PossuiRastreador;

        try
        {
            await _repository.AtualizarAsync(motorista);
            _logger.LogInformation(
                "Motorista atualizado com sucesso. ID {MotoristaId}",
                motorista.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao atualizar o motorista {MotoristaId}.", id);
            ModelState.AddModelError(string.Empty,
                "Não foi possível salvar as alterações. Tente novamente.");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(MotoristaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var motorista = new Motorista
        {
            Nome = model.Nome,
            Email = model.Email,
            Telefone = model.Telefone,
            ModeloVeiculo = model.ModeloVeiculo,
            Matricula = model.Matricula,
            PossuiTreinamentoCargaEspecial = model.PossuiTreinamentoCargaEspecial,
            PossuiRastreador = model.PossuiRastreador
        };

        try
        {
            await _repository.AdicionarAsync(motorista);
            _logger.LogInformation(
                "Motorista cadastrado com sucesso. ID {MotoristaId}",
                motorista.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao salvar o cadastro de motorista.");
            ModelState.AddModelError(string.Empty,
                "Não foi possível salvar o cadastro. Tente novamente.");
            return View(model);
        }

        return RedirectToAction(nameof(Sucesso));
    }

    [HttpGet]
    public IActionResult Sucesso()
    {
        return View();
    }
}
