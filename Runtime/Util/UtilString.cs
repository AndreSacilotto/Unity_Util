using System;
using System.Globalization;
using System.Text;

namespace Spectra.Util
{
    public static class UtilString
    {
        public static NumberFormatInfo PercentFormat { get; } = new NumberFormatInfo
        {
            PercentGroupSeparator = string.Empty,
            PercentPositivePattern = 1,
            PercentNegativePattern = 1,
        };

        public static string NumberWithSign(int value) => value.ToString("+#;-#;0");
        public static string NumberWithSign(float value) => value.ToString("+#;-#;0");

        public static string EnumToName(Enum enumValue)
        {
            var str = enumValue.ToString();
            var sb = new StringBuilder(str.Length);

            if (char.IsLetterOrDigit(str[0]))
                sb.Append(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                var c = str[i];
                if (c == '_')
                    sb.Append(' ');
                else if (char.IsUpper(c) || char.IsNumber(c))
                    sb.Append(' ' + c);
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }

        public static string NicifyVariableName(string str)
        {
            if (str == null || str.Length == 0)
                return string.Empty;

            str = str.Trim();
            var sb = new StringBuilder();

            int i = 1;

            if (char.IsLetter(str[0]))
                sb.Append(char.ToUpper(str[0]));
            else if (str[0] == '_' && str.Length > 1)
                sb.Append(char.ToUpper(str[i++]));

            for (; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    var i1 = i + 1;
                    if (i1 < str.Length && char.IsLetterOrDigit(str[i1]) && !char.IsUpper(str[i1]))
                        sb.Append(' ');
                }
                sb.Append(str[i]);
            }

            return sb.ToString();
        }

        public static string LowPrecisePercentage(float value)
        {
            return value.ToString("0.##\\%", PercentFormat);
        }

        public static string PrecisePercentage(float value, out decimal decimalValue)
        {
            decimalValue = Math.Floor((decimal)value * 100m) / 100m;
            if (Math.Floor(decimalValue) % 1 == 0)
                return decimalValue.ToString("P0", PercentFormat);
            return decimalValue.ToString("P1", PercentFormat);
        }
    }
}
