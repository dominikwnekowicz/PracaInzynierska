using AutoMapper;
using PwszAlarmAPI.Dtos;
using PwszAlarmAPI.Infrastructure;
using PwszAlarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PwszAlarmAPI.Controllers.Api
{
    [Authorize]
    public class MessagesController : ApiController
    {
        private ApplicationDbContext _context;
        public MessagesController()
        {
            _context = new ApplicationDbContext();
        }
        //GET: /api/messages/1
        [HttpGet]
        public IHttpActionResult GetMessages(int id)
        {
            var messages = _context.Messages.ToList().Select(Mapper.Map<Messages, MessagesDto>).Where(m => m.AlarmId == id);
            return Ok(messages);
        }
    }
}
