﻿using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class AddSessionRequestMessage : IRequest<AddSessionResponseMessage>
  {
    public string SessionName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsSessionClosed { get; set; }
    public ObjectTypeEnum SessionType { get; set; }
    public long ObjectId { get; set; }
    public string CurrentUserName { get; set; }
  }
}