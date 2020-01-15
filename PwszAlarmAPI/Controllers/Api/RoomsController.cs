using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PwszAlarmAPI.Dtos;
using PwszAlarmAPI.Infrastructure;
using PwszAlarmAPI.Models;

namespace PwszAlarmAPI.Controllers.Api
{
    
    public class RoomsController : ApiController
    {
        private ApplicationDbContext _context;

        public RoomsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/rooms
        [HttpGet]
        public IHttpActionResult GetRooms()
        {
            return Ok(_context.Rooms.ToList().Select(Mapper.Map<Room, RoomDto>));
        }

        // GET /api/rooms/1
        [HttpGet]
        public IHttpActionResult GetRoom(int id)
        {
            var room = _context.Rooms.SingleOrDefault(c => c.Id == id);

            if (room == null)
                return NotFound();

            return Ok(Mapper.Map<Room, RoomDto>(room));
        }
    }
}
