using System.Threading.Tasks;
using Kamina.Contracts.Core.Objects;

namespace Kamina.Contracts.Core.Logic
{
    public interface IWordResponseLogic
    {
        Task<TextResponse> HandleText(string text);
    }
}
