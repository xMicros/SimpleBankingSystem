using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBankingSystem.Domain.Queries.GetBalanceAndStatus;

namespace SimpleBankingSystem.API.Controllers
{
    /// <summary>
    /// Queries for account data
    /// </summary>
    [Produces("application/json")]
    [Route("api/Queries/Account")]
    [ApiController]
    public class AccountQueriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Account queries controller constructor
        /// </summary>
        /// <param name="mediator">Mediator object</param>
        public AccountQueriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets the balance and the status of the account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetBalanceAndStatus
        ///
        /// </remarks>
        /// <returns>JSON data with status code</returns>
        /// <response code="200">When the data was successfully received</response>
        /// <response code="400">When the query or other argument was NULL</response>  
        /// <response code="500">When there occured some other error during query receiving</response>   
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GetBalanceAndStatusQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBalanceAndStatus()
        {
            try
            {
                return Ok(await _mediator.Send(new GetBalanceAndStatusQuery()));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}