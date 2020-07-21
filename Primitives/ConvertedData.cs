using System.Collections.ObjectModel;

namespace Primitives
{
    public class ConvertedData
    {
        public string Type { get; set; }

        public ObservableCollection<BaseObject> Items { get; set; }
    }
}