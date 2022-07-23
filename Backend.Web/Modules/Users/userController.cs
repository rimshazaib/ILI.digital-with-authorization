using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Data.IRepositories.IJWTManager;
using Backend.Data.Repositories.JWTRepository;
using Microsoft.AspNetCore.Mvc;
using HRM.Web.Controllers.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Backend.Application.DataTransferObjects.UserDto;
using Backend.Data.Entities;
using Backend.Application.DataTransferObjects.Register;
using Backend.Web.Modules.Users;


namespace Backend.Web.Modules.UsersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : BaseController
    {
        private readonly userservices services;
        private readonly EFDataContext _context;
        private readonly IConfiguration iconfiguration;
        public userController(EFDataContext _context, IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
            this._context = _context;
            services =new userservices(_context, iconfiguration);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult registration(RegisterDto reg)
        {
            return Ok(services.registration(reg));
            //var res = await services.registration(reg);
            //return Ok(res);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(users usersdata)
        {
            return Ok(services.Authenticate(usersdata));
        }


    }
}

