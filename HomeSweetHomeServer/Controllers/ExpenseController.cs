using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Services;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using HomeSweetHomeServer.Exceptions;
using Newtonsoft.Json;
using System.Web;
using System.Net;

namespace HomeSweetHomeServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Expense")]
    [Authorize]
    public class ExpenseController : Controller
    {
        IJwtTokenService _jwtTokenService;
        IUserExpenseService _userExpenseService;

        public ExpenseController(IJwtTokenService jwtTokenService, IUserExpenseService userExpenseService)
        {
            _jwtTokenService = jwtTokenService;
            _userExpenseService = userExpenseService;
        }

        [HttpPost("AddExpense", Name = "AddExpense")]
        public async Task<IActionResult> AddExpense([FromBody] ClientExpenseModel clientExpense)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStrAsync(token);

            ExpenseModel expense = clientExpense.Expense;
            List<int> participants = clientExpense.Participants;

            await _userExpenseService.AddExpenseAsync(user, expense, participants);

            return Ok();
        }
    }
}
