#nullable enable
namespace SymbolicLinker;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
internal class CommandLink : Button {
    private string _note = "";
    private ButtonIcon _buttonIcon;

    [DefaultValue("")]
    public string Note {
        get => _note;
        set {
            if (_note != value) {
                _note = value;
                UpdateNote();
            }
        }
    }

    [DefaultValue(ButtonIcon.None)]
    public ButtonIcon ButtonIcon {
        get => _buttonIcon;
        set {
            if (_buttonIcon != value) {
                _buttonIcon = value;
                UpdateIcon();
            }
        }
    }

    const int BS_COMMANDLINK = 0x000E;
    const int BCM_SETNOTE = 0x1609;
    const int BCM_FIRST = 0x1600;
    const int BM_SETIMAGE = 0x00F7;
    const int BCM_SETSHIELD = 0x160C;

    public CommandLink() {
        this.FlatStyle = FlatStyle.System;
    }

    protected override CreateParams CreateParams {
        get {
            var p = base.CreateParams;
            p.Style |= BS_COMMANDLINK;
            return p;
        }
    }

    private void UpdateNote() {
        if (_note.IsNullEmptyWhitespace()) {
            _ = NativeMethods.SendMessage(this.Handle, BCM_SETNOTE, 0, 0);
            return;
        }

        nint NoteParam = 0;
        try {
            NoteParam = Marshal.StringToHGlobalUni(_note);
            _ = NativeMethods.SendMessage(this.Handle, BCM_SETNOTE, 0, NoteParam);
        }
        finally {
            Marshal.FreeHGlobal(NoteParam);
        }
    }
    private void UpdateIcon() {
        _ = NativeMethods.SendMessage(this.Handle, BCM_FIRST, 0, 2);
        switch (_buttonIcon) {
            case ButtonIcon.Application: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Application.Handle);
            } break;
            case ButtonIcon.Asterisk: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Asterisk.Handle);
            } break;
            case ButtonIcon.Error: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Error.Handle);
            } break;
            case ButtonIcon.Exclamation: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Exclamation.Handle);
            } break;
            case ButtonIcon.Hand: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Hand.Handle);
            } break;
            case ButtonIcon.Information: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Information.Handle);
            } break;
            case ButtonIcon.Question: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Question.Handle);
            } break;
            case ButtonIcon.Warning: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Warning.Handle);
            } break;
            case ButtonIcon.WinLogo: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.WinLogo.Handle);
            } break;
            case ButtonIcon.Shield: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, SystemIcons.Shield.Handle);
            } break;
            case ButtonIcon.ShieldSmall: {
                _ = NativeMethods.SendMessage(this.Handle, BCM_SETSHIELD, 0, 2);
            } break;
            default: {
                _ = NativeMethods.SendMessage(this.Handle, BM_SETIMAGE, 1, 0);
            } break;
        }
    }
}
