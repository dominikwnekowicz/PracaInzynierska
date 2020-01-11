using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PwszAlarmAPI.Dtos;
using PwszAlarmAPI.Models;

namespace PwszAlarmAPI.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Alarm, AlarmDto>();
            Mapper.CreateMap<AlarmDto, Alarm>();
            Mapper.CreateMap<Room, RoomDto>();
            Mapper.CreateMap<RoomDto, Room>();
        }
    }
}