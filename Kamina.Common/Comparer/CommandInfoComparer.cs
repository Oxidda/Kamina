using System.Collections.Generic;
using Discord.Commands;

namespace Kamina.Common.Comparer
{
    public class CommandInfoComparer : IEqualityComparer<CommandInfo>
    {
        public bool Equals(CommandInfo x, CommandInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(CommandInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
