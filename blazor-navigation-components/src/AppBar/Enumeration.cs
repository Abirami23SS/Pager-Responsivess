namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Specifies the height mode of the <see cref="SfAppBar"/> component which define the height of the AppBar.
    /// </summary>
    public enum AppBarMode
    {
        /// <summary>
        /// Specifies default height for the AppBar.
        /// </summary>
        Regular,

        /// <summary>
        /// Specifies longer height for the AppBar to show the longer titles, images, or to provide a stronger presence.
        /// </summary>
        Prominent,

        /// <summary>
        /// Specifies compressed (short) height for AppBar to accommodate all the app bar content in a denser layout.
        /// </summary>
        Dense
    }

    /// <summary>
    /// Specifies the position of the <see cref="SfAppBar"/> component.
    /// </summary>
    public enum AppBarPosition
    {
        /// <summary>
        /// Position the AppBar at the top.
        /// </summary>
        Top,

        /// <summary>
        /// Position the AppBar at the bottom.
        /// </summary>
        Bottom
    }

    /// <summary>
    /// Specifies the color of the <see cref="SfAppBar"/> component.
    /// </summary>
    public enum AppBarColor
    {
        /// <summary>
        /// Use light color for AppBar.
        /// </summary>
        Light,

        /// <summary>
        /// Use dark color for AppBar.
        /// </summary>
        Dark,

        /// <summary>
        /// Use primary color for AppBar.
        /// </summary>
        Primary,

        /// <summary>
        /// Inherit color from parent for AppBar. AppBar background and colors inherited from it's parent element.
        /// </summary>
        Inherit
    }
}
