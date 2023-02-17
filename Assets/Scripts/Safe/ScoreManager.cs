using System.IO;
using UnityEngine;

namespace Safe
{
    public class ScoreManager
    {
        private const string saveFileName = "player_score.txt";

        public static void SaveScore(ulong score)
        {
            string saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
            File.WriteAllText(saveFilePath, score.ToString());
        }

        public static ulong LoadScore()
        {
            string saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(saveFilePath))
            {
                string savedScore = File.ReadAllText(saveFilePath);
                ulong result;
                if (ulong.TryParse(savedScore, out result))
                {
                    return result;
                }
            }
            return 0;
        }
    }
}

