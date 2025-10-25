// using System;
// using System.IO;
// using System.Text.Json;
// using System.Collections.Generic;
// using UnityEngine; 

// public class PuzzleLoader : MonoBehaviour
// {
//     public List<PuzzleData> puzzles;

//     void Start()
//     {
//         PuzzleManager();
//     }

//     void LoadPuzzles()
//     {
//         try
//         {
//             string path = Path.Combine(Application.streamingAssetsPath, "qiuze.json");


//             if (!File.Exists(path))
//             {
//                 Debug.LogError($"Файл не знайдено: {path}");
//                 return;
//             }

//             string json = File.ReadAllText(path);
//             //puzzles = JsonSerializer.Deserialize<List<PuzzleData>>(json);

//             //Debug.Log($" Завантажено {puzzles.Count} головоломок.");
//         }
//         catch (Exception ex)
//         {
//             Debug.LogError("Помилка читання qiuze.json"+ ex.Message);
//         }
//     }

//     public class PuzzleManager : MonoBehaviour
//     {
//         public Transform puzzlesParent;
//         public GameObject puzzlePrefab; 

//         void Start()
//         {
//             var loader = GetComponent<PuzzleLoader>();
//             foreach (var p in loader.puzzles)
//             {
//                 var panel = Instantiate(puzzlePrefab, puzzlesParent);
//                 var ui = panel.GetComponent<PuzzleUI>(); // твій скрипт для відображення
//                 ui.SetPuzzle(p);
//             }
//         }
//     }

// }
