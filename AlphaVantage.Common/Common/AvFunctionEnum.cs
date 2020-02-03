namespace AlphaVantage.Common
{
    public class AvFunctionEnum : Enumeration
    {
        public static AvFunctionEnum Undefined = new AvFunctionEnum(0, "__UNKNOWN__");
        public static AvFunctionEnum SMA = new AvFunctionEnum(1, "SMA");
        public static AvFunctionEnum EMA = new AvFunctionEnum(2, "EMA");
        public static AvFunctionEnum WMA = new AvFunctionEnum(3, "WMA");
        public static AvFunctionEnum DEMA = new AvFunctionEnum(4, "DEMA");
        public static AvFunctionEnum TEMA = new AvFunctionEnum(5, "TEMA");
        public static AvFunctionEnum TRIMA = new AvFunctionEnum(6, "TRIMA");
        public static AvFunctionEnum KAMA = new AvFunctionEnum(7, "KAMA");
        public static AvFunctionEnum MAMA = new AvFunctionEnum(8, "MAMA");
        public static AvFunctionEnum T3 = new AvFunctionEnum(9, "T3");
        public static AvFunctionEnum MACD = new AvFunctionEnum(10, "MACD");
        public static AvFunctionEnum RSI = new AvFunctionEnum(11, "RSI");
        public static AvFunctionEnum STOCHRSI = new AvFunctionEnum(12, "STOCHRSI");
        public static AvFunctionEnum WILLR = new AvFunctionEnum(13, "WILLR");
        public static AvFunctionEnum ADX = new AvFunctionEnum(14, "ADX");
        public static AvFunctionEnum ADXR = new AvFunctionEnum(15, "ADXR");
        public static AvFunctionEnum APO = new AvFunctionEnum(16, "APO");
        public static AvFunctionEnum PPO = new AvFunctionEnum(17, "PPO");
        public static AvFunctionEnum MOM = new AvFunctionEnum(18, "MOM");
        public static AvFunctionEnum BOP = new AvFunctionEnum(19, "BOP");
        public static AvFunctionEnum CCI = new AvFunctionEnum(20, "CCI");
        public static AvFunctionEnum CMO = new AvFunctionEnum(21, "CMO");
        public static AvFunctionEnum ROC = new AvFunctionEnum(22, "ROC");
        public static AvFunctionEnum ROCR = new AvFunctionEnum(23, "ROCR");
        public static AvFunctionEnum AROON = new AvFunctionEnum(24, "AROON");
        public static AvFunctionEnum AROONOSC = new AvFunctionEnum(25, "AROONOSC");
        public static AvFunctionEnum MFI = new AvFunctionEnum(26, "MFI");
        public static AvFunctionEnum TRIX = new AvFunctionEnum(27, "TRIX");
        public static AvFunctionEnum ULTOSC = new AvFunctionEnum(28, "ULTOSC");
        public static AvFunctionEnum DX = new AvFunctionEnum(29, "DX");
        public static AvFunctionEnum MINUS_DI = new AvFunctionEnum(30, "MINUS_DI");
        public static AvFunctionEnum PLUS_DI = new AvFunctionEnum(31, "PLUS_DI");
        public static AvFunctionEnum MINUS_DM = new AvFunctionEnum(32, "MINUS_DM");
        public static AvFunctionEnum PLUS_DM = new AvFunctionEnum(33, "PLUS_DM");
        public static AvFunctionEnum BBANDS = new AvFunctionEnum(34, "BBANDS");
        public static AvFunctionEnum MIDPOINT = new AvFunctionEnum(35, "MIDPOINT");
        public static AvFunctionEnum MIDPRICE = new AvFunctionEnum(36, "MIDPRICE");
        public static AvFunctionEnum SAR = new AvFunctionEnum(37, "SAR");
        public static AvFunctionEnum TRANGE = new AvFunctionEnum(38, "TRANGE");
        public static AvFunctionEnum ATR = new AvFunctionEnum(39, "ATR");
        public static AvFunctionEnum NATR = new AvFunctionEnum(40, "NATR");
        public static AvFunctionEnum ADOSC = new AvFunctionEnum(41, "ADOSC");
        public static AvFunctionEnum OBV = new AvFunctionEnum(42, "OBV");
        public static AvFunctionEnum HT_TRENDLINE = new AvFunctionEnum(43, "HT_TRENDLINE");
        public static AvFunctionEnum HT_SINE = new AvFunctionEnum(44, "HT_SINE");
        public static AvFunctionEnum HT_TRENDMODE = new AvFunctionEnum(45, "HT_TRENDMODE");
        public static AvFunctionEnum HT_DCPERIOD = new AvFunctionEnum(46, "HT_DCPERIOD");
        public static AvFunctionEnum HT_DCPHASE = new AvFunctionEnum(47, "HT_DCPHASE");
        public static AvFunctionEnum HT_PHASOR = new AvFunctionEnum(48, "HT_PHASOR");

        public static AvFunctionEnum STOCH = new AvFunctionEnum(49, "STOCH");
        public static AvFunctionEnum STOCHF = new AvFunctionEnum(50, "STOCHF");        
        public static AvFunctionEnum VWAP = new AvFunctionEnum(51, "VWAP");
        public static AvFunctionEnum AD = new AvFunctionEnum(52, "AD");        
        public static AvFunctionEnum MACDEXT = new AvFunctionEnum(53, "MACDEXT");


        public static AvFunctionEnum InteraDay = new AvFunctionEnum(54, "TIME_SERIES_INTRADAY");
        public static AvFunctionEnum Daily = new AvFunctionEnum(55, "TIME_SERIES_DAILY");
        public static AvFunctionEnum DailyAdjusted = new AvFunctionEnum(56, "TIME_SERIES_DAILY_ADJUSTED");
        public static AvFunctionEnum Weekly = new AvFunctionEnum(57, "TIME_SERIES_WEEKLY");
        public static AvFunctionEnum WeeklyAdjusted = new AvFunctionEnum(58, "TIME_SERIES_WEEKLY_ADJUSTED");
        public static AvFunctionEnum Monthly = new AvFunctionEnum(59, "TIME_SERIES_MONTHLY");
        public static AvFunctionEnum MonthlyAdjusted = new AvFunctionEnum(60, "TIME_SERIES_MONTHLY_ADJUSTED");
        public static AvFunctionEnum BatchStockQuotes = new AvFunctionEnum(61, "BATCH_STOCK_QUOTES");


        public AvFunctionEnum(int id, string name) : base(id, name) { }
        public AvFunctionEnum() : base(0, "__UNKNOWN__") { }
        public static AvFunctionEnum FromName(string name)
        {
            return FromDisplayName<AvFunctionEnum>(name);
        }
    }

}
