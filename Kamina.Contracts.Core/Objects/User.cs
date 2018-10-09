using System.Collections.Generic;

namespace Kamina.Contracts.Core.Objects
{
    public class User
    {
        public ulong Id { get; set; }
        public Guild Guild { get; set; }
        public string Name { get; set; }
        public List<CalenderItem> CalenderItems { get; set; }
    }
}
