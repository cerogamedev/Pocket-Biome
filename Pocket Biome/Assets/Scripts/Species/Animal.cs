// Animal.cs  (/Scripts/Species)
using UnityEngine;
using DG.Tweening;

public class Animal : MonoBehaviour
{
    public AnimalSpeciesData Data { get; private set; }
    private Cell _host;
    private int _hunger;
    void Start()
    {
        transform.localScale = new(0, 0, 0);
        transform.DOScale(1, 1f);
    }

    public void Init(AnimalSpeciesData data, Cell host)
    {
        
        Data = data;
        _host = host;
        _hunger = data.startingHunger;
    }

    public void Tick()   // Her tur EcosystemManager çağırır
    {
        // Yeme
        if (_host.HasPlant &&
            _host.CurrentPlant.Data == Data.favouritePlant)
        {
            Destroy(_host.CurrentPlant.gameObject);
            _host.ClearPlant();              // (fonksiyonu birazdan ekleyeceğiz)
            _hunger = Data.startingHunger;   // Karnını doyurdu
        }
        else
        {
            _hunger--;
            if (_hunger <= 0)
            {
                _host.RemoveAnimal();        // Cell fonksiyonu
                Destroy(gameObject);
            }
        }
    }
}
