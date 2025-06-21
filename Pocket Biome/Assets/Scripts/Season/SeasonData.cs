using UnityEngine;

[CreateAssetMenu(fileName="Season", menuName="PocketBiome/Season")]
public class SeasonData : ScriptableObject
{
    [Header("Display")]
    public string seasonName;
    public Color backgroundTint = Color.white;
    public Sprite icon;

    [Header("Rules")]
    public int durationTurns = 8;          
    [Range(0f,1f)] public float growthMult = 1f;   
    [Range(0f,1f)] public float birthMult  = 1f;   
    public bool freezesMoist = false;     
}
