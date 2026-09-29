using Microsoft.EntityFrameworkCore;
using Unalog.Core.Entities;
using Unalog.Core.Interfaces;
using Unalog.Infrastructure.Data;

namespace Unalog.Infrastructure.Repositories;

public class MotoristaRepository : IMotoristaRepository
{
    private readonly ApplicationDbContext _context;

    public MotoristaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Motorista motorista)
    {
        await _context.Motoristas.AddAsync(motorista);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Motorista>> ObterTodosAsync()
    {
        return await _context.Motoristas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Motorista?> ObterPorIdAsync(int id)
    {
        return await _context.Motoristas.FindAsync(id);
    }

    public async Task AtualizarAsync(Motorista motorista)
    {
        _context.Motoristas.Update(motorista);
        await _context.SaveChangesAsync();
    }
}
