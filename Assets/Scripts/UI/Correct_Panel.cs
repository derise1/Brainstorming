using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Correct_Panel : MonoBehaviour
{
    [Header("Text Variables")]
    [SerializeField] private TextMeshProUGUI correctText;
    [SerializeField] private string correctTextField;
    [SerializeField] private string uncorrectTextField;

    [Header("UI Color Background")]
    [SerializeField] private Image correctBackground;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color uncorrectColor;

    [SerializeField] private Button nextButton;

    public Button NextButton => nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnClickButtonNext);
    }

    public void ShowCorrectPanel(bool correct)
    {
        correctBackground.color = correct ? correctColor : uncorrectColor;
        correctText.text = correct ? correctTextField : uncorrectTextField;
        gameObject.SetActive(true);
        DOTween.Sequence()
            .Append(correctBackground.DOFade(0.5f, 0.7f))
            .Join(correctText.DOFade(1f, 0.7f))
            .Play();
    }

    public void OnClickButtonNext()
    {
        DOTween.Sequence()
            .Append(nextButton.transform.DOScale(0.9f, 0.2f))
            .OnComplete(() => { 
                nextButton.transform.DOScale(1f, 0.2f); 
                correctBackground.DOFade(0, 0.7f);
                correctText.DOFade(0f, 0.7f);
            })
            .OnComplete(() => {
                correctBackground.color = Color.white;
                correctText.text = "";
                gameObject.SetActive(false);
            })
            .Play();
        gameObject.SetActive(false);
    }
}
