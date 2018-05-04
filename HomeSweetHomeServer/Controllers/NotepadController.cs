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
    [Route("api/Notepad")]
    [Authorize]
    public class NotepadController : Controller
    {
        IJwtTokenService _jwtTokenService;
        INotepadService _notepadService;

        public NotepadController(IJwtTokenService jwtTokenService, INotepadService notepadService)
        {
            _jwtTokenService = jwtTokenService;
            _notepadService = notepadService;
        }

        //Synchronizes clients notepad
        [HttpPost("Synchronize", Name = "SynchronizeNotepad")]
        public async Task<IActionResult> Synchronize([FromBody] ClientNotepadContextModel client)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStr(token);

            var res = await _notepadService.SynchronizeNotepad(user, client);
            
            return Ok(res);
        }

        //Adds note
        [HttpPost("AddNote", Name = "AddNote")]
        public async Task<IActionResult> AddNote([FromBody] NotepadModel note)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStr(token);
            
            await _notepadService.AddNote(user, note);

            return Ok();
        }

        //Deletes note
        [HttpGet("DeleteNote", Name = "DeleteNote")]
        public async Task<IActionResult> DeleteNote([FromQuery] int noteId)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStr(token);

            await _notepadService.DeleteNote(user, noteId);

            return Ok();
        }

        //Updates note
        [HttpPost("UpdateNote", Name = "UpdateNote")]
        public async Task<IActionResult> UpdateNote([FromBody] NotepadModel note)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStr(token);

            await _notepadService.UpdateNote(user, note);

            return Ok();
        }
    }
}
