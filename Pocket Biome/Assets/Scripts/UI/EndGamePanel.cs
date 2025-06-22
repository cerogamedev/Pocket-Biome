using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, popText, divText;
    [SerializeField] private Button restartBtn;
    [SerializeField] private GameObject Canva;

    private void Awake()
    {
        restartBtn.onClick.AddListener(() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void Show(int score, int pop, int div)
    {
        scoreText.text = $"Score: {score}";
        popText.text   = $"Population: {pop}";
        divText.text   = $"Divinity: {div}";
        Canva.SetActive(true);
    }
}
