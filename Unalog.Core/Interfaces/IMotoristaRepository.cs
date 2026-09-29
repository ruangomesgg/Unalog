using System.Collections.Generic;
using System.Threading.Tasks;
using Unalog.Core.Entities;

namespace Unalog.Core.Interfaces;

public interface IMotoristaRepository
{
    Task AdicionarAsync(Motorista motorista);
    Task<IEnumerable<Motorista>> ObterTodosAsync();
    Task<Motorista?> ObterPorIdAsync(int id);
    Task AtualizarAsync(Motorista motorista);
}
