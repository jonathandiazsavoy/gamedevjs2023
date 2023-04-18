using Godot;

namespace code.Helpers
{
    public class MusicPlayer
    {
        private AudioStreamPlayer audioPlayer;
        private string pathToMusicFolder;
        private string musicFileExtension;

        public MusicPlayer(AudioStreamPlayer audioPlayer, string pathToSoundFolder, string soundFileExtension = ".wav")
        {
            this.audioPlayer = audioPlayer;
            this.pathToMusicFolder = pathToSoundFolder;
            this.musicFileExtension = soundFileExtension;
        }

        public void Play(string fileName)
        {
            audioPlayer.Stream = ResourceLoader.Load<AudioStream>(pathToMusicFolder + fileName + musicFileExtension);
            audioPlayer.PitchScale= 1.0f;
            audioPlayer.Play();
        }
        public void Play()
        {
            audioPlayer.Play();
        }
    }
}
