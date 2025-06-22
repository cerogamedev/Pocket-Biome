using UnityEngine;
using DG.Tweening;

public enum CellSpriteType { Dry, Moist, Frozen }

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite drySprite;
    [SerializeField] private Sprite moistSprite;

    private ICellState _currentState;

    public bool IsMoist() => _currentState is MoistState;

    public Animal CurrentAnimal { get; private set; }
    public bool HasAnimal => CurrentAnimal != null;


    private SpriteRenderer _sr;

    public Plant CurrentPlant { get; private set; }

    public bool HasPlant => CurrentPlant != null;

    public void ClearPlant() => CurrentPlant = null;
    public void RemoveAnimal() => CurrentAnimal = null;

    public void PlaceAnimal(AnimalSpeciesData species)
    {
        if (HasAnimal) return;

        Animal newAni = Instantiate(species.prefab, transform);
        newAni.transform.localPosition = Vector3.zero;
        newAni.Init(species, this);
        CurrentAnimal = newAni;
    }

    public void PlacePlant(PlantSpeciesData species)
    {
        if (HasPlant) return;

        Plant newPlant = Instantiate(species.plantPrefab, transform);
        newPlant.transform.localPosition = Vector3.zero;  // Hücrenin ortası
        newPlant.Init(species, this);
        CurrentPlant = newPlant;
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        ChangeState(new DryState());       // Başlangıç
    }

    public void ChangeState(ICellState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void AdvanceTurn() => _currentState.Tick(this);

    public void SetSprite(CellSpriteType type)
    {
        _sr.sprite = type switch
        {
            CellSpriteType.Dry => drySprite,
            CellSpriteType.Moist => moistSprite,
            _ => _sr.sprite
        };
    }

    // Geçici: Mouse ile sulama
    private void OnMouseDown()
    {
        if (_currentState is DryState)
            ChangeState(new MoistState());
    }
}
