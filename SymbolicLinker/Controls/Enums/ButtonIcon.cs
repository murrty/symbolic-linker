namespace SymbolicLinker;
/// <summary>
/// The icon that will appear within the button.
/// </summary>
internal enum ButtonIcon {
    /// <summary>
    /// No icon.
    /// </summary>
    None,
    /// <summary>
    /// Appears as the default application icon.
    /// </summary>
    Application,
    /// <summary>
    /// Appears as a blue circle with an "i" in the middle.
    /// </summary>
    Asterisk,
    /// <summary>
    /// Appears as a red circle with an "x" in the middle.
    /// <para>Same as <see cref="Hand"/>.</para>
    /// </summary>
    Error,
    /// <summary>
    /// Appears as a yellow triangle with an exclamation "!" in the middle.
    /// <para>Same as <see cref="Warning"/>.</para>
    /// </summary>
    Exclamation,
    /// <summary>
    /// Appears as a red circle with an "x" in the middle.
    /// <para>Same as <see cref="Error"/>.</para>
    /// </summary>
    Hand,
    /// <summary>
    /// Appears as a blue circle with an "i" in the middle.
    /// <para>Same as <see cref="Asterisk"/>.</para>
    /// </summary>
    Information,
    /// <summary>
    /// Appears as a blue circle with a question mark "?" in the middle.
    /// </summary>
    Question,
    /// <summary>
    /// Appears as a yellow triangle with an exclamation "!" in the middle.
    /// <para>Same as <see cref="Exclamation"/>.</para>
    /// </summary>
    Warning,
    /// <summary>
    /// Supposedly appears as the Windows Logo icon, but may just be the <see cref="Application"/> icon.
    /// </summary>
    WinLogo,
    /// <summary>
    /// Appears as a Yellow/Blue User Account Control shield.
    /// <para>This is larger than <see cref="ShieldSmall"/>.</para>
    /// </summary>
    Shield,
    /// <summary>
    /// Appears as a Yellow/Blue User Account Control shield.
    /// <para>This is smaller than <see cref="Shield"/>.</para>
    /// </summary>
    ShieldSmall
}
