using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOnsOptionController : ControllerBase
    {
        private readonly IAddOnsOptionRepository _addOnsOptionRepository;

        public AddOnsOptionController(IAddOnsOptionRepository addOnsOptionRepository)
        {
            _addOnsOptionRepository = addOnsOptionRepository;
        }

        // GET: api/addonsoption/{businessId}/{productId}/{addOnsId}/all
        [HttpGet("{businessId}/{productId}/{addOnsId}/all")]
        public async Task<ActionResult<IReadOnlyList<AddOnsOption>>> GetAllAddOnsOptions(int businessId, int productId, int addOnsId)
        {
            var options = await _addOnsOptionRepository.GetAllAddOnsOptions(businessId, productId, addOnsId);
            var sortedOptions = options.OrderBy(x => x.Id);
            return Ok(sortedOptions);
        }

        // GET: api/addonsoption/{businessId}/{productId}/{addOnsId}/{optionId}
        [HttpGet("{businessId}/{productId}/{addOnsId}/{optionId}")]
        public async Task<ActionResult<AddOnsOption>> GetAddOnsOption(int businessId, int productId, int addOnsId, int optionId)
        {
            var option = await _addOnsOptionRepository.GetAddOnsOption(businessId, productId, addOnsId, optionId);
            if (option == null)
                return NotFound();
            return Ok(option);
        }

        // GET: api/addonsoption/paged
        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<AddOnsOption>>> GetAddOnsOptions(int businessId, int productId, int addOnsId, [FromQuery] PaginationParams paginationParams)
        {
            var pagedOptions = await _addOnsOptionRepository.GetAddOnsOptionsWithPage(businessId, productId, addOnsId, paginationParams);
            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                pagedOptions.TotalCount,
                pagedOptions.PageSize,
                pagedOptions.CurrentPage,
                pagedOptions.TotalPages
            }));
            return Ok(pagedOptions);
        }

        // POST: api/addonsoption
        [HttpPost]
        public async Task<ActionResult<AddOnsOption>> CreateAddOnsOption([FromBody] AddOnsOption addOnsOption)
        {
            if (addOnsOption == null)
                return BadRequest("AddOnsOption data cannot be null.");

            await _addOnsOptionRepository.CreateAddOnsOption(addOnsOption);
            return CreatedAtAction(nameof(GetAddOnsOption), new { businessId = addOnsOption.BusinessId, productId = addOnsOption.ProductId, addOnsId = addOnsOption.AddOnsId, optionId = addOnsOption.Id }, addOnsOption);
        }

        // PUT: api/addonsoption/{businessId}/{productId}/{addOnsId}/{optionId}
        [HttpPut("{businessId}/{productId}/{addOnsId}/{optionId}")]
        public async Task<ActionResult> UpdateAddOnsOption(int businessId, int productId, int addOnsId, int optionId, [FromBody] AddOnsOption newOption)
        {
            if (newOption == null)
                return BadRequest("AddOnsOption data cannot be null.");

            if (optionId != newOption.Id)
                return BadRequest("AddOnsOption ID mismatch.");

            var existingOption = await _addOnsOptionRepository.GetAddOnsOption(businessId, productId, addOnsId, optionId);
            if (existingOption == null)
                return NotFound();

            existingOption.OptionName = newOption.OptionName;
            existingOption.Price = newOption.Price;

            await _addOnsOptionRepository.UpdateAddOnsOption(existingOption);
            return NoContent();
        }

        // DELETE: api/addonsoption/{businessId}/{productId}/{addOnsId}/{optionId}
        [HttpDelete("{businessId}/{productId}/{addOnsId}/{optionId}")]
        public async Task<ActionResult> DeleteAddOnsOption(int businessId, int productId, int addOnsId, int optionId)
        {
            var option = await _addOnsOptionRepository.GetAddOnsOption(businessId, productId, addOnsId, optionId);
            if (option == null)
                return NotFound();

            await _addOnsOptionRepository.DeleteAddOnsOption(option);
            return NoContent();
        }
    }
}
