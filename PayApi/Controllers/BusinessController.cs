using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessController(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        // GET: api/business
        [HttpGet("/all")]
        public async Task<ActionResult<IReadOnlyList<Business>>> GetAllBusinesses()
        {
            var businesses = await _businessRepository.GetAllBusinesses();
            return Ok(businesses);
        }

        // GET: api/business/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> GetBusiness(int id)
        {
            var business = await _businessRepository.GetBusiness(id);

            if (business == null)
                return NotFound();

            return Ok(business);
        }

        //Get businesses with page list
        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<Business>>> GetBusinesses([FromQuery] PaginationParams paginationParams)
        {
            var pagedBusinesses = await _businessRepository.GetBusinessesWithPage(paginationParams);

            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                pagedBusinesses.TotalCount,
                pagedBusinesses.PageSize,
                pagedBusinesses.CurrentPage,
                pagedBusinesses.TotalPages
            }));

            return Ok(pagedBusinesses); 
        }

        // POST: api/business
        [HttpPost]
        public async Task<ActionResult> CreateBusiness([FromBody] Business business)
        {
            if (business == null)
                return BadRequest("Business object is null");

            await _businessRepository.CreateBusiness(business);
            return Ok(business);
        }

        // PUT: api/business/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBusiness(int id, Business newBusiness)
        {
            if (newBusiness == null)
                return BadRequest("Business data cannot be null.");

            var oldBusiness = await _businessRepository.GetBusiness(id); 

            if (oldBusiness == null)
                return NotFound();

            oldBusiness.BusinessName = newBusiness.BusinessName;
            oldBusiness.BusinessDescription = newBusiness.BusinessDescription;
            oldBusiness.BusinessAddress = newBusiness.BusinessAddress;
            oldBusiness.BusinessImageURL = newBusiness.BusinessImageURL;
            oldBusiness.BusinessWorkingHours = newBusiness.BusinessWorkingHours;

            await _businessRepository.UpdateBusiness(oldBusiness); 

            return NoContent();
        }


        // DELETE: api/business/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusiness(int id)
        {
            var business = await _businessRepository.GetBusiness(id);
            if (business == null)
                return NotFound();

            await _businessRepository.DeleteBusiness(business);
            return NoContent();
        }
    }
}


