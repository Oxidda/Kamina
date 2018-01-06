//using System;
//using System.Collections.Concurrent;
//using System.Diagnostics;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;
//using Discord;
//using Discord.Audio;
//using Discord.Commands;
//using Discord.WebSocket;
//using Kamina.Common.Logging;


//namespace Kamina.Logic.Commands
//{
//    public class SimpleAudio : ModuleBase
//    {
//        private AudioService service;

//        public SimpleAudio(AudioService serivce)
//        {
//            this.service = serivce;
//        }

//        [Command("play", RunMode = RunMode.Async)]
//        public async Task PlayCmd([Remainder] string s)
//        {
//            var song = @"C:\Users\Jasper\Source\Repos\Kamina2\Kamina.Console\bin\Debug\Tutturuu.mp3";
//            await service.SendAudioAsync(Context.Guild, Context.Channel, song);
//        }

//        [Command("join", RunMode = RunMode.Async)]
//        public async Task JoinCmd()
//        {
//            await service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
//        }

//        //[Command("join")]
//        // public async Task JoinChannel(IVoiceChannel channel = null)
//        // {
//        //     // Get the audio channel
//        //     channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
//        //     if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

//        //     // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
//        //     IAudioClient audioClient = await channel.ConnectAsync();

//        //     string path = "Tutturuu.mp3";
//        //     using (var ffmpeg = CreateStream(path))
//        //     using (var output = ffmpeg.StandardOutput.BaseStream)
//        //     using (var discord = audioClient.CreatePCMStream(AudioApplication.Mixed))
//        //     {
//        //         try { await output.CopyToAsync(discord); }
//        //         finally { await discord.FlushAsync(); }
//        //     }
//        // }

//        private Process CreateStream(string path)
//        {
//            return Process.Start(new ProcessStartInfo
//            {
//                FileName = "ffmpeg",
//                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
//                UseShellExecute = false,
//                RedirectStandardOutput = true,
//            });
//        }


//        //[Command("Qpm", RunMode = RunMode.Async)]
//        //public async Task Qpm(IVoiceChannel channel = null)
//        //{ 
//        //    channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
//        //    if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

//        //    // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.

//        //    var audioClient = await channel.ConnectAsync();
//        //    //var channel = await Context.Guild.GetVoiceChannelAsync(212999038583177216);

//        //    //var audioClient = await channel.ConnectAsync();
//        //    //  //Channel = channel ?? (msg.Author as IGuildUser)?.VoiceChannel;
//        //    //  //if (channel == null) { await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

//        //    //         var ffmpeg = CreateStream(@"d:\test");
//        //    //  var outputoutput = ffmpeg.GetMediaStreamSource();

//        //    //var v = File.Open("E:\test.mp3", FileMode.Open);
//        //    string path = "Tutturuu.mp3";
//        //    using (var ffmpeg = CreateStream(path))
//        //    using (var output = ffmpeg.StandardOutput.BaseStream)
//        //    using (var discord = audioClient.CreatePCMStream(AudioApplication.Mixed))
//        //    {
//        //        try { Task.WaitAll(output.CopyToAsync(discord)); }
//        //        finally { await discord.FlushAsync(); }
//        //    }

//        //    //using (var ffmpeg = CreateStream("Tutturuu.mp3"))
//        //    //using (var output = ffmpeg.StandardOutput.BaseStream)
//        //    //using (var discord = audioClient.CreatePCMStream(AudioApplication.Mixed))
//        //    //{
//        //    //    try { await output.CopyToAsync(discord); }
//        //    //    finally { await discord.FlushAsync(); }
//        //    //}
//        //}

