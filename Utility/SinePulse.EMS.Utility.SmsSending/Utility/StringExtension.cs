﻿using System;

namespace SinePulse.EMS.Utility.SmsSending.Utility
{
  public static class StringExtensions
  {
    public static string Left(this string str, int length)
    {
      return str.Substring(0, Math.Min(length, str.Length));
    }

    public static string Right(this string str, int length)
    {
      return str.Substring(Math.Max(str.Length - length, 0), Math.Min(length, str.Length));
    }
  }
}