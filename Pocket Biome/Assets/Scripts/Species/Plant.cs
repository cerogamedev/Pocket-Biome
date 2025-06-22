// Plant.cs   (Scripts/Species)
using UnityEngine;
using DG.Tweening;

public class Plant : MonoBehaviour
{
    public PlantSpeciesData Data { get; private set; }
    private Cell _parentCell;
    void Start()
    {
        transform.localScale = new (0, 0, 0);
        transform.DOScale(1, 1f);
    }
    public void Init(PlantSpeciesData data, Cell host)
    {
        
        Data = data;
        _parentCell = host;
    }
}
