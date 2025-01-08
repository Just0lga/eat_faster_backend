using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProgrammerRepository
    {
        // Create a programmer
        Task CreateProgrammer(Programmer programmer);

        // Delete a programmer
        Task DeleteProgrammer(Programmer programmer);

        // Get a programmer
        Task<Programmer> GetProgrammer(int businessId, int programmerId);

        // Get all programmers async
        Task<IReadOnlyList<Programmer>> GetAllProgrammers(int businessId);

        // Get programmers with pagination
        Task<PagedList<Programmer>> GetProgrammersWithPage(int businessId, PaginationParams paginationParams);

        // Update a programmer
        Task UpdateProgrammer(Programmer programmer);
    }
}
