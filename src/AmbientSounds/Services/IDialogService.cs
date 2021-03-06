﻿using System.Threading.Tasks;

namespace AmbientSounds.Services
{
    /// <summary>
    /// Interface for triggering a dialog
    /// or modal pop up.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens a settings dialog.
        /// </summary>
        Task OpenSettingsAsync();

        /// <summary>
        /// Opens a rename dialog.
        /// </summary>
        /// <param name="currentName">The current name use to pre-populate the field.</param>
        /// <returns>The raw value from the input text field. This can be the same as the currentName value.</returns>
        Task<string> RenameAsync(string currentName);
    }
}
