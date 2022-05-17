using System.Collections.Generic;

namespace Wechaty.Module.Schemas
{
    public interface IFilter
    {
        StringOrRegex? this[string key] { get; }
        IReadOnlyList<string> Keys { get; }
    }
}
