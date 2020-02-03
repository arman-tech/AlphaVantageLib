using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common
{
    public class AvOutputSizeEnum : Enumeration
    {
        public static AvOutputSizeEnum Undefined = new AvOutputSizeEnum(0, "__UNKNOWN__");
        public static AvOutputSizeEnum Compact = new AvOutputSizeEnum(1, "Compact");
        public static AvOutputSizeEnum Full = new AvOutputSizeEnum(2, "Full size");

        public AvOutputSizeEnum(int id, string name) : base(id, name) { }
        public AvOutputSizeEnum() : base(0, "__UNKNOWN__") { }

        public static AvOutputSizeEnum FromName(string name)
        {
            return FromDisplayName<AvOutputSizeEnum>(name);
        }

    }
}
