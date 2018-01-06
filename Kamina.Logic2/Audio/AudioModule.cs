using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Kamina.Logic2.Audio
{
    public class AudioModule : ModuleBase<ICommandContext>
    {
        // Scroll down further for the AudioService.
        // Like, way down
        private readonly AudioService _service;

        // Remember to add an instance of the AudioService
        // to your IServiceCollection when you initialize your bot
        public AudioModule(AudioService service)
        {
            _service = service;
        }

        // You *MUST* mark these commands with 'RunMode.Async'
        // otherwise the bot will not respond until the Task times out.
        [Command("qpm", RunMode = RunMode.Async)]
        public async Task QpmTask()
        {
            await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
            await _service.SendAudioAsync(Context.Guild, Context.Channel, "Tutturuu.mp3");
            await _service.LeaveAudio(Context.Guild);
        }

        [Command("toucha", RunMode = RunMode.Async)]
        public async Task TouchaTask()
        {
            await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
            await _service.SendAudioAsync(Context.Guild, Context.Channel, "SOMEBODY TOUCHA MY SPAGHET.mp3");
            await _service.LeaveAudio(Context.Guild);
        }

        // Remember to add preconditions to your commands,
        // this is merely the minimal amount necessary.
        // Adding more commands of your own is also encouraged.
        //[Command("leave", RunMode = RunMode.Async)]
        //public async Task LeaveCmd()
        //{
        //    await _service.LeaveAudio(Context.Guild);
        //}

        //[Command("play", RunMode = RunMode.Async)]
        //public async Task PlayCmd([Remainder] string song)
        //{
        //    await _service.SendAudioAsync(Context.Guild, Context.Channel, song);
        //}
    }
}
