using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


public static class ExtensionMethods
{
    /*
    public static void DefaultFont(this UIFont font, float Size)
    {
        font = UIFont.FromName("DIN Alternate", Size);
    }
    */

    public static bool TryToEmail(this string value)
    {
        bool result;
        try
        {
            System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress(value);
            result = true;
        }
        catch
        {
            result = false;
        }

        return result;
    }

    //有回傳資料的try
    public static string TryToString(this object obj, string DefaultValue)
    {
        if (obj == null)
        {
            return DefaultValue;
        }

        try
        {
            return Convert.ToString(obj);
        }
        catch
        {
            return DefaultValue;
        }
    }

    public static int TryToInt(this object obj, int DefaultValue)
    {
        if (obj == null)
        {
            return DefaultValue;
        }

        try
        {
            return Convert.ToInt32(obj);
        }
        catch (Exception ce)
        {
            return DefaultValue;
        }
    }

    public static float TryToFloat(this object obj, float DefaultValue)
    {
        if (obj == null)
        {
            return DefaultValue;
        }

        try
        {
            return (float)Convert.ToDouble(obj);
        }
        catch
        {
            return DefaultValue;
        }
    }

    public static double TryToDouble(this object obj, double DefaultValue)
    {
        if (obj == null)
        {
            return DefaultValue;
        }

        try
        {
            return Convert.ToDouble(obj);
        }
        catch
        {
            return DefaultValue;
        }
    }

    public static DateTime TryToDateTime(this object obj, DateTime DefaultValue)
    {
        if (obj == null)
        {
            return DefaultValue;
        }

        try
        {
            return Convert.ToDateTime(obj);
        }
        catch
        {
            return DefaultValue;
        }
    }

    public static bool TryToBool(this object obj, bool DefaultValue)
    {
        if (obj == null)
        {
            return DefaultValue;
        }

        try
        {
            return Convert.ToBoolean(obj);
        }
        catch
        {
            return DefaultValue;
        }
    }
    public static Guid TryToGuid(this object obj, Guid DefaultValue)
    {
        if (obj == null)
        {
            return DefaultValue;
        }
        try
        {
            return Guid.Parse(obj.ToString());
        }
        catch
        {
            return DefaultValue;
        }
    }
    public static T ParseToType<T>(this object Target, T DefaultValue)
    {
        if (Target == null)
        {
            return DefaultValue;

        }
        try
        {
            return (T)Convert.ChangeType(Target, typeof(T));
        }
        catch
        {
            return DefaultValue;
        }

    }
}
