//using System;
//using System.Collections.Concurrent;
//using System.IO;
//using System.Threading.Tasks;
//using Windows.Foundation;
//using Windows.Media;
//using Windows.Media.Core;
//using Windows.Media.Transcoding;
//using Discord;
//using Discord.Audio;
//using Discord.Commands;


//namespace Kamina.Logic.Commands
//{
//    public class SimpleAudio : ModuleBase
//    {
//        //// Scroll down further for the AudioService.
//        //// Like, way down
//        //private readonly AudioService _service;

//        //// Remember to add an instance of the AudioService
//        //// to your IServiceCollection when you initialize your bot
//        //public AudioModule(AudioService service)
//        //{
//        //    _service = service;
//        //}

//        //// You *MUST* mark these commands with 'RunMode.Async'
//        //// otherwise the bot will not respond until the Task times out.
//        //[Command("join", RunMode = RunMode.Async)]
//        //public async Task JoinCmd()
//        //{
//        //    await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
//        //}

//        //// Remember to add preconditions to your commands,
//        //// this is merely the minimal amount necessary.
//        //// Adding more commands of your own is also encouraged.
//        //[Command("leave", RunMode = RunMode.Async)]
//        //public async Task LeaveCmd()
//        //{
//        //    await _service.LeaveAudio(Context.Guild);
//        //}

//        //[Command("play", RunMode = RunMode.Async)]
//        //public async Task PlayCmd([Remainder] string song)
//        //{
//        //    await _service.SendAudioAsync(Context.Guild, Context.Channel, song);
//        //}

//        [Command("Qpm", RunMode = RunMode.Async)]
//        public async Task Qpm(IVoiceChannel channel = null)
//        {

//            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
//            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

//            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.

//            var audioClient = await channel.ConnectAsync();
//            //var channel = await Context.Guild.GetVoiceChannelAsync(212999038583177216);

//            //var audioClient = await channel.ConnectAsync();
//            //  //Channel = channel ?? (msg.Author as IGuildUser)?.VoiceChannel;
//            //  //if (channel == null) { await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

//            //         var ffmpeg = CreateStream(@"d:\test");
//            //  var outputoutput = ffmpeg.GetMediaStreamSource();

//            var v = File.Open("E:\test.mp3", FileMode.Open);


//            AudioOutStream discord = audioClient.CreatePCMStream(AudioApplication.Mixed);
//            //  //output
//            await v.CopyToAsync(discord);
//            await discord.FlushAsync();

//            //await output.CopyToAsync(discord);
//            //await discord.FlushAsync();
//            //// For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
//            //var audioClient = await channel.ConnectAsync();
//        }

//        //private FFmpegInteropMSS CreateStream(string path)
//        //{

//        //  //var v = File.Open(path, FileMode.Open);

//        //  //  var vv = v.AsRandomAccessStream();
//        //  //  var bla = FFmpegInteropMSS.CreateFFmpegInteropMSSFromStream(vv, true, true);

//        //  //  return bla;
//        //  //  // Pass MediaStreamSource to Media Element

//        //}
//    }

//    public class AudioService
//    {
//        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();

//        public async Task JoinAudio(IGuild guild, IVoiceChannel target)
//        {
//            IAudioClient client;
//            if (ConnectedChannels.TryGetValue(guild.Id, out client))
//            {
//                return;
//            }
//            if (target.Guild.Id != guild.Id)
//            {
//                return;
//            }

//            var audioClient = await target.ConnectAsync();

//            if (ConnectedChannels.TryAdd(guild.Id, audioClient))
//            {
//                // If you add a method to log happenings from this service,
//                // you can uncomment the following line to make use of that.
//                //await Log(LogSeverity.Info, $"Connected to voice on {guild.Name}.");
//            }
//        }

//        public async Task LeaveAudio(IGuild guild)
//        {
//            IAudioClient client;
//            if (ConnectedChannels.TryRemove(guild.Id, out client))
//            {
//                await client.StopAsync();
//                //await Log(LogSeverity.Info, $"Disconnected from voice on {guild.Name}.");
//            }
//        }

//        public async Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path)
//        {
//         //using (var ms = File.OpenRead("E:\test.mp3"))
//                //using (var rdr = new (ms))
//                //using (var wavStream = WaveFormatConversionStream.CreatePcmStream(rdr))
//                //using (var baStream = new BlockAlignReductionStream(wavStream))
//                //using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
//                //{
//                //    waveOut.Init(baStream);
//                //    waveOut.Play();
//                //    while (waveOut.PlaybackState == PlaybackState.Playing)
//                //    {
//                //        Thread.Sleep(100);
//                //    }
//                //}

           
//            }

//            //await Log(LogSeverity.Debug, $"Starting playback of {path} in {guild.Name}");
//            //      var output = CreateStream(path).StandardOutput.BaseStream;

//            // You can change the bitrate of the outgoing stream with an additional argument to CreatePCMStream().
//            // If not specified, the default bitrate is 96*1024.
//            //var stream = client.CreatePCMStream(AudioApplication.Music);
//            //await output.CopyToAsync(stream);
//            //await stream.FlushAsync().ConfigureAwait(false);

//        }

//    }

//    //private Process CreateStream(string path)
//    //{
//    //    return Process.Start(new ProcessStartInfo
//    //    {
//    //        FileName = "ffmpeg.exe",
//    //        Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
//    //        UseShellExecute = false,
//    //        RedirectStandardOutput = true
//    //    });
//    //}
//}

//}
//}