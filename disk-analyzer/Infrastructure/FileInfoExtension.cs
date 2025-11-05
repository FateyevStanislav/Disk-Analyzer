using System.Security.Cryptography;

namespace DiskAnalyzer.Infrastructure;

public static class FileInfoExtension
{
    public static bool IsEqualTo(this FileInfo file1, FileInfo file2)
    {
        return file1.GetFileContentHash() == file2.GetFileContentHash();
    }

    public static string GetFileContentHash(this FileInfo file)
    {
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(file.FullName);
        byte[] hashBytes = sha256.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
    }
}

