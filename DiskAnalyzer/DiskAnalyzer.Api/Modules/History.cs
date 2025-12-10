using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Api.Modules
{
    public class History
    {
        private List<AnalysisResult> historyList;

        public History()
        {
            this.historyList = new();
        }

        public void AddRecord(AnalysisResult record)
        {
            historyList.Add(record);
        }

        public IEnumerable<AnalysisResult> GetAllRecords()
        {
            return historyList.AsReadOnly().Reverse();
        }

        public IEnumerable<AnalysisResult> GetLastRecords(int count)
        {
            if (count >= historyList.Count)
            {
                count = historyList.Count;
            }

            return historyList.GetRange(historyList.Count - count, count).AsReadOnly().Reverse();
        }
    }
}
