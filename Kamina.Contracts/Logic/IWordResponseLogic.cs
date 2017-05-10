using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kamina.Contracts.Objects;

namespace Kamina.Contracts.Logic
{
    public interface IWordResponseLogic
    {
        Task<TextResponse> HandleText(string text);
    }
}
