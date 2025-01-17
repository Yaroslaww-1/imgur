﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaLakeUsers.Services.Users;
using System;
using MediaLakeUsers.Controllers.Users;

namespace MediaLakeUsers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<Guid> Create([FromBody] CreateUserRequest request)
        {
            return await _userService.CreateUser(request.Email, request.Name, request.Password, request.Roles);
        }
    }
}
