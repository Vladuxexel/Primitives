using System.Collections.ObjectModel;

namespace Primitives
{
    /// <summary>
    /// Converting data for TreeView
    /// </summary>
    public class ConvertedData
    {
        public string Type { get; set; }

        public ObservableCollection<BaseObject> Items { get; set; }
    }
}