using Blackpool.Zengenti.CMS.Models.GenericTypes;

namespace RazorPageWeddingWebsite.Helpers.Wrappers
{
    public interface ISerializationHelper
    {
        T? Deserialize<T>(SerialisedItem item);
        Task<T?> DeserializeAsync<T>(SerialisedItem item);
    }
}
