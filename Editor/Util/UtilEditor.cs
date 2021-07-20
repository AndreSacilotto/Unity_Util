using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Spectra.Util.Editor
{
    public static class UtilEditor
    {
        #region SO
        public static string GetSOAssetPath(ScriptableObject so)
        {
            return AssetDatabase.GetAssetPath(so.GetInstanceID());
        }
        public static string GetSOFileNameByID(ScriptableObject so)
        {
            return Path.GetFileNameWithoutExtension(GetSOAssetPath(so));
        }

        #endregion

        #region FindAsset
        public static T[] FindAssets<T>() where T : UnityObject
        {
            string[] assets = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            return Array.ConvertAll(assets, x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x)));
        }

        public static string[] FindAssetsPath<T>() where T : UnityObject
        {
            string[] assets = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            return Array.ConvertAll(assets, x => AssetDatabase.GUIDToAssetPath(x));
        }

        public static void FindAssets<T>(Action<T> act) where T : UnityObject
        {
            string[] assets = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            for (int i = 0; i < assets.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(assets[i]);
                T obj = AssetDatabase.LoadAssetAtPath<T>(path);
                act(obj);
            }
        }
        #endregion

    }
}