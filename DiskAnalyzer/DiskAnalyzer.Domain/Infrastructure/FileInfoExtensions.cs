using System.Security.Cryptography;

namespace DiskAnalyzer.Library.Infrastructure;

public static class FileInfoExtensions
{
    public static bool IsEqualTo(this FileInfo file1, FileInfo file2)
    {
        if (file1.Length != file2.Length)
            return false;
        var hashFile1 = file1.GetFileContentHash();
        var hashFile2 = file2.GetFileContentHash();
        for (var i = 0; i < hashFile1.Length; i++)
        {
            if (hashFile1[i] != hashFile2[i])
                return false;
        }
        return true;
    }

    public static byte[] GetFileContentHash(this FileInfo file)
    {
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(file.FullName);
        return sha256.ComputeHash(stream);
    }
}