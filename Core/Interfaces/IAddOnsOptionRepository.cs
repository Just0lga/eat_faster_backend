using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAddOnsOptionRepository
    {
        // Create an AddOnsOption
        Task CreateAddOnsOption(AddOnsOption addOnsOption);

        // Delete an AddOnsOption
        Task DeleteAddOnsOption(AddOnsOption addOnsOption);

        // Get an AddOnsOption
        Task<AddOnsOption> GetAddOnsOption(int businessId, int productId, int addOnsId, int optionId);

        // Get all AddOnsOptions for an AddOns
        Task<IReadOnlyList<AddOnsOption>> GetAllAddOnsOptions(int businessId, int productId, int addOnsId);

        // Get AddOnsOptions with pagination
        Task<PagedList<AddOnsOption>> GetAddOnsOptionsWithPage(int businessId, int productId, int addOnsId, PaginationParams paginationParams);

        // Update an AddOnsOption
        Task UpdateAddOnsOption(AddOnsOption addOnsOption);
    }
}
