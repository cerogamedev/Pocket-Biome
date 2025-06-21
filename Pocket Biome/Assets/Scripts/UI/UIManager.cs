using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Button nextTurnBtn;
    [SerializeField] private GridManager grid;
    [SerializeField] private TextMeshProUGUI seasonText;
    [SerializeField] private EndGamePanel endPanel;
    [SerializeField] private MutationManager mutationManager;

    [SerializeField] private MutationPopup mutationPopup;
    private readonly int[] _mutationTurns = {5, 15, 25};

    private int _turn = 0, _maxTurn = 30;

    private void Awake()
    {
        SeasonManager.Instance.OnSeasonChanged += s => seasonText.text = s.seasonName;
        nextTurnBtn.onClick.AddListener(OnNextTurn);
        UpdateLabel();
    }

    private void OnNextTurn()
    {
        if (_turn >= _maxTurn) return;

        _turn++;
        grid.AdvanceTurn();
        SeasonManager.Instance.TickTurn();
        UpdateLabel();

        if (System.Array.Exists(_mutationTurns, t => t == _turn))
        {
            var choices = mutationManager.GetRandomChoices();
            mutationPopup.Show(choices);
            nextTurnBtn.interactable = false; 
            mutationPopup.GetComponent<MutationPopup>().gameObject
                .SetActive(true);

            mutationPopup.gameObject.SetActive(true);
            mutationPopup.GetComponent<MutationPopup>()
                         .Show(choices);

            mutationPopup.gameObject
                .GetComponent<MutationPopup>()
                .Show(choices);
            mutationPopup.GetComponent<MutationPopup>()
                         .Show(choices);
        }
    }


    private void UpdateLabel()
        => turnText.text = $"Turn {_turn}/{_maxTurn}";
    public void ReEnableNextTurn() => nextTurnBtn.interactable = true;

    
}
