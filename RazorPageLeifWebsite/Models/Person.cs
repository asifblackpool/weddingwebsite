using Zengenti.Contensis.Delivery;

namespace RazorPageLeifExample.Models {

    public class Person: EntryModel  {
        public string Name { get; set; } = null!;
        public Image? Photo { get; set; }

        public string? PhotoUrl => ImageHelper.GetImageUrl(Photo);
    }
}
