using UnityEngine;

[System.Serializable]
public class AdjacentSpread : ISpreadStrategy
{
    private static readonly Vector2Int[] _dirs =
    {
        new(1,0), new(-1,0), new(0,1), new(0,-1)
    };

    public void Spread(GridManager grid, Cell origin, PlantSpeciesData species)
    {
        foreach (var dir in _dirs)
        {
            float chance = species.spreadChance * SeasonManager.Instance.Current.growthMult;
            if (Random.value > chance) continue;

            Cell target = grid.GetCell(origin, dir);
            if (target != null && !target.HasPlant)
            {
                target.PlacePlant(species);
            }
        }
    }
}
