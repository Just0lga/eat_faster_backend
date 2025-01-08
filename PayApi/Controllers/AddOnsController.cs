using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOnsController : ControllerBase
    {
        private readonly IAddOnsRepository _addOnsRepository;

        public AddOnsController(IAddOnsRepository addOnsRepository)
        {
            _addOnsRepository = addOnsRepository;
        }

        // GET: api/addons/{businessId}/{productId}/all
        [HttpGet("{businessId}/{productId}/all")]
        public async Task<ActionResult<IReadOnlyList<AddOns>>> GetAllAddOns(int businessId, int productId)
        {
            var addOns = await _addOnsRepository.GetAllAddOns(businessId, productId);
            var sortedAddOns = addOns.OrderBy(x => x.Id);
            return Ok(sortedAddOns);
        }

        // GET: api/addons/{businessId}/{productId}/{addOnId}
        [HttpGet("{businessId}/{productId}/{addOnId}")]
        public async Task<ActionResult<AddOns>> GetAddOn(int businessId, int productId, int addOnId)
        {
            var addOn = await _addOnsRepository.GetAddOn(businessId, productId, addOnId);
            if (addOn == null)
                return NotFound();
            return Ok(addOn);
        }

        // GET: api/addons/paged
        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<AddOns>>> GetAddOns(int businessId, int productId, [FromQuery] PaginationParams paginationParams)
        {
            var pagedAddOns = await _addOnsRepository.GetAddOnsWithPage(businessId, productId, paginationParams);
            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                pagedAddOns.TotalCount,
                pagedAddOns.PageSize,
                pagedAddOns.CurrentPage,
                pagedAddOns.TotalPages
            }));
            return Ok(pagedAddOns);
        }

        // POST: api/addons
        [HttpPost]
        public async Task<ActionResult<AddOns>> CreateAddOn([FromBody] AddOns addOn)
        {
            if (addOn == null)
                return BadRequest("AddOn data cannot be null.");

            await _addOnsRepository.CreateAddOn(addOn);
            return CreatedAtAction(nameof(GetAddOn), new { businessId = addOn.BusinessId, productId = addOn.ProductId, addOnId = addOn.Id }, addOn);
        }

        // PUT: api/addons/{businessId}/{productId}/{addOnId}
        [HttpPut("{businessId}/{productId}/{addOnId}")]
        public async Task<ActionResult> UpdateAddOn(int businessId, int productId, int addOnId, [FromBody] AddOns newAddOn)
        {
            if (newAddOn == null)
                return BadRequest("AddOn data cannot be null.");

            if (addOnId != newAddOn.Id)
                return BadRequest("AddOn ID mismatch.");

            var existingAddOn = await _addOnsRepository.GetAddOn(businessId, productId, addOnId);
            if (existingAddOn == null)
                return NotFound();

            existingAddOn.AddOnsName = newAddOn.AddOnsName;
            existingAddOn.AddOnsDescription = newAddOn.AddOnsDescription;

            await _addOnsRepository.UpdateAddOn(existingAddOn);
            return NoContent();
        }

        // DELETE: api/addons/{businessId}/{productId}/{addOnId}
        [HttpDelete("{businessId}/{productId}/{addOnId}")]
        public async Task<ActionResult> DeleteAddOn(int businessId, int productId, int addOnId)
        {
            var addOn = await _addOnsRepository.GetAddOn(businessId, productId, addOnId);
            if (addOn == null)
                return NotFound();

            await _addOnsRepository.DeleteAddOn(addOn);
            return NoContent();
        }
    }
}
