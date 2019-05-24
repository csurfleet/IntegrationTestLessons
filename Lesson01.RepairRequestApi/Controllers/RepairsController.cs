using System;
using System.Linq;
using System.Threading.Tasks;
using Lesson01.RepairRequestApi.Models;
using Lesson01.RepairRequestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lesson01.RepairRequestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairsController : ControllerBase
    {
        private readonly IAllocationService _allocationService;
        private readonly IUsersService _usersService;

        public RepairsController(IAllocationService allocationService, IUsersService usersService)
        {
            _allocationService = allocationService ?? throw new ArgumentNullException(nameof(allocationService));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        // POST api/repairs
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RepairRequest value)
        {
            if (!IsRiskyJob(value.RepairType))
            {
                _allocationService.AllocateRepair(value);
                return Ok();
            }

            var user = await _usersService.Get(value.RequestingUserId);

            if (user.UserRoles.Contains(UserRole.RiskAssessor))
            {
                _allocationService.AllocateRepair(value);
                return Ok();
            }

            return BadRequest($"User '{value.RequestingUserId}' cannot request risky repairs");
        }

        public bool IsRiskyJob(RepairType repairType)
        {
            var riskyTypes = new[] { RepairType.BoilerReplacement };

            return riskyTypes.Contains(repairType);
        }
    }
}
