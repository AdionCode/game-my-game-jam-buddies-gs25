using UnityEngine;

[System.Serializable]
public class GameJamData
{
    public string jamName;
    public float durationInSeconds;
    [Range(0f, 1f)] public float difficulty = 0.1f;
    public int Exp;
    public int Money;
}
