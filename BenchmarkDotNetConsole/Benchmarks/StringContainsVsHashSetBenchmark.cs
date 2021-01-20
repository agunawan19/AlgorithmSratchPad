using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetConsole.Benchmarks
{
    public class StringContainsVsHashSetBenchmark : BenchmarkBase
    {
        private const int NumberOfLoop = 500_000;

        private const string InvalidFileNamesString =
            ",CON,PRN,AUX,NUL,COM1,COM2,COM3,COM4,COM5,COM6,COM7,COM8,COM9,LPT1,LPT2,LPT3,LPT4,LPT5,LPT6,LPT7,LPT8,LPT9,";

        private HashSet<string> _invalidFileNames;
        private readonly List<string> _randomFileNames = new List<string>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            _invalidFileNames = new HashSet<string>
            {
                "CON",
                "PRN",
                "AUX",
                "NUL",
                "COM1",
                "COM2",
                "COM3",
                "COM4",
                "COM5",
                "COM6",
                "COM7",
                "COM8",
                "COM9",
                "LPT1",
                "LPT2",
                "LPT3",
                "LPT4",
                "LPT5",
                "LPT6",
                "LPT7",
                "LPT8",
                "LPT9"
            };

            for (var i = 0; i < NumberOfLoop; i++) _randomFileNames.Add(Path.GetRandomFileName());
        }

        private static bool IsSafeFileNameStringContains1(string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).ToUpper();
            const string invalidFileNames =
                ",CON,PRN,AUX,NUL,COM1,COM2,COM3,COM4,COM5,COM6,COM7,COM8,COM9,LPT1,LPT2,LPT3,LPT4,LPT5,LPT6,LPT7,LPT8,LPT9,";
            return !invalidFileNames.Contains($",{fileNameWithoutExtension},");
        }

        private static bool IsSafeFileNameStringContains2(string fileName, string invalidFileNames)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).ToUpper();
            return !invalidFileNames.Contains($",{fileNameWithoutExtension},");
        }

        private static bool IsSafeFileNameHashSet1(string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).ToUpper();
            var invalidFilenames = new List<string> {"CON", "PRN", "AUX", "NUL"};
            invalidFilenames.AddRange(Enumerable.Range(1, 9).Select(n => $"COM{n}"));
            invalidFilenames.AddRange(Enumerable.Range(1, 9).Select(n => $"LPT{n}"));
            var invalidFilenameHashSet = new HashSet<string>(invalidFilenames);
            return !invalidFilenameHashSet.Contains(fileNameWithoutExtension);
        }

        private static bool IsSafeFileNameHashSet2(string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).ToUpper();
            var invalidFileNameHashSet = new HashSet<string>
            {
                "CON",
                "PRN",
                "AUX",
                "NUL",
                "COM1",
                "COM2",
                "COM3",
                "COM4",
                "COM5",
                "COM6",
                "COM7",
                "COM8",
                "COM9",
                "LPT1",
                "LPT2",
                "LPT3",
                "LPT4",
                "LPT5",
                "LPT6",
                "LPT7",
                "LPT8",
                "LPT9"
            };
            return !invalidFileNameHashSet.Contains(fileNameWithoutExtension);
        }

        private static bool IsSafeFileNameHashSet3(string fileName, HashSet<string> invalidFileNameHashSet)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).ToUpper();
            return !invalidFileNameHashSet.Contains(fileNameWithoutExtension);
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        [ParamsSource(nameof(FileNameData))] public string FileName { get; set; }

        public static IEnumerable<string> FileNameData => new[] {"COM1.XLSX", "REPORT.XLSX"};

        // [Benchmark]
        // public bool IsSafeFileNameStringContains1Benchmark() => IsSafeFileNameStringContains1(FileName);
        //
        // [Benchmark]
        // public bool IsSafeFileNameStringContains2Benchmark() =>
        //     IsSafeFileNameStringContains2(FileName, InvalidFileNamesString);
        //
        // [Benchmark]
        // public bool IsSafeFileNameHashSet1Benchmark() => IsSafeFileNameHashSet1(FileName);
        //
        // [Benchmark]
        // public bool IsSafeFileNameHashSet2Benchmark() => IsSafeFileNameHashSet2(FileName);
        //
        // [Benchmark]
        // public bool IsSafeFileNameHashSet3Benchmark() => IsSafeFileNameHashSet3(FileName, _invalidFileNames);
        
        [Benchmark]
        public void IsSafeFileNameStringContains1NLoops()
        {
            foreach (var randomFileName in _randomFileNames)
                _ = IsSafeFileNameStringContains1(randomFileName);
        }

        [Benchmark]
        public void IsSafeFileNameStringContains2NLoops()
        {
            foreach (var randomFileName in _randomFileNames)
                _ = IsSafeFileNameStringContains2(randomFileName, InvalidFileNamesString);
        }

        // [Benchmark]
        // public void IsSafeFileNameHashSet1NLoops()
        // {
        //     foreach (var randomFileName in _randomFileNames) _ = IsSafeFileNameHashSet1(randomFileName);
        // }
        
        // [Benchmark]
        // public void IsSafeFileNameHashSet2NLoops()
        // {
        //     foreach (var randomFileName in _randomFileNames) _ = IsSafeFileNameHashSet2(randomFileName);
        // }

        [Benchmark]
        public void IsSafeFileNameHashSet3NLoops()
        {
            foreach (var randomFileName in _randomFileNames)
                _ = IsSafeFileNameHashSet3(randomFileName, _invalidFileNames);
        }
    }
}
