using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers
{
    [ApiController]
    [Route("tiles")]
    public class TileController : ControllerBase
    {
        private readonly IRepository<Tile> _tileRepository;
        private readonly IMapper _mapper;

        public TileController(IRepository<Tile> tileRepository, IMapper mapper)
        {
            _tileRepository = tileRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IEnumerable<TileDto>> GetAll()
        {
            var tilesFromRepo = await _tileRepository.GetAll();

            return _mapper.Map<IEnumerable<TileDto>>(tilesFromRepo);
        }

        [HttpGet("{id}", Name = "GetTile")]
        public async Task<IActionResult> GetTile(string id)
        {
            var tileFromRepo = await _tileRepository.GetById(id);

            if (tileFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TileDto>(tileFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<TileDto>> CreateTile(TileForCreationDto tile)
        {
            var tileEntity = _mapper.Map<Tile>(tile);
            await _tileRepository.Create(tileEntity);

            var tileToReturn = _mapper.Map<TileDto>(tileEntity);
            return CreatedAtRoute("GetTile", new { id = tileToReturn.Id }, tileToReturn);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTile(TileDto tile)
        {
            var tileEntity = _mapper.Map<Tile>(tile);
            await _tileRepository.Update(tileEntity);

            return Ok(_mapper.Map<TileDto>(tileEntity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTile(string id)
        {
            if (!await _tileRepository.DoesExistById(id))
            {
                return NotFound();
            }

            await _tileRepository.Archive(id);

            return NoContent();
        }
    }
}
