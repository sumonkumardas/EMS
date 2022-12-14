using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SinePulse.EMS.Utility
{
  public static class EnumExtensions
  {
    public static string GetEnumDisplayName(this Enum enumValue)
    {
      return enumValue.GetType()?
                    .GetMember(enumValue.ToString())?
                    .First()?
                    .GetCustomAttribute<DisplayAttribute>()?
                    .GetName();
    }
  }
}
