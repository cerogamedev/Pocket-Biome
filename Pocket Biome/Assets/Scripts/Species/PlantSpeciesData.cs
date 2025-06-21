using UnityEngine;

[CreateAssetMenu(fileName = "PlantSpecies", menuName = "PocketBiome/Plant Species")]
public class PlantSpeciesData : ScriptableObject
{
    [Header("Display")]
    public string speciesName;
    public Sprite icon;

    [Header("Gameplay")]
    [Range(0f, 1f)] public float spreadChance = 0.3f;   // Hücre başı
    public int turnsToMature = 2;                       // İleri kullanım için
    [Tooltip("Prefab with Plant component")]
    public Plant plantPrefab;
}
