using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Kamina.Common.Channel;

namespace Kamina.Logic.Audio
{
    public class AudioModule : ModuleBase<ICommandContext>
    {
        private readonly AudioService service;

        ///TODO : Put in JSON file so I donbt have to fucking code all these things./

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

        [Command("nioce", RunMode = RunMode.Async)]
        public async Task NioceTask()
        {
            await PlaySoundByMp3("nioce");
        }

        [Command("thevoiceofholland", RunMode = RunMode.Async)]
        public async Task TheVoiceOfHollandTask()
        {
            await PlaySoundByMp3("thevoiceofholland");
        }

        [Command("papeti", RunMode = RunMode.Async)]
        public async Task PapetiTask()
        {
            await PlaySoundRandomFromFolder(4, 1, "papetipoepeti");
        }

        [Command("echthe", RunMode = RunMode.Async)]
        public async Task EchtHeTask()
        {
            await PlaySoundByMp3("echthe");
        }

        [Command("alcohol", RunMode = RunMode.Async)]
        public async Task AlcoholTask()
        {
            await PlaySoundByMp3("alcohol");
        }

        [Command("mooiman", RunMode = RunMode.Async)]
        public async Task MooiManTask()
        {
            await PlaySoundByMp3("mooiman");
        }

        [Command("rage", RunMode = RunMode.Async)]
        public async Task RageTask()
        {
            await PlaySoundByMp3("rage");
        }

        [Command("haha", RunMode = RunMode.Async)]
        public async Task HahaTask()
        {
            await PlaySoundByMp3("haha");
        }

        [Command("fu", RunMode = RunMode.Async)]
        public async Task FuTask()
        {
            await PlaySoundByMp3("fu");
        }

        [Command("kingjj", RunMode = RunMode.Async)]
        public async Task KingjjTask()
        {
            await PlaySoundByMp3("kingjj");
        }

        //[Command("1hp", RunMode = RunMode.Async)]
        //public async Task OneHpTask()
        //{
        //    await PlaySoundByMp3("1hp");
        //}

        private async Task PlaySoundRandomFromFolder(int max, int min, string foldername)
        {
            if (await Context.IsVoice())
            {
                if (await service.TryToJoinAudioChannel(Context.Guild, (Context.User as IVoiceState).VoiceChannel))
                {
                    Random random = new Random();
                    int number = random.Next(min, max);

                    await service.SendAudioAsync(Context.Guild, Context.Channel, $"AudioFiles\\{foldername}\\{number}.mp3");
                    await service.LeaveAudio(Context.Guild);
                }
            }

        }

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
