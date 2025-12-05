using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Api.Modules
{
    public class History
    {
        private List<Record> historyList;

        public History()
        {
            this.historyList = new();
        }

        public void AddRecord(Record record)
        {
            historyList.Add(record);
        }

        public IEnumerable<Record> GetAllRecords()
        {
            return historyList.AsReadOnly().Reverse();
        }

        public IEnumerable<Record> GetLastRecords(int count)
        {
            if (count >= historyList.Count)
            {
                count = historyList.Count;
            }

            return historyList.GetRange(historyList.Count - count, count).AsReadOnly().Reverse();
        }
    }
}
