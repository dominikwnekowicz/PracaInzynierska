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
                if(DateTime.Now.Subtract(alarmDto.NotifyDate).TotalDays >= 30)
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
        public IHttpActionResult CreateAlarm(AlarmDto alarmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var alarm = Mapper.Map<AlarmDto, Alarm>(alarmDto);
            _context.Alarms.Add(alarm);
            _context.SaveChanges();

            alarmDto.Id = alarm.Id;

            return Created(new Uri(Request.RequestUri + "/" + alarm.Id), alarmDto );
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

        // DELETE /api/alarms/1
        [HttpDelete]
        public void DeleteAlarm(int id)
        {
            var alarmInDb = _context.Alarms.SingleOrDefault(c => c.Id == id);

            if (alarmInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Alarms.Remove(alarmInDb);
            _context.SaveChanges();
        }
    }
}
