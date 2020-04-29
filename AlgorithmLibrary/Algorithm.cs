﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLibrary
{
    public static class Algorithm
    {
        public static int[] GetPivotIndexes(int[] arr)
        {
            if (arr.Length == 1) return new int[1];
            if (arr.Length < 3) return new[] { -1 };

            var pivotIndexes = new List<int>();
            var leftSum = 0;
            var rightSum = arr.Skip(1).Aggregate((acc, n) => acc + n);

            for (int i = 0, j = 1; j < arr.Length; i++, j++)
            {
                rightSum -= arr[j];
                leftSum += arr[i];

                if (leftSum == rightSum) pivotIndexes.Add(j);
            }

            return pivotIndexes.Count == 0 ? new[] { -1 } : pivotIndexes.ToArray();
        }

        public static bool IntersectWith(this int[] reference, int[] other) =>
            reference[0] < other[1] && reference[1] > other[0];

        public static bool IntersectWith(
            this ValueTuple<int, int> reference,
            ValueTuple<int, int> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static bool IntersectWith(
            this ValueTuple<decimal, decimal> reference,
            ValueTuple<decimal, decimal> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static bool IntersectWith(
            this ValueTuple<TimeSpan, TimeSpan> reference,
            ValueTuple<TimeSpan, TimeSpan> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static bool IntersectWith(
            this ValueTuple<DateTime, DateTime> reference,
            ValueTuple<DateTime, DateTime> other) =>
            reference.Item1 < other.Item2 && reference.Item2 > other.Item1;

        public static (T From, T To)? GetIntersectionValuePair<T>(
            (T From, T To) reference, (T From, T To) other)
            where T : IComparable, IComparable<T>, IEquatable<T>, IFormattable
        {
            if (!reference.IntersectWith(other, isInclusive: false)) return null;

            return (
                reference.From.CompareTo(other.From) == -1 ? other.From : reference.From,
                reference.To.CompareTo(other.To) == -1 ? reference.To : other.To
            );
        }

        public static bool IntersectWith<T>(this (T From, T To) reference, (T From, T To) other, bool isInclusive = true)
            where T : IComparable, IComparable<T>, IEquatable<T>, IFormattable =>
            reference.From.CompareTo(other.To) <= (isInclusive ? 0 : -1) &&
            reference.To.CompareTo(other.From) >= (isInclusive ? 0 : 1);


        public static int[] GetIntersectionValuePair(int[] reference, int[] other)
        {
            if (!reference.IntersectWith(other)) return new int[] { };

            var (start, end) = (
                reference[0].CompareTo(other[0]) == -1 ? other[0] : reference[0],
                reference[1].CompareTo(other[1]) == -1 ? reference[1] : other[1]
            );

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
             select GetIntersectionValuePair(reference, other)
            ).ToArray();

        public static IEnumerable<(TimeSpan From, TimeSpan To)> GetOpenTimeFrames(
            IEnumerable<(TimeSpan From, TimeSpan To)> dailyEvents,
            (TimeSpan From, TimeSpan To) dailySchedule,
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

                var isEventFromStartedAfterScheduleFrom =
                    currentEvent.Value.From.CompareTo(scheduleFrom) == 1;
                if (isEventFromStartedAfterScheduleFrom)
                {
                    var timeStartingPoint = previousEvent?.To ?? scheduleFrom;

                    openTimeFrame = (
                        timeStartingPoint,
                        timeStartingPoint.Add(currentEvent.Value.From.Subtract(timeStartingPoint))
                    );
                    AddToOpenTimeFrameList(openTimeFrames, openTimeFrame, allocatedTime);
                }

                previousEvent = currentEvent;
            }

            openTimeFrame = (currentEvent?.To ?? scheduleFrom, scheduleTo);
            AddToOpenTimeFrameList(openTimeFrames, openTimeFrame, allocatedTime);

            return openTimeFrames;
        }

        private static void AddToOpenTimeFrameList(ICollection<(TimeSpan, TimeSpan)> openTimeList, (TimeSpan From, TimeSpan To) openTimeItem, TimeSpan allocatedTime)
        {
            if (!openTimeItem.HasEnoughTime(allocatedTime)) return;

            openTimeList.Add(openTimeItem);
        }

        private static bool HasEnoughTime(this (TimeSpan From, TimeSpan To) openTimeFrame, TimeSpan value) =>
            openTimeFrame.From != openTimeFrame.To && openTimeFrame.To.Subtract(openTimeFrame.From) >= value;

        public static bool IsBetween<T>(this T value, T lowerBound, T upperBound, bool isInclusive = true)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable => 
            isInclusive ?
            value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) <= 0 :
            value.CompareTo(lowerBound) > 0 && value.CompareTo(upperBound) < 0;
        
        private static Type GetPropertyType(PropertyInfo toPropertyInfo) =>
            toPropertyInfo.PropertyType.IsGenericType &&
            toPropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? Nullable.GetUnderlyingType(toPropertyInfo.PropertyType)
                : toPropertyInfo.PropertyType;

    }
}