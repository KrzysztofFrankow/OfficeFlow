using System.ComponentModel;
using System.Reflection;

namespace OfficeFlow.Application.Enums
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString())!;

            if (field is null)
            {
                return value.ToString();
            }

            DescriptionAttribute? attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                  .SingleOrDefault() as DescriptionAttribute;

            return attribute?.Description ?? value.ToString();
        }
    }
}
