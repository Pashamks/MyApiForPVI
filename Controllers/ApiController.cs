using lab5.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabController: ControllerBase
    {
        List<GamesHistory> _gamesHistory = new List<GamesHistory> {
                new GamesHistory { WinnerEmail="winner1@gmail.com", LoserEmail = "loser1@gmail.com",GameStartTime = new DateTime(2022, 4, 1) ,GameFinishedTime = DateTime.Now  },
                new GamesHistory { WinnerEmail="winner2@gmail.com", LoserEmail = "loser1@gmail.com",GameStartTime = new DateTime(2022, 4, 2) ,GameFinishedTime = DateTime.Now  }};
        static List<LoginModel> _users = new List<LoginModel>();
       
        /// <summary>
        /// Login user
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        /// <response code="200">Returns success response</response>
        // [ProducesResponseType(typeof(CommonResponse<UserClientSideData>), 200)]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var email_checker = new EmailAddressAttribute();
            
            if (loginModel.Password.Length >= 8 && email_checker.IsValid(loginModel.Email) && !string.IsNullOrEmpty(loginModel.Name))
            {
                _users.Add(loginModel);
                return Ok(new ResponceMessage { Message = "You successfully login!", Status = true });
            }

            return BadRequest(new ResponceMessage { 
                Message = "There are problems with your data: password must be longer than 8 symbols, email must be real and name can't be empty!",
                Status = false});
        }
        /// <summary>
        /// Get battles history
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        /// <response code="200">Returns success response</response>
        [ProducesResponseType(typeof(List<GamesHistory>), 200)]
        [Route("history")]
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            if(_gamesHistory.Count > 1)
                return Ok(_gamesHistory);
            return NoContent();
        }
        /// <summary>
        /// Update user info
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        /// <response code="200">Returns success response</response>
        [ProducesResponseType(typeof(ResponceMessage), 200)]
        [Route("update_user_info")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserInfo(LoginModel loginModel)
        {
            foreach (var item in _users)
            {
                if(item.Email == loginModel.Email)
                {
                    _users.Remove(item);
                    _users.Add(loginModel);
                    return Ok(new ResponceMessage { Message = "Your info was successfully updated!", Status = true});
                }
            }

            return NotFound();
        }
        /// <summary>
        /// Play game
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        /// <response code="200">Returns success response</response>
         [ProducesResponseType(typeof(GameOponent), 200)]
        [Route("play")]
        [HttpPost]
        public async Task<IActionResult> Play()
        {
            if(_users.Count > 2)
            {
                Random random = new Random();
                int i = random.Next(1, _users.Count);
                return Ok(new GameOponent { Name = _users[i].Name, Gmail = _users[i].Email});
            }
            return NoContent();
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        /// <response code="200">Returns success response</response>
        // [ProducesResponseType(typeof(CommonResponse<UserClientSideData>), 200)]
        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(UserToDelete userToDelete)
        {
            foreach (var item in _users)
            {
                if(item.Email == userToDelete.Email &&  item.Password == userToDelete.Password)
                {
                    _users.Remove(item);
                    return Ok(new ResponceMessage { Message = "User successfully deleted", Status = true });
                }
            }
            return NotFound();
        }
    }
}
