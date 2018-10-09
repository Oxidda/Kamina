using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Kamina.Common.Core.Logging;

namespace Kamina.Logic.Core.Audio
{
    public class AudioService
    {
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels =
            new ConcurrentDictionary<ulong, IAudioClient>();

        public async Task<bool> TryToJoinAudioChannel(IGuild guild, IVoiceChannel target)
        {
            try
            {

                IAudioClient client;
                if (ConnectedChannels.TryGetValue(guild.Id, out client))
                {
                    return false;
                }

                if (target.Guild.Id != guild.Id)
                {
                    return false;
                }

                var audioClient = await target.ConnectAsync();

                if (ConnectedChannels.TryAdd(guild.Id, audioClient))
                {
                    return true;
                    // If you add a method to log happenings from this service,
                    // you can uncomment these commented lines to make use of that.
                    //await Log(LogSeverity.Info, $"Connected to voice on {guild.Name}.");
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                throw;
            }
        }

        public async Task LeaveAudio(IGuild guild)
        {
            IAudioClient client;
            if (ConnectedChannels.TryRemove(guild.Id, out client))
            {
                await client.StopAsync();
                //await Log(LogSeverity.Info, $"Disconnected from voice on {guild.Name}.");
            }
        }

        public async Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path)
        {
            //await Logger.LogAsync(path);
            // Your task: Get a full path to the file if the value of 'path' is only a filename.
            if (!File.Exists(path))
            {
                await channel.SendMessageAsync("File does not exist.");
                return;
            }

            try
            {
                IAudioClient client;
                if (ConnectedChannels.TryGetValue(guild.Id, out client))
                {
                    //await Log(LogSeverity.Debug, $"Starting playback of {path} in {guild.Name}");
                    Process process = CreateStream(path);
                    AudioOutStream stream = client.CreatePCMStream(AudioApplication.Music);
                    process.Start();
                    Stream output = process.StandardOutput.BaseStream;
                    await output.CopyToAsync(stream);
                    await stream.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        private Process CreateStream(string path)
        {
            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $"-i {path}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };

            return p;
            //return Process.Start(new ProcessStartInfo
            //{
            //    FileName = "ffmpeg",
            //    Arguments = $"-i {path} -ac 2 -f s16le -ar 48000 pipe:1",
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true
            //});
        }
    }
}

