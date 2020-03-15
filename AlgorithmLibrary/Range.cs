using System;
using System.Globalization;

namespace AlgorithmLibrary
{
    public class Range<T> : IRange<T>
        where T : IComparable, IComparable<T>, IEquatable<T>, IFormattable
    {
        public T From { get; }

        public T To { get; }

        public Range(T from, T to) => (From, To) = (from, to);

        /// <summary>
        /// Initialize DateTime or TimeSpan type from time string
        /// e.g. new Range &lt;TimeSpan&gt;("08:00", "17:00")
        /// </summary>
        /// <param name="from">string in "HH:mm" format</param>
        /// <param name="to">string in "HH:mm" format</param>
        public Range(string from, string to)
        {
            var culture = CultureInfo.CurrentCulture;
            if (typeof(T) == typeof(TimeSpan))
            {
                const string format = @"hh\:mm";
                (From, To) = (
                    (T)(object)TimeSpan.ParseExact(from, format, culture),
                    (T)(object)TimeSpan.ParseExact(to, format, culture)
                );
            }
            else if (typeof(T) == typeof(DateTime))
            {
                const string format = "HH:mm";
                (From, To) = (
                    (T)(object)DateTime.ParseExact(from, format, null),
                    (T)(object)DateTime.ParseExact(to, format, null)
                );
            }
            else
            {
                throw new InvalidCastException(
                    message: $"Object must be of type {typeof(Range<DateTime>).Name} or {typeof(Range<TimeSpan>).Name}.");
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is null) return 1;

            if (!(obj is Range<T>))
            {
                throw new ArgumentException(
                    paramName: nameof(obj),
                    message: $"Object must be of type {typeof(Range<T>).Name}.");
            }

            if (Equals(obj)) return 0;

            return From.CompareTo(((Range<T>)obj).From) < 0 ? -1 : 1;
        }

        public int CompareTo(IRange<T> other)
        {
            if (other is null) return 1;
            if (Equals(other)) return 0;

            return From.CompareTo(other.From) < 0 ? -1 : 1;
        }

        public bool Equals(T other) =>
            other is Range<T> otherRange &&
            (From, To).Equals((otherRange.From, otherRange.To));

        public override int GetHashCode() => new { From, To }.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Range<T> otherRange &&
            From.Equals(otherRange.From) && To.Equals(otherRange.To);

        public static bool operator ==(Range<T> leftOperand, Range<T> rightOperand) =>
            leftOperand is { } leftRange && rightOperand is { } rightRange ?
            leftRange.Equals(rightRange) :
            Equals(leftOperand, rightOperand);

        public static bool operator !=(Range<T> leftOperand, Range<T> rightOperand) =>
            leftOperand is { } leftRange && rightOperand is { } rightRange ?
                !leftRange.Equals(rightRange) :
                !Equals(leftOperand, rightOperand);

        public static bool operator >=(Range<T> leftOperand, Range<T> rightOperand) =>
            leftOperand is { } leftRange && rightOperand is { } rightRange ?
                leftRange.CompareTo(rightRange) >= 0 :
                Equals(leftOperand, rightOperand);

        public static bool operator <=(Range<T> leftOperand, Range<T> rightOperand) =>
            leftOperand is { } leftRange && rightOperand is { } rightRange ?
                leftRange.CompareTo(rightRange) <= 0 :
                Equals(leftOperand, rightOperand);

        public static bool operator >(Range<T> leftOperand, Range<T> rightOperand) =>
            leftOperand is { } leftRange && rightOperand is { } rightRange ?
                leftRange.CompareTo(rightRange) > 0 :
                Equals(leftOperand, rightOperand);

        public static bool operator <(Range<T> leftOperand, Range<T> rightOperand) =>
            leftOperand is { } leftRange && rightOperand is { } rightRange ?
                leftRange.CompareTo(rightRange) < 0 :
                Equals(leftOperand, rightOperand);

        public override string ToString()
        {
            return $"{From},{To}";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "G";
            if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;

            string timeFormat;

            switch (format.ToUpperInvariant())
            {
                case "TG":
                case "TM":
                    if (typeof(T) == typeof(TimeSpan))
                    {
                        timeFormat = @"hh\:mm";
                    }
                    else if (typeof(T) == typeof(DateTime))
                    {
                        timeFormat = "HH:mm";
                    }
                    else
                    {
                        throw new FormatException($"The {format} format string is not supported.");
                    }

                    return $"{From.ToString(timeFormat, formatProvider)}-{To.ToString(timeFormat, formatProvider)}";

                case "TS":
                    timeFormat = "HH:mm tt";

                    if (typeof(T) == typeof(TimeSpan))
                    {
                        var dateTimeFrom = DateTime.Today + (TimeSpan)(object)From;
                        var dateTimeTo = DateTime.Today + (TimeSpan)(object)To;

                        return $"{dateTimeFrom.ToString(timeFormat, formatProvider)}-{dateTimeTo.ToString(timeFormat, formatProvider)}";
                    }
                    else if (typeof(T) == typeof(DateTime))
                    {
                        return $"{From.ToString(timeFormat, formatProvider)}-{To.ToString(timeFormat, formatProvider)}";
                    }
                    else
                    {
                        throw new FormatException($"The {format} format string is not supported.");
                    }

                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }

        public bool IsIntersectRange(IRange<T> other) =>
            From.CompareTo(other.To) == -1 && To.CompareTo(other.From) == 1;

        public IRange<T> IntersectRange(IRange<T> other)
        {
            if (!IsIntersectRange(other)) return null;

            var (from, to) =
            (
                From.CompareTo(other.From) == -1 ? other.From : From,
                To.CompareTo(other.To) == -1 ? To : other.To
            );

            return new Range<T>(from, to);
        }

        public IRange<T> DisjointRange(IRange<T> other)
        {
            if (IsIntersectRange(other)) return null;

            var (from, to) =
            (
                From.CompareTo(other.From) == -1 ? To : other.To,
                To.CompareTo(other.To) == -1 ? other.From : From
            );

            return from.CompareTo(to) == 0 ? null : new Range<T>(@from, to);
        }
    }
}