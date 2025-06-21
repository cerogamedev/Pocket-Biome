using UnityEngine;
using System;

public class SeasonManager : MonoBehaviour
{
    public static SeasonManager Instance { get; private set; }

    [SerializeField] private SeasonData[] seasons;   // Inspector’da sırayla Spring→...
    [SerializeField] private Camera mainCam;         // Arka plan rengi için
    public SeasonData Current { get; private set; }

    public event Action<SeasonData> OnSeasonChanged;

    private int _index = 0;
    private int _turnsIntoSeason = 0;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        SetSeason(0);    // İlkbahar
    }

    public void TickTurn()
    {
        _turnsIntoSeason++;
        if (_turnsIntoSeason >= Current.durationTurns)
        {
            _index = (_index + 1) % seasons.Length;
            SetSeason(_index);
        }
    }

    private void SetSeason(int idx)
    {
        Current = seasons[idx];
        _turnsIntoSeason = 0;

        // Basit arka plan tint
        if (mainCam != null) mainCam.backgroundColor = Current.backgroundTint;

        OnSeasonChanged?.Invoke(Current);
    }
}
