using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL.Services;
using BLL.Interfaces;
using BLL.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GanreController : Controller
    {
        private IMapper _mapper;
        private IGanreService _ganreService;
        public GanreController(IGanreService ganreService, IMapper mapper)
        {
            _ganreService = ganreService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            var ganres = _mapper.Map<IEnumerable<GanreViewModel>>(await _ganreService.GetAllAsync());
            return new JsonResult(ganres);
        }
        [HttpGet("{id}", Name = nameof(GetGanre))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGanre(int id)
        {
            var ganre = _mapper.Map<GanreViewModel>(await _ganreService.GetByIdAsync(id));
            if (ganre == null)
            {
                return NotFound();
            }
            return Ok(ganre);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGame(GanreViewModel game)
        {
            var ganreModel = _mapper.Map<GanreModel>(game);
            await _ganreService.AddAsync(ganreModel);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] GanreViewModel game)
        {
            var ganreModel = _mapper.Map<GanreModel>(game);
            await _ganreService.UpdateAsync(ganreModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            await _ganreService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
