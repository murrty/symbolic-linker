#nullable enable
namespace SymbolicLinker;
using System.Diagnostics;
internal static class Tests {
    [Conditional("DEBUG")]
    public static void RunTests() {
        //CreateTestFileSymbolicLink();
        //CreateTestFileHardLink();
        //CreateTestDirectorySymbolicLink();
        //CreateTestDirectoryJunction();

        Console.WriteLine("Tests finished.");
    }

    const string FileA = @"C:\Temp\FileA.jpg";
    const string FileB = @"C:\Temp\FileB.jpg";
    const string DirA = @"C:\Temp";
    const string DirB = @"C:\Temp2";

    private static void CreateTestFileSymbolicLink() {
        MakeLink.CreateFileSymbolicLink(
            Source: FileB,
            DestinationDirectory: Environment.CurrentDirectory,
            DestinationFileName: "SymbolicLink.jpg",
            Elevated: false);
    }
    private static void CreateTestFileHardLink() {
        MakeLink.CreateFileHardLink(
            Source: FileB,
            DestinationDirectory: Environment.CurrentDirectory,
            DestinationFileName: "HardLink.jpg",
            Elevated: false);
    }
    private static void CreateTestDirectorySymbolicLink() {
        MakeLink.CreateDirectorySymbolicLink(
            Source: DirB,
            DestinationDirectory: Environment.CurrentDirectory,
            DestinationFolderName: "SymbolicLink",
            Elevated: false);
    }
    private static void CreateTestDirectoryJunction() {
        MakeLink.CreateDirectoryJunction(
            Source: DirB,
            DestinationDirectory: Environment.CurrentDirectory,
            DestinationFolderName: "Junction",
            Elevated: false);
    }
}
