using AutoMapper;
using PwszAlarmAPI.Dtos;
using PwszAlarmAPI.Infrastructure;
using PwszAlarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace PwszAlarmAPI.Controllers.Api
{
    [Authorize]
    public class AlarmsController : ApiController
    {
        private ApplicationDbContext _context;

        public AlarmsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/alarms
        [HttpGet]
        public IHttpActionResult GetAlarms()
        {
            var alarms = _context.Alarms.ToList().Select(Mapper.Map<Alarm, AlarmDto>);
            foreach(var alarmDto in alarms )
            {
                if(DateTime.Now.Subtract(alarmDto.NotifyDate).TotalDays >= 7)
                {
                    _context.Alarms.FirstOrDefault(a => a.Id == alarmDto.Id).Archived = true;
                }
            }
            _context.SaveChanges();

            return Ok(_context.Alarms.ToList().Select(Mapper.Map<Alarm, AlarmDto>));
        }

        // GET /api/alarms/1
        [HttpGet]
        public IHttpActionResult GetAlarm(int id)
        {
            var alarm = _context.Alarms.SingleOrDefault(c => c.Id == id);

            if (alarm == null)
                return NotFound();

            return Ok(Mapper.Map<Alarm, AlarmDto>(alarm));
        }
        
        // POST /api/alarms
        [HttpPost]
        public async Task<IHttpActionResult> CreateAlarm(AlarmDto alarmDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var alarm = Mapper.Map<AlarmDto, Alarm>(alarmDto);
            _context.Alarms.Add(alarm);
            _context.SaveChanges();
            var rooms = _context.Rooms.ToList().Select(Mapper.Map<Room, Room>);
            var room = rooms.FirstOrDefault(r => r.Id == alarmDto.RoomId);
            alarmDto.Id = alarm.Id;
            string title = alarmDto.Name + " - Sala: " + room.Name + " - " + alarmDto.NotifyDate.ToString();
            await ApplicationFirebase.SendData(title, title);
            return Ok();
        }

        // PUT /api/alarms/1
        [HttpPut]
        public void UpdateAlarm(int id, AlarmDto alarmDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var alarmInDb = _context.Alarms.SingleOrDefault(c => c.Id == id);

            if (alarmInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(alarmDto, alarmInDb);

            _context.SaveChanges();
        }
    }
}
