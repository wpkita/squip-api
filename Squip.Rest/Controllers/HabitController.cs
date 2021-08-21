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
    [Route("api/habits")]
    public class HabitController : ControllerBase
    {
        private readonly IRepository<Habit> _habitRepository;
        private readonly IMapper _mapper;

        public HabitController(IRepository<Habit> habitRepository, IMapper mapper)
        {
            _habitRepository = habitRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IEnumerable<HabitDto>> GetHabits()
        {
            var habitsFromRepo = await _habitRepository.GetAll();

            return _mapper.Map<IEnumerable<HabitDto>>(habitsFromRepo);
        }

        [HttpGet("{habitId}", Name = "GetHabit")]
        public async Task<IActionResult> GetHabit(Guid habitId)
        {
            var habitFromRepo = await _habitRepository.GetById(habitId.ToString());

            if (habitFromRepo == null) return NotFound();

            return Ok(_mapper.Map<HabitDto>(habitFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<HabitDto>> CreateHabit(HabitForCreationDto habit)
        {
            var habitEntity = _mapper.Map<Habit>(habit);
            await _habitRepository.Create(habitEntity);

            var habitToReturn = _mapper.Map<HabitDto>(habitEntity);
            return CreatedAtRoute("GetHabit", new {habitId = habitToReturn.Id}, habitToReturn);
        }

        [HttpDelete("{habitId}")]
        public async Task<IActionResult> DeleteHabit(Guid habitId)
        {
            if (!await _habitRepository.DoesExistById(habitId.ToString())) return NotFound();

            await _habitRepository.Archive(habitId.ToString());

            return NoContent();
        }
    }
}
