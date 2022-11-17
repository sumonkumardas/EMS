using System;
using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.RegisterMessages
{
  public class RegisterRequestMessage : IRequest<ValidatedData<RegisterResponseMessage>>
  {
    public string FullName { get; set; }
    public DateTime DOB { get; set; }
    public DateTime JoiningDate { get; set; }
    
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}