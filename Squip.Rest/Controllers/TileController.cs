using System;
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
        public async Task<IActionResult> GetTile([FromQuery] Guid id)
        {
            var tileFromRepo = await _tileRepository.GetById(id.ToString());

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

        [HttpDelete("{tileId}")]
        public async Task<IActionResult> DeleteTile(Guid tileId)
        {
            if (!await _tileRepository.DoesExistById(tileId.ToString()))
            {
                return NotFound();
            }

            await _tileRepository.Archive(tileId.ToString());

            return NoContent();
        }
    }
}
