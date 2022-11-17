namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public class RabbitMqConfig
  {
    public string Username { get; set; }
    public string Password { get; set; }
    public string ServerHost { get; set; }
    public string RabbitMqVirtualHost { get; set; }
  }
}