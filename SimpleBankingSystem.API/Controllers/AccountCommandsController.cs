using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBankingSystem.Domain.Commands.DepositMoney;
using SimpleBankingSystem.Domain.Commands.WithdrawMoney;
using SimpleBankingSystem.Domain.Exceptions;

namespace SimpleBankingSystem.API.Controllers
{
    /// <summary>
    /// Commands for account management
    /// </summary>
    [Produces("application/json")]
    [Route("api/Commands/Account")]
    [ApiController]
    public class AccountCommandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Account commands controller constructor
        /// </summary>
        /// <param name="mediator">Mediator object</param>
        public AccountCommandsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Deposits specified amount of money to the account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /DepositMoney
        ///     {
        ///        "AccountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "MoneyAmount": 10.99
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Command sent by the client</param>
        /// <returns>Appropriate status code</returns>
        /// <response code="200">When the command was successfully executed</response>
        /// <response code="400">When the command or other argument was NULL</response> 
        /// <response code="404">When the command was forbidden due to validation failed</response>   
        /// <response code="500">When there occured some other error during command execution</response>   
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DepositMoney(DepositMoneyCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ForbiddenCommandException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Withdraws specified amount of money from the account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /WithdrawMoney
        ///     {
        ///        "AccountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "MoneyAmount": 10.99
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Command sent by the client</param>
        /// <returns>Appropriate status code</returns>
        /// <response code="200">When the command was successfully executed</response>
        /// <response code="400">When the command or other argument was NULL</response> 
        /// <response code="404">When the command was forbidden due to validation failed</response>   
        /// <response code="500">When there occured some other error during command execution</response> 
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> WithdrawMoney(WithdrawMoneyCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ForbiddenCommandException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}