using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class MakeLevelList
    {
        [MenuItem("Assets/Create/LevelList", false, 0)]
        public static void CreateLevelList()
        {
            var level1 = ScriptableObject.CreateInstance<Level>();
            level1.Tiles = new List<Vector2> { new Vector2 { x = 4, y = 7 }, new Vector2 { x = 4, y = 6 } };
            AssetDatabase.CreateAsset(level1, "Assets/Levels/Level1.asset");
            AssetDatabase.SaveAssets();

            var list = ScriptableObject.CreateInstance<LevelList>();
            list.Levels = new List<Level> { level1 };
            AssetDatabase.CreateAsset(list, "Assets/Levels/LevelList.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
