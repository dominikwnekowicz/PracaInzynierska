using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PwszAlarmAPI.Dtos;
using PwszAlarmAPI.Infrastructure;
using PwszAlarmAPI.Models;

namespace PwszAlarmAPI.Hubs
{
    public class MessagesHub : Hub
    {
        public void Send( int alarmId, string userName, string message)
        {
            var messageDto = SaveMessage(alarmId, userName, message);
            Clients.All.sendMessageToClients(messageDto.Id, messageDto.AlarmId, messageDto.UserName, messageDto.Message, messageDto.MessageTime);
        }

        public void SendMessage(int alarmId, string userName, string message)
        {
            var messageDto = SaveMessage(alarmId, userName, message);
            Clients.All.sendMessageToClients(messageDto.Id, messageDto.AlarmId, messageDto.UserName, messageDto.Message, messageDto.MessageTime);
        }
        private MessagesDto SaveMessage(int alarmId, string userName, string message)
        {
            var messageDto = new MessagesDto
            {
                UserName = userName,
                AlarmId = alarmId,
                Message = message,
                MessageTime = DateTime.Now
            };
            var _context = new ApplicationDbContext();
            var messageObj = Mapper.Map<MessagesDto, Messages>(messageDto);
            _context.Messages.Add(messageObj);
            _context.SaveChanges();
            messageDto.Id = messageObj.Id;
            return messageDto;
        }
    }
}