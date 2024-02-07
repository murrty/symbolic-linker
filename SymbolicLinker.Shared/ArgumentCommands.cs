namespace SymbolicLinker.Shared;
internal static class ArgumentCommands {
    public const string CreateSymbolicFileLink = "-s";
    public const string CreateSymbolicFileLinkHere = "-sh";
    public const string CreateSymbolicFileLinkElevated = "-se";
    public const string CreateSymbolicFileLinkHereElevated = "-she";

    public const string CreateHardFileLink = "-h";
    public const string CreateHardFileLinkHere = "-hh";
    public const string CreateHardFileLinkElevated = "-he";
    public const string CreateHardFileLinkHereElevated = "-hhe";

    public const string CreateSymbolicDirectoryLink = "-d";
    public const string CreateSymbolicDirectoryLinkHere = "-dh";
    public const string CreateSymbolicDirectoryLinkElevated = "-de";
    public const string CreateSymbolicDirectoryLinkHereElevated = "-dhe";

    public const string CreateDirectoryJunction = "-j";
    public const string CreateDirectoryJunctionHere = "-jh";
    public const string CreateDirectoryJunctionElevated = "-je";
    public const string CreateDirectoryJunctionHereElevated = "-jhe";
}
