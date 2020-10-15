using System;
using System.Text;

namespace CronMaker
{
    public class CronField
    {
        private const char DefaultValue = '*';
        private int AllowValueMin { get; set; }
        private int AllowValueMax { get; set; }

        private string Value { get; set; }
        public override string ToString() => Value;

        public CronField(int allowValueMin, int allowValueMax)
        {
            AllowValueMin = allowValueMin;
            AllowValueMax = allowValueMax;
            Value = DefaultValue.ToString();
        }

        #region Validates
        private void ValidateAllowValues(int value, string paramName)
        {
            if (value > AllowValueMax || value < AllowValueMin)
                 throw new ArgumentException($"The allowed values for the [{paramName}] property are the following range [{AllowValueMin} - {AllowValueMax}]", paramName);
        }
        private void ValidateAllowValues(int[] values, string paramName)
        {
            foreach (int value in values)
            {
                ValidateAllowValues(value, paramName);
            }
        }
        #endregion

        #region Methods
        public void Every()
        {
            Value = DefaultValue.ToString();
        }
        public void Every(int value, int startIn)
        {
            ValidateAllowValues(value, nameof(value));
            ValidateAllowValues(startIn, nameof(startIn));
            Value = $"{startIn}/{value}";
        }
        public void EverySpesific(params int[] values)
        {
            ValidateAllowValues(values, nameof(values));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(',');
                }

                sb.Append(values[i]);
            }
            Value = sb.ToString();
        }
        public void EveryBetween(int value1, int value2)
        {
            Value = $"{value1}-{value2}";
        }
        #endregion
    }
    public class CronStructure
    {
        public CronField Minutes { get; private set; }
        public CronField Hours { get; private set; }
        public CronField DayOfMonth { get; private set; }
        public CronField Month { get; private set; }
        public CronField DayOfWeek { get; private set; }

        public CronStructure()
        {
            Minutes = new CronField(0, 59);
            Hours = new CronField(0, 23);
            DayOfMonth = new CronField(1, 31);
            Month = new CronField(1, 12);
            DayOfWeek = new CronField(0, 6);
        }

        public override string ToString()
        {
            return $"{Minutes} {Hours} {DayOfMonth} {Month} {DayOfWeek}";
        }
    }
}
