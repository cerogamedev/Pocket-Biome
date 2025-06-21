using UnityEngine;

public enum DietType { Herbivore /*, Carnivore*/ }

[CreateAssetMenu(fileName = "AnimalSpecies", menuName = "PocketBiome/Animal Species")]
public class AnimalSpeciesData : ScriptableObject
{
    [Header("Display")]
    public string speciesName;
    public Sprite icon;

    [Header("Gameplay")]
    public DietType diet = DietType.Herbivore;
    [Range(0, 8)] public int startingHunger  = 3;   // Tur sayısı
    [Range(0f, 1f)] public float birthChance = 0.2f;
    public PlantSpeciesData favouritePlant;         // Şimdilik tek bitki
    public Animal prefab;                           // Atılacak prefab
}
