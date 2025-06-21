using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private GridManager grid;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public int CalculateScore(out int diversity, out int population)
    {
        var speciesSet = new System.Collections.Generic.HashSet<string>();
        population = 0;

        foreach (Cell c in grid.AllCells)
        {
            if (c.HasPlant)
            {
                speciesSet.Add(c.CurrentPlant.Data.name);
                population++;
            }
            if (c.HasAnimal)
            {
                speciesSet.Add(c.CurrentAnimal.Data.name);
                population++;
            }
        }
        diversity = speciesSet.Count;
        return population * diversity;
    }
}
