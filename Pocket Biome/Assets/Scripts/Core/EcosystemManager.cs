using UnityEngine;
using System.Collections.Generic;

public class EcosystemManager : MonoBehaviour
{
    public static EcosystemManager Instance { get; private set; }

    [SerializeField] private List<PlantSpeciesData> plantSpecies;
    [SerializeField] private AdjacentSpread spreadAlgorithm = new();  // Strategy

    [SerializeField] private List<AnimalSpeciesData> animalSpecies;

    private GridManager _grid;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Init(GridManager grid) => _grid = grid;
    public void ProcessPlantGrowth()
    {
        foreach (Cell cell in _grid.AllCells)
        {
            if (cell.HasPlant)
            {
                spreadAlgorithm.Spread(_grid, cell, cell.CurrentPlant.Data);
            }
            else if (cell.IsMoist())        
            {
                PlantSpeciesData species = plantSpecies[0]; 
                float seasonMult = SeasonManager.Instance.Current.growthMult;
                float mutMult = 1f;    // default
                if (MutationManager.Instance != null)
                    mutMult = MutationManager.Instance.GetMultiplier(MutationType.SpreadBoost);
                float chance = species.spreadChance * seasonMult * mutMult;

                if (Random.value < chance)
                    cell.PlacePlant(species);
            }
        }
    }
    public void ProcessAnimals()
    {
        foreach (Cell cell in _grid.AllCells)
        {
            if (!cell.HasAnimal && cell.HasPlant)
            {
                AnimalSpeciesData animal = animalSpecies[0];
                float mutBirth = 1f;            // varsayılan
                if (MutationManager.Instance != null)
                    mutBirth = MutationManager.Instance.GetMultiplier(MutationType.BirthBoost);

                float birth = animal.birthChance * SeasonManager.Instance.Current.birthMult * mutBirth;
                if (Random.value < birth)
                    cell.PlaceAnimal(animal);
            }

            // Hayvan turu
            if (cell.HasAnimal)
                cell.CurrentAnimal.Tick();
        }
    }


}
