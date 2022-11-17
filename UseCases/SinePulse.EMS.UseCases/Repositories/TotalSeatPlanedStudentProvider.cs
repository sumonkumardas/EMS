using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class TotalSeatPlanedStudentProvider : ITotalSeatPlanedStudentProvider
  {
    private readonly IRepository _repository;

    public TotalSeatPlanedStudentProvider(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<int> GetTotalSeatPlanedStudentOfTest(long testId)
    {
      return await GetTotalSeatPlanedStudent(seatPlan => seatPlan.Test.Id == testId);
    }

    public async Task<int> GetTotalSeatPlanedStudentOfRoom(long roomId)
    {
      return await GetTotalSeatPlanedStudent(seatPlan => seatPlan.Room.Id == roomId);
    }

    public async Task<int> GetTotalSeatPlanedStudentOfTestExcept(long testId, long seatPlanId)
    {
      return await GetTotalSeatPlanedStudent(seatPlan => seatPlan.Test.Id == testId && seatPlan.Id != seatPlanId);
    }

    public async Task<int> GetTotalSeatPlanedStudentOfRoomExcept(long roomId, long seatPlanId)
    {
      return await GetTotalSeatPlanedStudent(seatPlan => seatPlan.Room.Id == roomId && seatPlan.Id != seatPlanId);
    }

    private async Task<int> GetTotalSeatPlanedStudent(Expression<Func<SeatPlan, bool>> condition)
    {
      return await _repository.Filter(condition).SumAsync(x => x.TotalStudent);
    }
  }
}