//        ////private Process CreateStream(string path)
//        ////{
//        ////    return Process.Start(new ProcessStartInfo
//        ////    {
//        ////        FileName = "ffmpeg.exe",
//        ////        Arguments = $"-hide_banner -loglevel panic -i {path} -ac 2 -f s16le -ar 48000 pipe:1",
//        ////        UseShellExecute = false,
//        ////        RedirectStandardOutput = true
//        ////    });
//        ////}
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
//            audioClient.Connected += AudioClient_Connected;
//            if (ConnectedChannels.TryAdd(guild.Id, audioClient))
//            { 
//                // If you add a method to log happenings from this service,
//                // you can uncomment these commented lines to make use of that.
//                //await Log(LogSeverity.Info, $"Connected to voice on {guild.Name}.");
//            }
//        }

//        private Task AudioClient_Connected()
//        {
//            Logger.Log("bla");
//            return Task.CompletedTask;
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

//        //public static byte[] AdjustVolume(byte[] audioSamples, float volume)
//        //{
//        //    if (Math.Abs(volume - 1f) < 0.0001f) return audioSamples;

//        //    // 16-bit precision for the multiplication
//        //    var volumeFixed = (int)Math.Round(volume * 65536d);

//        //    var count = audioSamples.Length / 2;

//        //    fixed (byte* srcBytes = audioSamples)
//        //    {
//        //        var src = (short*)srcBytes;

//        //        for (var i = count; i != 0; i--, src++)
//        //            *src = (short)(((*src) * volumeFixed) >> 16);
//        //    }

//        //    return audioSamples;
//        //}

//        public async Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path)
//        {
//            // Your task: Get a full path to the file if the value of 'path' is only a filename.
//            if (!File.Exists(path))
//            {
//                await channel.SendMessageAsync("File does not exist.");
//                return;
//            }
//            var _bytesSent = 0;
//            IAudioClient client;
//            if (ConnectedChannels.TryGetValue(guild.Id, out client))
//            {
//                //await Log(LogSeverity.Debug, $"Starting playback of {path} in {guild.Name}");
//                using (var output = CreateStream(path).StandardOutput.BaseStream)
//                {
//                    var pcm = client.CreatePCMStream(AudioApplication.Music, bufferMillis: 500);
//                   await client.SetSpeakingAsync(true);
                    
//                   // client.

//                    byte[] buffer = new byte[3840];
//                    int bytesRead = 0;

//                    while ((bytesRead = Read(buffer, 0, buffer.Length, output)) > 0)
//                    {
//                      //  AdjustVolume(buffer, Volume);
//                        await pcm.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
//                        unchecked { _bytesSent += bytesRead; }
//                    }
//                    //using (var stream = client.CreatePCMStream(AudioApplication.Mixed)) //AudioApplication.Mixed
//                    //{
//                    //    try { await output.CopyToAsync(stream); }
//                    //    finally { await stream.FlushAsync(); }
//                    //}
//                }
//            }
//        }
//        private readonly object locker = new object();
//        public int Read(byte[] b, int offset, int toRead, Stream outstream)
//        {
//            lock (locker)
//                return outstream.Read(b, offset, toRead);
//        }


//        private Process CreateStream(string path)
//        {

//            var args = $"-err_detect ignore_err -i {path} -f s16le -ar 48000 -vn -ac 2 pipe:1 -loglevel error -af 1";
//            //if (!_isLocal)
//            //    args = "-reconnect 1 -reconnect_streamed 1 -reconnect_delay_max 5 " + args;

//            return Process.Start(new ProcessStartInfo
//            {
//                FileName = "ffmpeg",
//                Arguments = args,
//                UseShellExecute = false,
//                RedirectStandardOutput = true,
//                RedirectStandardError = false,
//                CreateNoWindow = true,
//            });

//            //return Process.Start(new ProcessStartInfo
//            //{
//            //    FileName = "ffmpeg.exe",
//            //    Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
//            //    UseShellExecute = false,
//            //    RedirectStandardOutput = true
//            //});
//        }
//    }

//}

////private Process CreateStream(string path)
////{
////    return Process.Start(new ProcessStartInfo
////    {
////        FileName = "ffmpeg.exe",
////        Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
////        UseShellExecute = false,
////        RedirectStandardOutput = true
////    });
////}
