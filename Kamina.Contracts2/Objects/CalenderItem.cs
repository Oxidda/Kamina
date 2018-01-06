using System;
using System.Collections.Generic;

namespace Kamina.Contracts.Objects
{
    public class CalenderItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guild Guild { get; set; }
        public List<User> Users { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CalenderRecurrance Recurrance { get; set; }
    }

    public enum CalenderRecurrance
    {
        Once = 0,
        Daily = 1,
        Monthly = 2,
        Yearly = 3
    }
}
