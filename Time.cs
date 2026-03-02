using System;

namespace Taller01
{
    public class Time
    {
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;

        public int Hour => _hour;
        public int Minute => _minute;
        public int Second => _second;
        public int Millisecond => _millisecond;

        private void ValidHour(int hour)
        {
            if (hour < 0 || hour > 23)
                throw new Exception($"The hour: {hour}, is not valid.");
        }

        private void ValidMinute(int minute)
        {
            if (minute < 0 || minute > 59)
                throw new Exception($"The minute: {minute}, is not valid.");
        }

        private void ValidSecond(int second)
        {
            if (second < 0 || second > 59)
                throw new Exception($"The second: {second}, is not valid.");
        }

        private void ValidMillisecond(int millisecond)
        {
            if (millisecond < 0 || millisecond > 999)
                throw new Exception($"The millisecond: {millisecond}, is not valid.");
        }

        public Time(int hour, int minute, int second, int millisecond)
        {
            ValidHour(hour);
            ValidMinute(minute);
            ValidSecond(second);
            ValidMillisecond(millisecond);

            _hour = hour;
            _minute = minute;
            _second = second;
            _millisecond = millisecond;
        }

        public Time() : this(0, 0, 0, 0) { }
        public Time(int hour) : this(hour, 0, 0, 0) { }
        public Time(int hour, int minute) : this(hour, minute, 0, 0) { }
        public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }

        public long ToMilliseconds()
        {
            return ((Hour * 60L + Minute) * 60L + Second) * 1000L + Millisecond;
        }

        public long ToSeconds()
        {
            return ((Hour * 60L + Minute) * 60L + Second);
        }

        public long ToMinutes()
        {
            return (Hour * 60L + Minute);
        }

        public Time Add(Time other)
        {
            int ms = _millisecond + other._millisecond;
            int s = _second + other._second;
            int m = _minute + other._minute;
            int h = _hour + other._hour;

            if (ms > 999)
            {
                s += ms / 1000;
                ms %= 1000;
            }

            if (s > 59)
            {
                m += s / 60;
                s %= 60;
            }

            if (m > 59)
            {
                h += m / 60;
                m %= 60;
            }

            h %= 24;

            return new Time(h, m, s, ms);
        }

        public bool IsOtherDay(Time other)
        {
            int ms = _millisecond + other._millisecond;
            int s = _second + other._second;
            int m = _minute + other._minute;
            int h = _hour + other._hour;

            if (ms > 999) s += ms / 1000;
            if (s > 59) m += s / 60;
            if (m > 59) h += m / 60;

            return h > 23;
        }

        public override string ToString()
        {
            int hour12 = _hour % 12;
            if (hour12 == 0) hour12 = 12;

            string ampm = _hour < 12 ? "AM" : "PM";

            return $"{hour12:00}:{_minute:00}:{_second:00}.{_millisecond:000} {ampm}";
        }
    }
}
