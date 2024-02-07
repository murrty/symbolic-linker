namespace SymbolicLinkerInstaller;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

// fug
internal static class FileCompress {
    internal static void CompressToFile(string Source, string Destination) {
        using FileStream fsSrc = new(Source, FileMode.Open, FileAccess.Read);
        using FileStream fsDest = new(Destination, FileMode.Create);
        using GZipStream gzip = new(fsDest, CompressionLevel.Optimal);
        fsSrc.CopyTo(gzip);
        fsDest.Flush();
    }
    internal static void DecompressToFile(string Source, string Destination) {
        using FileStream fsSrc = new(Source, FileMode.Open, FileAccess.Read);
        using FileStream fsDest = new(Destination, FileMode.Create);
        using GZipStream gzip = new(fsSrc, CompressionMode.Decompress);
        gzip.CopyTo(fsDest);
        fsDest.Flush();
    }
    internal static void DecompressToFile(byte[] sourceBytes, string Destination) {
        using MemoryStream msSrc = new(sourceBytes);
        using FileStream fsDest = new(Destination, FileMode.Create);
        using GZipStream gzip = new(msSrc, CompressionMode.Decompress);
        gzip.CopyTo(fsDest);
        fsDest.Flush();
    }

    internal static async Task DecompressToFileAsync(byte[] sourceBytes, string Destination) {
        using MemoryStream msSrc = new(sourceBytes);
        await DecompressToFileAsync(msSrc, Destination);
    }
    internal static async Task DecompressToFileAsync(Stream source, string Destination) {
        using FileStream fsDest = new(Destination, FileMode.Create);
        using GZipStream gzip = new(source, CompressionMode.Decompress);
        await gzip.CopyToAsync(fsDest);
        await fsDest.FlushAsync();
    }
    internal static async Task DecompressToStreamAsync(byte[] sourceBytes, Stream Destination) {
        using MemoryStream msSrc = new(sourceBytes);
        await DecompressToStreamAsync(msSrc, Destination);
    }
    internal static async Task DecompressToStreamAsync(Stream source, Stream Destination) {
        using GZipStream gzip = new(source, CompressionMode.Decompress, true);
        await gzip.CopyToAsync(Destination);
        await Destination.FlushAsync();
        if (Destination.CanSeek) {
            Destination.Position = 0;
        }
    }

    internal static void CreateZipFile(string SourceDirectory, string DestinationFile) {
        int RemoveLength = SourceDirectory.Length + 1;
        using FileStream fs = new(DestinationFile, FileMode.Create, FileAccess.Write, FileShare.None);
        using ZipArchive Zip = new(fs, ZipArchiveMode.Create, true);
        if (Directory.Exists(SourceDirectory)) {
            InsertDirectory(SourceDirectory, Zip, RemoveLength);
        }
    }
    private static void InsertDirectory(string SourceDirectory, ZipArchive Zip, int RemoveLength) {
        var Directories = Directory.EnumerateDirectories(SourceDirectory);
        foreach (var Dir in Directories) {
            if (!Directory.Exists(Dir)) {
                continue;
            }
            InsertDirectory(Dir, Zip, RemoveLength);
        }

        var Files = Directory.EnumerateFiles(SourceDirectory);
        foreach (var Fi in Files) {
            if (!File.Exists(Fi)) {
                continue;
            }
            InsertFile(Fi, Zip, RemoveLength);
        }
    }
    private static void InsertFile(string SourceFile, ZipArchive Zip, int RemoveLength) {
        using FileStream fs = new(SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        var ZipEntry = Zip.CreateEntry(SourceFile.Substring(RemoveLength), CompressionLevel.Optimal);
        using var EntryStream = ZipEntry.Open();
        fs.CopyTo(EntryStream);
        EntryStream.Flush();
    }
}
