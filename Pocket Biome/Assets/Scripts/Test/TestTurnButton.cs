// TestTurnButton.cs
using UnityEngine;
public class TestTurnButton : MonoBehaviour
{
    [SerializeField] private GridManager gm;
    void OnGUI()
    {
        if (GUILayout.Button("Advance Turn")) gm.AdvanceTurn();
    }
}
