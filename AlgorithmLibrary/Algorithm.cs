using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlgorithmLibrary
{
    public static class Algorithm
    {
        public static bool IntersectWith(this int[] reference, int[] other) =>
            reference[0] < other[1] && reference[1] > other[0];

        //public static bool IntersectWith(
        //    this ValueTuple<int, int> reference,
        //    ValueTuple<int, int> other) =>
        //    reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        //public static bool IntersectWith(
        //    this ValueTuple<decimal, decimal> reference,
        //    ValueTuple<decimal, decimal> other) =>
        //    reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        //public static bool IntersectWith(
        //    this ValueTuple<TimeSpan, TimeSpan> reference,
        //    ValueTuple<TimeSpan, TimeSpan> other) =>
        //    reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        //public static bool IntersectWith(
        //    this ValueTuple<DateTime, DateTime> reference,
        //    ValueTuple<DateTime, DateTime> other) =>
        //    reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static (T From, T To)? GetIntersectionValuePair<T>((T From, T To) reference, (T From, T To) other)
            where T : IComparable, IComparable<T>, IEquatable<T>, IFormattable
        {
            if (!reference.IntersectWith(other, isInclusive: false)) return null;

            return (reference.From.CompareTo(other.From) == -1 ? other.From : reference.From,
                reference.To.CompareTo(other.To) == -1 ? reference.To : other.To);
        }

        public static bool
            IntersectWith<T>(this (T From, T To) reference, (T From, T To) other, bool isInclusive = true)
            where T : IComparable, IComparable<T>, IEquatable<T>, IFormattable =>
            reference.From.CompareTo(other.To) <= (isInclusive ? 0 : -1) &&
            reference.To.CompareTo(other.From) >= (isInclusive ? 0 : 1);

        public static int[] GetIntersectionValuePair(int[] reference, int[] other)
        {
            if (!reference.IntersectWith(other)) return new int[] { };

            var (start, end) = (reference[0].CompareTo(other[0]) == -1 ? other[0] : reference[0],
                reference[1].CompareTo(other[1]) == -1 ? reference[1] : other[1]);

            return new[] { start, end };
        }

        //public static TimeSpan[] GetIntersectionValuePair(
        //    ValueTuple<TimeSpan, TimeSpan> reference,
        //    ValueTuple<TimeSpan, TimeSpan> other)
        //{
        //    if (!reference.IntersectWith(other)) return new TimeSpan[] { };

        //    var (from, to) = (
        //        reference.Item1.CompareTo(other.Item1) == -1 ? other.Item1 : reference.Item1,
        //        reference.Item2.CompareTo(other.Item2) == -1 ? reference.Item2 : other.Item2
        //    );

        //    return new[] { from, to };
        //}

        public static int[][] GetIntersections(int[][][] arr)
        {
            if (arr.Length == 1) return arr[0];

            int[][] intersections = null;

            for (int first = 0, next = 1; next < arr.Length; next++)
            {
                var references = intersections ?? arr[first];
                var others = arr[next];

                intersections = GetReferencesAndOthersIntersections(references, others);

                if (intersections.Length == 0) break;
            }

            return intersections ?? new int[][] { };
        }

        private static int[][] GetReferencesAndOthersIntersections(
            IEnumerable<int[]> references, IEnumerable<int[]> others) =>
            (from reference in references
                from other in others
                where reference.IntersectWith(other)
                select GetIntersectionValuePair(reference, other)).ToArray();

        public static IEnumerable<(TimeSpan From, TimeSpan To)> GetOpenTimeFrames(
            IEnumerable<(TimeSpan From, TimeSpan To)> dailyEvents, (TimeSpan From, TimeSpan To) dailySchedule,
            TimeSpan allocatedTime)
        {
            var openTimeFrames = new List<(TimeSpan From, TimeSpan To)>();

            (TimeSpan From, TimeSpan To)? currentEvent = null;
            (TimeSpan From, TimeSpan To)? previousEvent = null;
            (TimeSpan From, TimeSpan To) openTimeFrame;

            var (scheduleFrom, scheduleTo) = dailySchedule;
            foreach (var dailyEvent in dailyEvents)
            {
                currentEvent = dailyEvent;

                var isEventFromStartedAfterScheduleFrom = currentEvent.Value.From.CompareTo(scheduleFrom) == 1;
                if (isEventFromStartedAfterScheduleFrom)
                {
                    var timeStartingPoint = previousEvent?.To ?? scheduleFrom;

                    openTimeFrame = (timeStartingPoint,
                        timeStartingPoint.Add(currentEvent.Value.From.Subtract(timeStartingPoint)));
                    AddToOpenTimeFrameList(openTimeFrames, openTimeFrame, allocatedTime);
                }

                previousEvent = currentEvent;
            }

            openTimeFrame = (currentEvent?.To ?? scheduleFrom, scheduleTo);
            AddToOpenTimeFrameList(openTimeFrames, openTimeFrame, allocatedTime);

            return openTimeFrames;
        }

        private static void AddToOpenTimeFrameList(ICollection<(TimeSpan, TimeSpan)> openTimeList,
            (TimeSpan From, TimeSpan To) openTimeItem, TimeSpan allocatedTime)
        {
            if (!openTimeItem.HasEnoughTime(allocatedTime)) return;

            openTimeList.Add(openTimeItem);
        }

        private static bool HasEnoughTime(this (TimeSpan From, TimeSpan To) openTimeFrame, TimeSpan value) =>
            openTimeFrame.From != openTimeFrame.To && openTimeFrame.To.Subtract(openTimeFrame.From) >= value;

        public static bool IsBetween<T>(this T value, T lowerBound, T upperBound, bool isInclusive = true)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable =>
            isInclusive
                ? value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) <= 0
                : value.CompareTo(lowerBound) > 0 && value.CompareTo(upperBound) < 0;

        private static Type GetPropertyType(PropertyInfo toPropertyInfo) =>
            toPropertyInfo.PropertyType.IsGenericType &&
            toPropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? Nullable.GetUnderlyingType(toPropertyInfo.PropertyType)
                : toPropertyInfo.PropertyType;

        public static int[] Merge(this int[] source, int[] other)
        {
            var all = new int[source.Length + other.Length];
            Buffer.BlockCopy(source, 0, all, 0, source.Length * sizeof(int));
            Buffer.BlockCopy(other, 0, all, source.Length * sizeof(int), other.Length * sizeof(int));

            return all;
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<T> source, IEnumerable<T> other, bool isDistinct = false)
            where T : IComparable<T>
        {
            const string argumentExceptionMsg = "{0} cannot be null";
            if (source == null) throw new ArgumentNullException(string.Format(argumentExceptionMsg, nameof(source)));
            if (other == null) throw new ArgumentNullException(string.Format(argumentExceptionMsg, nameof(other)));

            return isDistinct ? source.Union(other) : source.Concat(other);
        }
    }
}
