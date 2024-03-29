﻿using Godot;

namespace code.Helpers
{
    public class SoundPlayer
    {
        private AudioStreamPlayer audioPlayer;
        private string pathToSoundFolder;
        private string soundFileExtension;

        public SoundPlayer(AudioStreamPlayer audioPlayer, string pathToSoundFolder, string soundFileExtension = ".wav")
        {
            this.audioPlayer = audioPlayer;
            this.pathToSoundFolder = pathToSoundFolder;
            this.soundFileExtension = soundFileExtension;
        }

        public void Play(string fileName)
        {
            audioPlayer.Stream = ResourceLoader.Load<AudioStream>(pathToSoundFolder + fileName + soundFileExtension);
            audioPlayer.Play();
        }
        public void Play()
        {
            audioPlayer.Play();
        }
    }
}
