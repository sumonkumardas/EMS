﻿using System.Threading.Tasks;
using MassTransit;

namespace SinePulse.EMS.AlertProcessingService.Externals
{
  public class AlertProcessingServiceInstance : AlertProcessingService.AlertProcessingServiceInstance
  {
    private IBusControl _bus;

    public override async Task Start()
    {
      var objectGraph = new ObjectGraph();
      await objectGraph.Initialize();

      _bus = objectGraph.Bus;

      await _bus.StartAsync();
    }

    public override async Task Stop()
    {
      await _bus.StopAsync();
    }
  }
}