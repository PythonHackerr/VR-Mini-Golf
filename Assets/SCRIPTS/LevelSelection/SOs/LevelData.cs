using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int shotCount; //maximum shot player can take

    public bool isCompleted;
    public float completionTime;
    public int completionShotsTaken;
}
