using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Answer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI textAnswer;
    private Answer _answer;
    private QuizManager _quizManager;
    private bool _isClick = false;

    public Answer Answer => _answer;
    public bool IsClick => _isClick;

    public void SetAnswer(Answer answer, QuizManager quizManager)
    {
        _answer = answer;
        textAnswer.text = answer.text;
        _quizManager = quizManager;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _quizManager.CheckCorrectAnswer(this);
        _isClick = true;
    }
}
