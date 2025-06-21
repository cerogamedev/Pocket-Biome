// Plant.cs   (Scripts/Species)
using UnityEngine;

public class Plant : MonoBehaviour
{
    public PlantSpeciesData Data { get; private set; }
    private Cell _parentCell;

    public void Init(PlantSpeciesData data, Cell host)
    {
        Data = data;
        _parentCell = host;
    }
}
