namespace SymbolicLinker;
using System.Runtime.InteropServices;
internal partial class NativeMethods {
    [DllImport("User32.dll")]
    public static extern int SendMessage(nint hwnd, nint msg, nint wParam, nint lParam);
}
