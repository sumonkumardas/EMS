using FluentValidation;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class AddRoomRequestMessageValidator : AbstractValidator<AddRoomRequestMessage>
  {
    private readonly IUniqueRoomPropertyChecker _uniqueRoomPropertyChecker;

    public AddRoomRequestMessageValidator(IUniqueRoomPropertyChecker uniqueRoomPropertyChecker)
    {
      _uniqueRoomPropertyChecker = uniqueRoomPropertyChecker;
      RuleFor(x => x.RoomNo).NotEmpty().WithMessage("Please specify Room No.");
      RuleFor(x => x.RoomNo).NotNull().WithMessage("Please specify Room No.");
      RuleFor(x => x.RoomNo).Must((m, x) => IsUniqueRoomNo(x, m.BranchId)).WithMessage("Room No already exists.");
    }

    private bool IsUniqueRoomNo(string designationName, long branchId)
    {
      return _uniqueRoomPropertyChecker.IsUniqueRoomNo(designationName, branchId);
    }
  }
}