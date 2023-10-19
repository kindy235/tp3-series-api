using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Models.EntityFramework;
using SeriesApi.Models.Repository;

namespace SeriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IDataRepository<Serie> dataRepository;
        //private readonly SeriesDbContext _context;

        public SeriesController(IDataRepository<Serie> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Series
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            var series = await dataRepository.GetAllAsync();
            if (series == null)
            {
                return NotFound();
            }
            return Ok(series.Value);
        }

        // GET: api/Series/GetById/5
        [Route("[action]/{id}")]
        [HttpGet]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Serie>> GetSerieById(int id)
        {
            var serie = await dataRepository.GetByIdAsync(id);

            if (serie == null)
            {
                return NotFound();
            }

            if (serie.Value == null)
            {
                return NotFound();
            }

            return Ok(serie.Value);
        }

        // GET: api/Series/GetByTitle/title
        [Route("[action]/{title}")]
        [HttpGet]
        [ActionName("GetByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSerieByTitle(string title)
        {
           
            var series = await dataRepository.GetAllByStringAsync(title);

            if (series == null)
            {
                return NotFound();
            }

            return Ok(series.Value);
        }
    }
}
