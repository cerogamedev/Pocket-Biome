using UnityEngine;
using System.Collections.Generic;

public class MutationManager : MonoBehaviour
{
    public static MutationManager Instance { get; private set; }

    [SerializeField] private List<MutationData> library;

    // Aktif mutasyonlar
    private readonly List<MutationData> _active = new();

    public bool HasMutation(MutationType type) =>
        _active.Exists(m => m.type == type);

    public float GetMultiplier(MutationType type)
    {
        float mult = 1f;
        foreach (var m in _active)
            if (m.type == type) mult *= m.value;
        return mult;
    }

    public void AddMutation(MutationData data) => _active.Add(data);

    // UI Popup isteği
    public MutationData[] GetRandomChoices(int count = 3)
    {
        var list = new List<MutationData>();
        while (list.Count < count)
        {
            var pick = library[Random.Range(0, library.Count)];
            if (!list.Contains(pick)) list.Add(pick);
        }
        return list.ToArray();
    }
}
