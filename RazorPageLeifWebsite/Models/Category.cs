using Zengenti.Contensis.Delivery;

namespace RazorPageLeifExample.Models {

    public class Category: EntryModel  {
        public string Name { get; set; } = null!;
        public String? Description { get; set; }
    }
}
