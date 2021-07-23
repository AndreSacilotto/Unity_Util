using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Spectra.Util
{
    public static class UtilResources
    {
        public static T[] LoadAll<T>() where T : UnityObject => Resources.LoadAll<T>("");

        public static void UnloadAssetImmediate(UnityObject obj)
        {
            Resources.UnloadAsset(obj);
            Resources.UnloadUnusedAssets();
        }

        public static void UnloadAllAssets(UnityObject[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
                Resources.UnloadAsset(objs[i]);
        }

        public static void UnloadAllAssetsImmediate(UnityObject[] objs)
        {
            UnloadAllAssets(objs);
            Resources.UnloadUnusedAssets();
        }
    }
}