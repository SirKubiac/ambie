﻿using AmbientSounds.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmbientSounds.Services
{
    /// <summary>
    /// Interface for playing multiple
    /// sounds simultaneously.
    /// </summary>
    public interface IMixMediaPlayerService
    {
        /// <summary>
        /// Sound is added.
        /// </summary>
        event EventHandler<Sound> SoundAdded;

        /// <summary>
        /// Sound was removed. String is
        /// the sound's ID.
        /// </summary>
        event EventHandler<string> SoundRemoved;

        /// <summary>
        /// Raised when playback changes between
        /// playing and paused.
        /// </summary>
        event EventHandler<MediaPlaybackState> PlaybackStateChanged;

        /// <summary>
        /// Raised when a sound activation was blocked
        /// because the active sound limit was reached.
        /// </summary>
        event EventHandler MaxReached;

        /// <summary>
        /// Global volume control. Max = 1. Min = 0.
        /// </summary>
        double GlobalVolume { get; set; }

        /// <summary>
        /// The ID of the current mix being played.
        /// If a mix is not being played, this will be empty.
        /// </summary>
        string CurrentMixId { get; set; }

        /// <summary>
        /// Dictionary of screensavers for the active tracks.
        /// </summary>
        Dictionary<string, string[]> Screensavers { get; }

        /// <summary>
        /// The current global state of the player.
        /// </summary>
        MediaPlaybackState PlaybackState { get; set; }

        /// <summary>
        /// Resumes playback.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses playback.
        /// </summary>
        void Pause();

        /// <summary>
        /// If the given sound is playing,
        /// the sound will be paused and removed.
        /// If the sound was paused, the sound
        /// will be played.
        /// </summary>
        /// <param name="s">The sound to toggle.</param>
        /// <param name="keepPaused">Optional. If true, an inserted sound will not be played automatically.</param>
        Task ToggleSoundAsync(Sound s, bool keepPaused = false, string parentMixId = "");

        /// <summary>
        /// Removes all active tracks.
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Removes the sound
        /// from being played.
        /// </summary>
        /// <param name="soundId">The sound to pause and remove.</param>
        void RemoveSound(string soundId);

        /// <summary>
        /// Returns true if the sound is active.
        /// </summary>
        /// <param name="soundId">The sound to check.</param>
        bool IsSoundPlaying(string soundId);

        /// <summary>
        /// Retrieves the volume for the given sound.
        /// </summary>
        double GetVolume(string soundId);

        /// <summary>
        /// Sets the volume for the given sound.
        /// </summary>
        void SetVolume(string soundId, double value);
    }
}
