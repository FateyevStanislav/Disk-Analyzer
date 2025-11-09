using DiskAnalyzer.Domain;
using System.Collections.Concurrent;

namespace DiskAnalyzer.Domain
{
    public static class WeightingRecordRepository
    {
        private static readonly ConcurrentDictionary<Guid, WeightingRecord> Repository = new();

        public static int Count => Repository.Count;

        public static void AddRecord(WeightingRecord weightingRecord)
        {
            if (!Repository.TryAdd(weightingRecord.Id, weightingRecord))
            {
                throw new ArgumentException("Exception in AddRecord Method");
            }
            Console.WriteLine($"Successfully added new {weightingRecord.ToString()}");
        }

        public static WeightingRecord GetRecord(Guid Id)
        {
            if (!Repository.TryGetValue(Id, out WeightingRecord weightingRecord))
            {
                throw new ArgumentException("Exception in GetRecord Method");
            }
            Console.WriteLine($"Successfully got value of {weightingRecord.ToString()}");
            return weightingRecord;
        }

        public static void RemoveRecord(Guid Id)
        {
            if (!Repository.TryRemove(Id, out WeightingRecord weightingRecord))
            {
                throw new ArgumentException("Exception in RemoveRecord Method");
            }
            Console.WriteLine($"Successfully removed {weightingRecord.ToString()}");
        }

        public static void Clear()
        {
            Repository.Clear();
        }
    }
}
