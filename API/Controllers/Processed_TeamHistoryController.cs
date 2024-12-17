using Microsoft.AspNetCore.Mvc;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;




namespace API.Controllers.Controller_APITestClasses
{
    [ApiController]
    [Route("api/[controller]")]

    public class Processed_TeamHistoryController : ControllerBase
    {
        private readonly IAppLogger _appLogger;
        private readonly IProcessed_TeamNameHistoryRepository _processed_TeamNameHistoryRepository;



        public Processed_TeamHistoryController(IAppLogger appLogger, IProcessed_TeamNameHistoryRepository processed_TeamNameHistoryRepository)
        {
            _appLogger = appLogger;
            _processed_TeamNameHistoryRepository = processed_TeamNameHistoryRepository;
        }


        [HttpGet("{teamName}")]
        public async Task<IActionResult> GetTeamHistory(string teamName)
        {
            try
            {
                var result = await _processed_TeamNameHistoryRepository.TESTGetTeamsByNameAsync(teamName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
