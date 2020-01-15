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
            CreateMap<Alarm, AlarmDto>();
            CreateMap<AlarmDto, Alarm>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
        }
    }
}