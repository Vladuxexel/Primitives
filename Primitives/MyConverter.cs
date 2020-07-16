using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Primitives
{
    public class ConvertedData
    {
        public string Type { get; set; }

        public ObservableCollection<BaseObject> Items { get; set; }
    }
}