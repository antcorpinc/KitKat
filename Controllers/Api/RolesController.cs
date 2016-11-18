
using AutoMapper;
using KitKat.Data.Contracts;
using KitKat.Models;
using KitKat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  KitKat.Controllers.Api
{
    [Route("api/roles")]
    [Authorize]
    public class RolesController:Controller
    {
        private ILogger<RolesController> _logger;
        private IRoleRepository _repository;

        public RolesController(IRoleRepository repository,ILogger<RolesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var results = _repository.GetAllRoles();

            return Ok(Mapper.Map<IEnumerable<RoleViewModel>>(results));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]RoleViewModel role)
        {
            if(ModelState.IsValid)
            {
                // Convert View Model to Model
                var newRole = Mapper.Map<Role>(role);

                _repository.AddRole(newRole);
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{newRole.RoleName}", Mapper.Map<RoleViewModel>(newRole));
                }
                else
                {
                    return BadRequest("Failed to save changes");
                }
            }

            return BadRequest(ModelState);
        }
    }    
}