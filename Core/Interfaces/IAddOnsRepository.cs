using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAddOnsRepository
    {
        // Create an AddOn
        Task CreateAddOn(AddOns addOn);

        // Delete an AddOn
        Task DeleteAddOn(AddOns addOn);

        // Get an AddOn
        Task<AddOns> GetAddOn(int businessId, int productId, int addOnId);

        // Get all AddOns for a product
        Task<IReadOnlyList<AddOns>> GetAllAddOns(int businessId, int productId);

        // Get AddOns with pagination
        Task<PagedList<AddOns>> GetAddOnsWithPage(int businessId, int productId, PaginationParams paginationParams);

        // Update an AddOn
        Task UpdateAddOn(AddOns addOn);
    }
}
