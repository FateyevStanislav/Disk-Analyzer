namespace DiskAnalyzer.Library.Infrastructure
{
    public static class SizeFormatter
    {
        public static string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double formattedSize = bytes;

            while (formattedSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                formattedSize /= 1024;
            }

            return order == 0
                ? $"{formattedSize:0} {sizes[order]}"                    
                : $"{formattedSize:0.##} {sizes[order]}".Replace(",00", "");
        }
    }
}
