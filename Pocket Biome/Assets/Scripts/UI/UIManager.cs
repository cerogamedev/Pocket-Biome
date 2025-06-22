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
    _turn++;
    grid.AdvanceTurn();
    SeasonManager.Instance.TickTurn();
    UpdateLabel();

    // POPUP / MUTATION önce – oyun bitmediyse
    if (_turn < _maxTurn &&
        System.Array.Exists(_mutationTurns, t => t == _turn))
    {
        var choices = mutationManager.GetRandomChoices();
        mutationPopup.Show(choices);
        nextTurnBtn.interactable = false;
        return;                                  // popup kapatılınca buton açılacak
    }

    if (_turn >= _maxTurn)
    {
        nextTurnBtn.interactable = false;
        int pop, div;
        int score = ScoreManager.Instance.CalculateScore(out div, out pop);
        endPanel.Show(score, pop, div);          // ← panel açılır
    }
}



    private void UpdateLabel()
        => turnText.text = $"Turn {_turn}/{_maxTurn}";
    public void ReEnableNextTurn() => nextTurnBtn.interactable = true;

    
}
