using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService UnitService;
        private readonly ILogger<UnitController> logger;
        public UnitController(IUnitService _UnitService)
        {
            this.UnitService = _UnitService;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            return  Ok(await UnitService.GetAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            var Unit = await UnitService.GetAsync(id);
            if(Unit == null)
            {
                return NotFound();
            }
            return Ok(Unit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(int id, Unit Unit)
        {
            if(Unit == null || id != Unit.UnitId)
            {
                return BadRequest();
            }
            await UnitService.UpdateAsync(Unit);
            return Ok(Unit);
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit Unit)
        {
            if (Unit == null)
            {
                return BadRequest();
            }
            await UnitService.CreateAsync(Unit);
            return Ok(Unit);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            await UnitService.DeleteAsync(id);
            return Ok();
        }

        private bool UnitExists(int id)
        {
            return UnitService.IsExists(id);
        }
    }
}
