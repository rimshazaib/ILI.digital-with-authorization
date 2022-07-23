using Backend.Application.DataTransferObjects.Register;
using Backend.Data.Entities;
using Backend.Data.IRepositories.IJWTManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Data.Entities.tokens;
using Microsoft.Extensions.Configuration;
using Backend.Data.Repositories.JWTRepository;
using Backend.Application.Wrappers;
using Backend.Application.Enums;

namespace Backend.Web.Modules.Users
{
    public class userservices
    {
        private readonly JWTManagerRepository manger;
        private readonly EFDataContext _context;
        private readonly IConfiguration iconfiguration;
        public userservices(EFDataContext _context, IConfiguration iconfiguration)
        {
            this._context = _context;
            this.iconfiguration = iconfiguration;
            this.manger = new JWTManagerRepository(_context, iconfiguration);
        }

        public Response<dynamic> Authenticate(users usersdata)
        {
            var res = manger.Authenticate(usersdata);

            if (res == null)
            {
                return new Response<dynamic>(true, res, GeneralMessage.InvalidEmailOrPassword);
            }
            return new Response<dynamic>(true, res, GeneralMessage.UserLoginSuccessMessage);
        }
        [HttpPost]
        public Response<dynamic> registration(RegisterDto reg)
        {
            if (reg == null)
            {
                return new Response<dynamic>(true, reg, GeneralMessage.INVALIDCREDENTIALS);
            }
            var result = manger.registration(reg);
            return new Response<dynamic>(true, result.Result, GeneralMessage.RecordAdded);
        }
    }
}
