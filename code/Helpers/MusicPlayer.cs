using Godot;

namespace code.Helpers
{
    public class MusicPlayer
    {
        const float DEFAULT_PITCH_SCALE = 1.0f;

        private AudioStreamPlayer audioPlayer;
        private string pathToMusicFolder;
        private string musicFileExtension;
        private string currentlyPlaying; // Name of song being played already

        public MusicPlayer(AudioStreamPlayer audioPlayer, string pathToSoundFolder, string soundFileExtension = ".wav")
        {
            this.audioPlayer = audioPlayer;
            this.pathToMusicFolder = pathToSoundFolder;
            this.musicFileExtension = soundFileExtension;
        }

        public void Play(string fileName)
        {
            audioPlayer.PitchScale = DEFAULT_PITCH_SCALE; // By default reset pitch scale
            if (!fileName.Equals(currentlyPlaying))
            {
                audioPlayer.Stream = ResourceLoader.Load<AudioStream>(pathToMusicFolder + fileName + musicFileExtension);
                audioPlayer.Play();
                currentlyPlaying= fileName;
            }
        }
        public void Play()
        {
            audioPlayer.Play();
        }

        public void Stop()
        {
            audioPlayer.Stop();
            currentlyPlaying= null;
        }
    }
}
