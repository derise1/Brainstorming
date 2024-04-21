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

    private TextMeshProUGUI _buttonText;
    private Image _buttonImage;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnClickButtonNext);

        _buttonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
        _buttonImage = nextButton.GetComponentInChildren<Image>();
    }

    public void ShowCorrectPanel(bool correct)
    {
        correctBackground.color = correct ? correctColor : uncorrectColor;
        correctText.text = correct ? correctTextField : uncorrectTextField;
        gameObject.SetActive(true);
        
        DOTween.Sequence()
            .Append(correctBackground.DOFade(0.5f, 0.6f))
            .Join(correctText.DOFade(1f, 0.6f))
            .Join(_buttonText.DOFade(1f, 0.6f))
            .Join(_buttonImage.DOFade(1f, 0.6f))
            .Play();
    }

    public void OnClickButtonNext()
    {
        DOTween.Sequence()
            .Append(correctBackground.DOFade(0f, 0.6f))
            .Join(correctText.DOFade(0f, 0.6f))
            .Join(_buttonText.DOFade(0f, 0.6f))
            .Join(_buttonImage.DOFade(0f, 0.6f))
            .OnComplete(() => {
                correctBackground.color = Color.white;
                correctText.text = "";
                gameObject.SetActive(false);
            })
            .Play();
    }
}
