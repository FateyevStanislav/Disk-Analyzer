using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer;

public class Program
{
    public static void Main(string[] args)
    {
        //var direct = new DirectoryInfo("C:\\Users\\Станислав\\Downloads");
        //var res1 = direct.GetFilesSortedBy(f => f.Extension);
        //foreach (var file in res1)
        //    Console.WriteLine(file.FullName);
        //Console.WriteLine();
        //var res2 = direct.GetFilesWhere(f => f.LastAccessTime < new DateTime(2025, 11, 3));
        //foreach (var file in res1)
        //    Console.WriteLine(file.FullName);
        //Console.WriteLine();
        //var res3 = direct.GetFilesGroupedBy(f => f.Extension);
        //foreach (var group in res3)
        //{
        //    Console.WriteLine(group.Key);
        //    foreach (var file in group)
        //        Console.WriteLine(file);
        //}
        var file1 = new FileInfo("B:\\БД домашка.docx");
        var file2 = new FileInfo("B:\\БД домашка — копия.docx");
        Console.WriteLine(file1.IsEqualTo(file2));
    }
}