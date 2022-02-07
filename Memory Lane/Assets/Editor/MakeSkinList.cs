using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class MakeSkinList
    {
        [MenuItem("Assets/Create/Skin List", false, 0)]
        public static void CreateSkinList()
        {
            AssetDatabase.DeleteAsset("Assets/Skins");
            AssetDatabase.CreateFolder("Assets", "Skins");
            AssetDatabase.SaveAssets();

            Debug.ClearDeveloperConsole();

            var list = ScriptableObject.CreateInstance<SkinList>();
            list.Skins = new List<Skin>();

            var skinModelIds = AssetDatabase.FindAssets("skin", new string[] { "Assets/Models/Skins" });
            for (var i = 0; i < skinModelIds.Length; i++)
            {
                var skin = ScriptableObject.CreateInstance<Skin>();
                skin.Id = i;

                var skinName = AssetDatabase.GUIDToAssetPath(skinModelIds[i]);
                skin.Model = AssetDatabase.LoadAssetAtPath<GameObject>(skinName);

                AssetDatabase.CreateAsset(skin, $"Assets/Skins/Skin{i + 1}.asset");
                AssetDatabase.SaveAssets();

                list.Skins.Add(skin);
            }

            AssetDatabase.CreateAsset(list, "Assets/Skins/SkinList.asset");
            AssetDatabase.SaveAssets();

            Debug.Log("Skins generated successfully");
        }
    }
}
