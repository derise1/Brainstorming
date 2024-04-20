using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI correctAnswer;
    [SerializeField] private Button button_menu;

    private Scene_Loader scene_Loader;

    private void Awake()
    {
        scene_Loader = Scene_Loader.Instance;
        button_menu.onClick.AddListener(scene_Loader.LoadNextScene);
    }

    public void ShowEndPanel(int correct)
    {
        gameObject.SetActive(true);
        correctAnswer.text = correct.ToString();
    }
}
