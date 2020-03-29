using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBankingSystem.Domain.Commands.DepositMoney;
using SimpleBankingSystem.Domain.Commands.WithdrawMoney;
using SimpleBankingSystem.Domain.Exceptions;
using SimpleBankingSystem.Domain.Models.Entities;
using SimpleBankingSystem.Domain.Validators;

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
        private readonly IAccountStatusValidator _statusValidator;
        private readonly IAccountBalanceValidator _balanceValidator;
        private readonly IAccountEntity _account;

        /// <summary>
        /// Account commands controller constructor
        /// </summary>
        /// <param name="statusValidator">Account status validator</param>
        /// <param name="balanceValidator">Account balance validator</param>
        /// <param name="account">Account entity (normally would be retrieved from database)</param>
        public AccountCommandsController(IAccountStatusValidator statusValidator, IAccountBalanceValidator balanceValidator, IAccountEntity account)
        {
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
            _balanceValidator = balanceValidator ?? throw new ArgumentNullException(nameof(balanceValidator));
            _account = account ?? throw new ArgumentNullException(nameof(account));
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
        public IActionResult DepositMoney(DepositMoneyCommand command)
        {
            try
            {
                var commandHandler = new DepositMoneyCommandHandler(_statusValidator, _account);
                commandHandler.Execute(command);
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
        public IActionResult WithdrawMoney(WithdrawMoneyCommand command)
        {
            try
            {
                var commandHandler = new WithdrawMoneyCommandHandler(_statusValidator, _balanceValidator, _account);
                commandHandler.Execute(command);
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