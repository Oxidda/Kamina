using Discord.Audio;

namespace Kamina.Logic.Core.Audio
{
    public class AudioPlayerForGuild
    {
        public AudioPlayerForGuild(IAudioClient audioClient)
        {
            AudioClient = audioClient;
        }
        public IAudioClient AudioClient { get; set; }
        public bool IsSendingAudio { get; set; }
    }
}