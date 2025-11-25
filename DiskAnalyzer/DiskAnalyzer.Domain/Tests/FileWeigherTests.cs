using DiskAnalyzer.Library.Domain;
using NUnit.Framework;

namespace DiskAnalyzer.Library.Tests;

[TestFixture]
public class FileWeigherTests
{
    private string TempDir
    {
        get
        {
            var path = Path.Combine(Path.GetTempPath(), "fw_" + Guid.NewGuid().ToString("N"));
            return Directory.CreateDirectory(path).FullName;
        }
    }

    private static void Touch(string path, DateTime? createdUtc = null)
    {
        File.WriteAllText(path, "x");
        if (createdUtc.HasValue)
        {
            File.SetCreationTimeUtc(path, createdUtc.Value);
            File.SetLastWriteTimeUtc(path, createdUtc.Value);
        }
    }

    private void DeleteDirSafe(string path)
    {
        try { if (Directory.Exists(path)) Directory.Delete(path, true); } catch { /* best-effort */ }
    }

    [Test]
    public void CountFiles_DirectoryNotFound_Throws()
    {
        var missing = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Assert.That(!Directory.Exists(missing));
        Assert.Throws<DirectoryNotFoundException>(() => FileWeigher.CountFiles(missing, 0));
    }

    [Test]
    public void CountFiles_NegativeDepth_Throws()
    {
        var root = TempDir;
        try
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => FileWeigher.CountFiles(root, -1));
        }
        finally { DeleteDirSafe(root); }
    }

    [Test]
    public void CountFiles_DepthZero_OnlyRootFiles()
    {
        var root = TempDir;
        try
        {
            Touch(Path.Combine(root, "a.txt"));
            Touch(Path.Combine(root, "b.log"));
            Touch(Path.Combine(root, "c"));
            var sub1 = Directory.CreateDirectory(Path.Combine(root, "sub1")).FullName;
            Touch(Path.Combine(sub1, "d.txt"));
            Touch(Path.Combine(sub1, "e.txt"));

            var count = FileWeigher.CountFiles(root, maxDepth: 0);

            Assert.That(count, Is.EqualTo(3));
        }
        finally { DeleteDirSafe(root); }
    }

    [Test]
    public void CountFiles_DepthThree_IncludesUpToLevel3()
    {
        var root = TempDir;
        try
        {
            Touch(Path.Combine(root, "r0.txt"));
            var d1 = Directory.CreateDirectory(Path.Combine(root, "d1")).FullName;
            Touch(Path.Combine(d1, "r1.txt"));
            var d2 = Directory.CreateDirectory(Path.Combine(d1, "d2")).FullName;
            Touch(Path.Combine(d2, "r2.txt"));
            var d3 = Directory.CreateDirectory(Path.Combine(d2, "d3")).FullName;
            Touch(Path.Combine(d3, "r3.txt"));
            var d4 = Directory.CreateDirectory(Path.Combine(d3, "d4")).FullName;
            Touch(Path.Combine(d4, "r4.txt"));

            var count = FileWeigher.CountFiles(root, maxDepth: 3);

            Assert.That(count, Is.EqualTo(4));
        }
        finally { DeleteDirSafe(root); }
    }

    //[Test]
    //public void CountFiles_DepthTwo_OnlyTxt()
    //{
    //    var root = TempDir;
    //    try
    //    {
    //        Touch(Path.Combine(root, "a.txt"));
    //        Touch(Path.Combine(root, "b.log"));
    //        var d1 = Directory.CreateDirectory(Path.Combine(root, "d1")).FullName;
    //        Touch(Path.Combine(d1, "c.txt"));
    //        Touch(Path.Combine(d1, "d.md"));
    //        var d2 = Directory.CreateDirectory(Path.Combine(d1, "d2")).FullName;
    //        Touch(Path.Combine(d2, "e.txt"));
    //        Touch(Path.Combine(d2, "f.png"));
    //        var d3 = Directory.CreateDirectory(Path.Combine(d2, "d3")).FullName;
    //        Touch(Path.Combine(d3, "g.txt"));

    //        Predicate<FileInfo> onlyTxt = fi => string.Equals(fi.Extension, ".txt", StringComparison.OrdinalIgnoreCase);

    //        var count = FileWeigher.CountFiles(root, maxDepth: 2, predicate: onlyTxt);

    //        Assert.That(count, Is.EqualTo(3));
    //    }
    //    finally { DeleteDirSafe(root); }
    //}

    //[Test]
    //public void CountFiles_DepthTwo_CreatedAfterThreshold()
    //{
    //    var root = TempDir;
    //    try
    //    {
    //        var now = DateTime.UtcNow;
    //        var threshold = now.AddMinutes(-10);

    //        Touch(Path.Combine(root, "old0.txt"), createdUtc: threshold.AddMinutes(-1));
    //        Touch(Path.Combine(root, "new0.txt"), createdUtc: threshold.AddMinutes(+1));
    //        var d1 = Directory.CreateDirectory(Path.Combine(root, "d1")).FullName;
    //        Touch(Path.Combine(d1, "old1.txt"), createdUtc: threshold.AddMinutes(-2));
    //        Touch(Path.Combine(d1, "new1.log"), createdUtc: threshold.AddMinutes(+2));
    //        var d2 = Directory.CreateDirectory(Path.Combine(d1, "d2")).FullName;
    //        Touch(Path.Combine(d2, "new2.txt"), createdUtc: threshold.AddMinutes(+3));
    //        Touch(Path.Combine(d2, "old2.md"), createdUtc: threshold.AddMinutes(-3));
    //        var d3 = Directory.CreateDirectory(Path.Combine(d2, "d3")).FullName;
    //        Touch(Path.Combine(d3, "new3.txt"), createdUtc: threshold.AddMinutes(+4));

    //        Predicate<FileInfo> createdAfter = fi =>
    //        {
    //            return File.GetCreationTimeUtc(fi.FullName) > threshold;
    //        };

    //        var count = FileWeigher.CountFiles(root, maxDepth: 2, predicate: createdAfter);

    //        Assert.That(count, Is.EqualTo(3));
    //    }
    //    finally { DeleteDirSafe(root); }
    //}

    [Test]
    public void CountFiles_EmptyDirectory_ReturnsZero()
    {
        var root = TempDir;
        try
        {
            var count = FileWeigher.CountFiles(root, maxDepth: 5);
            Assert.That(count, Is.EqualTo(0));
        }
        finally { DeleteDirSafe(root); }
    }
}