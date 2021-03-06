﻿using AmbientSounds.Controls;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace AmbientSounds.Services.Uwp
{
    /// <summary>
    /// Class for opening dialogs.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// UWP apps crash if more than one content dialog
        /// is opened at the same time. This flag
        /// will be used to help ensure only one
        /// dialog is open at a time.
        /// </summary>
        public static bool IsDialogOpen;

        /// <inheritdoc/>
        public async Task OpenSettingsAsync()
        {
            if (IsDialogOpen) 
                return;

            IsDialogOpen = true;
            var resources = ResourceLoader.GetForCurrentView();
            var dialog = new ContentDialog()
            {
                Title = resources.GetString("SettingsText"),
                CloseButtonText = resources.GetString("CloseText"),
                Content = new SettingsControl()
            };
            await dialog.ShowAsync();
            IsDialogOpen = false;
        }

        /// <inheritdoc/>
        public async Task<string> RenameAsync(string currentName)
        {
            if (IsDialogOpen)
                return currentName;

            IsDialogOpen = true;
            var resources = ResourceLoader.GetForCurrentView();
            var inputBoxControl = new RenameInputBox() { Input = currentName };
            bool enterClicked = false;
            var dialog = new ContentDialog()
            {
                Title = resources.GetString("RenameText"),
                CloseButtonText = resources.GetString("CancelText"),
                PrimaryButtonText = resources.GetString("RenameText"),
                Content = inputBoxControl
            };
            inputBoxControl.EnterClicked += (s, e) => { dialog.Hide(); enterClicked = true; };

            var result = await dialog.ShowAsync();
            IsDialogOpen = false;

            return result == ContentDialogResult.Primary || enterClicked ? inputBoxControl.Input : currentName;
        }
    }
}
