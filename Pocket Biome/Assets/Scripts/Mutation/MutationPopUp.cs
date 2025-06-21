using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MutationPopup : MonoBehaviour
{
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private MutationManager mutationManager;

    private void Awake()
    {
        foreach (var btn in choiceButtons)
            btn.onClick.AddListener(() => Select(btn));
    }

    private void Select(Button btn)
    {
        var data = btn.GetComponent<MutationReference>().Data;
        mutationManager.AddMutation(data);
        UIManager ui = FindFirstObjectByType<UIManager>();
        ui.ReEnableNextTurn(); 
        gameObject.SetActive(false);
    }

    public void Show(MutationData[] choices)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            var refComp = choiceButtons[i].GetComponent<MutationReference>();
            refComp.Data = choices[i];
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].mutationName;
            //choiceButtons[i].image.sprite = choices[i].icon;
        }
    }
}

// yardımcı component

