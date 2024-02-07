#nullable enable
namespace SymbolicLinker;
/// <summary>
/// Options that can be used by the user when an existing file or folder is already present at the destination.
/// </summary>
internal enum ExistingAction : byte {
    /// <summary>
    /// Creates a backup of the existing file or folder.
    /// </summary>
    Backup = 0,
    /// <summary>
    /// Overwrites the existing file or folder.
    /// </summary>
    Overwrite = 1,
    /// <summary>
    /// Allows the user to change the name of the existing file or folder.
    /// </summary>
    ChangeExistingName = 2,
    /// <summary>
    /// Allows the user to specify a new name for the link.
    /// </summary>
    NewName = 3,
}
