using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammerController : ControllerBase
    {
        private readonly IProgrammerRepository _programmerRepository;

        public ProgrammerController(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        // GET: api/programmers/{businessId}/all
        [HttpGet("{businessId}/all")]
        public async Task<ActionResult<IReadOnlyList<Programmer>>> GetAllProgrammers(int businessId)
        {
            var programmers = await _programmerRepository.GetAllProgrammers(businessId);
            var sortedProgrammers = programmers.OrderBy(x => x.Id);
            return Ok(sortedProgrammers);
        }

        // GET: api/programmers/{businessId}/{programmerId}
        [HttpGet("{businessId}/{programmerId}")]
        public async Task<ActionResult<Programmer>> GetProgrammer(int businessId, int programmerId)
        {
            var programmer = await _programmerRepository.GetProgrammer(businessId, programmerId);
            if (programmer == null)
                return NotFound();
            return Ok(programmer);
        }

        // GET: api/programmers/paged
        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<Programmer>>> GetProgrammers(int businessId, [FromQuery] PaginationParams paginationParams)
        {
            var pagedProgrammers = await _programmerRepository.GetProgrammersWithPage(businessId, paginationParams);
            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                pagedProgrammers.TotalCount,
                pagedProgrammers.PageSize,
                pagedProgrammers.CurrentPage,
                pagedProgrammers.TotalPages
            }));
            return Ok(pagedProgrammers);
        }

        // POST: api/programmers
        [HttpPost]
        public async Task<ActionResult<Programmer>> CreateProgrammer([FromBody] Programmer programmer)
        {
            if (programmer == null)
                return BadRequest("Programmer data cannot be null.");

            await _programmerRepository.CreateProgrammer(programmer);
            return CreatedAtAction(nameof(GetProgrammer), new { businessId = programmer.BusinessId, programmerId = programmer.Id }, programmer);
        }

        // PUT: api/programmers/{businessId}/{programmerId}
        [HttpPut("{businessId}/{programmerId}")]
        public async Task<ActionResult> UpdateProgrammer(int businessId, int programmerId, [FromBody] Programmer newProgrammer)
        {
            if (newProgrammer == null)
                return BadRequest("Programmer data cannot be null.");

            if (programmerId != newProgrammer.Id)
                return BadRequest("Programmer ID mismatch.");

            var existingProgrammer = await _programmerRepository.GetProgrammer(businessId, programmerId);
            if (existingProgrammer == null)
                return NotFound();

            existingProgrammer.ProgrammerName = newProgrammer.ProgrammerName;
            existingProgrammer.ProgrammerEmail = newProgrammer.ProgrammerEmail;
            existingProgrammer.ProgrammerPhone = newProgrammer.ProgrammerPhone;

            await _programmerRepository.UpdateProgrammer(existingProgrammer);
            return NoContent();
        }

        // DELETE: api/programmers/{businessId}/{programmerId}
        [HttpDelete("{businessId}/{programmerId}")]
        public async Task<ActionResult> DeleteProgrammer(int businessId, int programmerId)
        {
            var programmer = await _programmerRepository.GetProgrammer(businessId, programmerId);
            if (programmer == null)
                return NotFound();

            await _programmerRepository.DeleteProgrammer(programmer);
            return NoContent();
        }
    }
}
