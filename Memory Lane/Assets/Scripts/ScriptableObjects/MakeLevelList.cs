using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class MakeLevelList
    {
        [MenuItem("Assets/Create/Level List", false, 0)]
        public static void CreateLevelList()
        {
            AssetDatabase.DeleteAsset("Assets/Levels");
            AssetDatabase.CreateFolder("Assets", "Levels");
            AssetDatabase.SaveAssets();

            var list = ScriptableObject.CreateInstance<LevelList>();
            list.Levels = new List<Level>();

            var lines = File.ReadAllLines(@"..\Data\Levels.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                var level = ScriptableObject.CreateInstance<Level>();
                level.Tiles = new List<Vector2>();

                var line = lines[i];
                try
                {
                    var tileSplit = line.Split();
                    foreach (var tile in tileSplit)
                    {
                        var pointSplit = tile.Split(',');
                        var x = int.Parse(pointSplit[0]);
                        var y = int.Parse(pointSplit[1]);
                        var vector = new Vector2(x, y);

                        level.Tiles.Add(vector);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    Debug.Log(line);
                }

                AssetDatabase.CreateAsset(level, $"Assets/Levels/Level{i + 1}.asset");
                AssetDatabase.SaveAssets();

                list.Levels.Add(level);
            }

            AssetDatabase.CreateAsset(list, "Assets/Levels/LevelList.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
