using System.Security.Cryptography;

namespace DiskAnalyzer.Domain.Extensions;

public static class FileInfoExtensions
{
    public static bool IsEqualTo(this FileInfo file1, FileInfo file2)
    {
        if (file1.Length != file2.Length)
            return false;

        var hash1 = file1.GetFileContentHash();
        var hash2 = file2.GetFileContentHash();

        return hash1.SequenceEqual(hash2);
    }

    public static byte[] GetFileContentHash(this FileInfo file)
    {
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(file.FullName);
        return sha256.ComputeHash(stream);
    }

    public static byte[] GetQuickHash(this FileInfo file, int bytesToRead = 8192)
    {
        using var stream = File.OpenRead(file.FullName);

        var buffer = new byte[Math.Min(bytesToRead, file.Length)];
        var bytesRead = stream.Read(buffer, 0, buffer.Length);

        return SHA256.HashData(buffer.AsSpan(0, bytesRead));
    }

    public static string GetFileContentHashString(this FileInfo file)
    {
        return Convert.ToHexString(file.GetFileContentHash());
    }

    public static string GetQuickHashString(this FileInfo file, int bytesToRead = 8192)
    {
        return Convert.ToHexString(file.GetQuickHash(bytesToRead));
    }
}