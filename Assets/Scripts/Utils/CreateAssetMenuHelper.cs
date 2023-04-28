using System;

namespace Utils
{
    public static class CreateAssetMenuHelper
    {
        public static string MenuName<T>()
        {
            return $"DemonicOcupation/{typeof(T).FullName!.Replace(".","/")}";
        }

        public static string ItemName<T>()
        {
            return typeof(T).Name;
        }
    }
}