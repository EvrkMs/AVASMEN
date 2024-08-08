// DataStore
using System.Collections.Generic;

namespace APIData
{
    public static class DataStore
    {
        public static Dictionary<string, long> Users { get; private set; } = new Dictionary<string, long>();
        public static Dictionary<string, int> Names { get; private set; } = new Dictionary<string, int>();
        public static List<string> NameList { get; private set; } = new List<string>();
        public static string TokenBot { get; set; }
        public static long ForwardChat { get; set; }
        public static long ChatId { get; set; }

        public static void Initialize()
        {
            ApiService.LoadData();
        }
    }
}