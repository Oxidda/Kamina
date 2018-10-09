
namespace Kamina.Contracts.Core.Objects
{
    public class TextResponse
    {
        public string Text { get; set; }
        public bool ShouldMentionSender { get; set; }
        public ulong PersonToMention { get; set; } = 0;
    }
}
