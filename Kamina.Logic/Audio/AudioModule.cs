using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Kamina.Common.Channel;

namespace Kamina.Logic.Audio
{
    public class AudioModule : ModuleBase<ICommandContext>
    {
        private readonly AudioService service;

        public AudioModule(AudioService service)
        {
            this.service = service;
        }
        
        [Command("qpm", RunMode = RunMode.Async)]
        public async Task QpmTask()
        {
            await PlaySoundByMp3("Tutturuu");
        }

        [Command("toucha", RunMode = RunMode.Async)]
        public async Task TouchaTask()
        {
            await PlaySoundByMp3("SOMEBODY TOUCHA MY SPAGHET");
        }

        [Command("lol", RunMode = RunMode.Async)]
        public async Task LolTask()
        {
            await PlaySoundByMp3("whitefoxlaughing");
        }

        [Command("yolo", RunMode = RunMode.Async)]
        public async Task YoloTask()
        {
            await PlaySoundByMp3("yolo");
        }
        
        [Command("theway", RunMode = RunMode.Async)]
        public async Task TheWayTask()
        {
            await PlaySoundByMp3("theway");
        }

        [Command("5euro", RunMode = RunMode.Async)]
        public async Task FiveEuroTask()
        {
            await PlaySoundByMp3("5euro");
        }

        [Command("virgin", RunMode = RunMode.Async)]
        public async Task VirginTask()
        {
            await PlaySoundByMp3("virgin");
        }

        //[Command("1hp", RunMode = RunMode.Async)]
        //public async Task OneHpTask()
        //{
        //    await PlaySoundByMp3("1hp");
        //}

        private async Task PlaySoundByMp3(string mp3)
        {
            if (await Context.IsVoice())
            {
                if (await service.TryToJoinAudioChannel(Context.Guild, (Context.User as IVoiceState).VoiceChannel))
                {
                    await service.SendAudioAsync(Context.Guild, Context.Channel, $"AudioFiles\\{mp3}.mp3");
                    await service.LeaveAudio(Context.Guild);
                }
            }
        }
    }
}
