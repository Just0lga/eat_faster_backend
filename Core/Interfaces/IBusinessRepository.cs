using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBusinessRepository
    {
        //Create a business
        Task CreateBusiness(Business business);

        //Delete a business
        Task DeleteBusiness(Business business);

        //Get all businesses async
        Task<IReadOnlyList<Business>> GetAllBusinesses();

        //Get a business async
        Task<Business> GetBusiness(int id);

        //Get businesses with page list
        Task<PagedList<Business>> GetBusinessesWithPage(PaginationParams paginationParams);

        //Update a business
        Task UpdateBusiness(Business business); 
    }
}
