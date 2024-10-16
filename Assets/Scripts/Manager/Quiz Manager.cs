using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using QuizGame.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Header("TEXT REFERENCES")]
    [SerializeField] private TextMeshProUGUI currentCountQustion;
    [SerializeField] private TextMeshProUGUI allCountQuestion;
    [SerializeField] private TextMeshProUGUI questionText;

    [Header("OBJECT REFERENCES")]
    [SerializeField] private Button_Answer buttonPrefab;
    [SerializeField] private GameObject containerQuestion;
    [SerializeField] private GameObject containerButton;
    [SerializeField] private Image backgroundQuestion;

    [Header("PANEL REFERENCES")]
    [SerializeField] private Correct_Panel correctPanel;
    [SerializeField] private EndGamePanel endGamePanel;

    private QuestionsService _questionsService;
    
    private List<Question> questionsList;
    private int currentIndexQuestion = -1;

    private int allCountCorrectAnswer = 0;
    private int currentCorrectAnswer = -1;

    private void Awake()
    {
        _questionsService = new QuestionsService();
        questionsList = new(_questionsService.Questions);
        
        containerQuestion.SetActive(true);
        correctPanel.NextButton.onClick.AddListener(NextQuestion);
        
        allCountQuestion.text = questionsList.Count.ToString();
        NextQuestion();
    }

    public void CheckCorrectAnswer(Button_Answer buttonAnswer)
    {
        if(!buttonAnswer.Answer.correct)
        {
            ClearQuestion(buttonAnswer);
        }
        else
        {
            if(CheckCountCorrectAnswer())
            {
                if(!buttonAnswer.IsClick)
                {
                    currentCorrectAnswer--;
                
                    if(currentCorrectAnswer == 0)
                    {
                        allCountCorrectAnswer++;
                        ClearQuestion(buttonAnswer);
                    }
                }
            }
            else
            {
                allCountCorrectAnswer++;
                ClearQuestion(buttonAnswer);
            }
        }
    }

    private void NextQuestion()
    {
        if(currentIndexQuestion >= questionsList.Count - 1)
        {
            containerQuestion.SetActive(false);
            endGamePanel.ShowEndPanel(allCountCorrectAnswer);
            backgroundQuestion.sprite = null;
        }
        else
        {
            currentIndexQuestion++;

            currentCountQustion.text = (currentIndexQuestion + 1).ToString();
            questionText.text = questionsList[currentIndexQuestion].question;

            LoadBackground(questionsList[currentIndexQuestion].background);

            CreateButtonAnswer();
        }
    }

    private void CreateButtonAnswer()
    {
        if(questionsList[currentIndexQuestion].answers.Count > 1)
        {
            questionsList[currentIndexQuestion].answers.Shuffle();
        }

        foreach(var answer in questionsList[currentIndexQuestion].answers)
        {
            Button_Answer button_Answer = Instantiate(buttonPrefab, containerButton.transform);
            button_Answer.SetAnswer(answer, this);
        }

        if(CheckCountCorrectAnswer())
        {
            currentCorrectAnswer = GetCountCorrectAnswer();
        }
        else
        {
            currentCorrectAnswer = -1;
        }
    }

    private void ClearQuestion(Button_Answer buttonAnswer)
    {
        questionText.text = "";
        
        if(containerButton.transform.childCount != 0)
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {   
                    foreach(RectTransform child in containerButton.transform)
                    {
                        child.DOScale(0f, 0.5f).OnComplete(() => {
                            Destroy(child.gameObject);
                        });
                    }
                })
                .OnComplete(() => correctPanel.ShowCorrectPanel(buttonAnswer.Answer.correct))
                .Play();
        }
    }

    private void LoadBackground(string backgroundPath)
    {
        if(string.IsNullOrEmpty(backgroundPath))
        {
            backgroundQuestion.sprite = null;
        }
        else
        {
            try
            {
                string path = Path.Combine(Application.streamingAssetsPath, backgroundPath);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(File.ReadAllBytes(path));
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                backgroundQuestion.sprite = sprite;
            }
            catch (Exception e)
            {
                Debug.LogError("Error for load background from path " + e.Message);
            }
        }
    }

    private int GetCountCorrectAnswer()
    {
        return questionsList[currentIndexQuestion].answers.Count(answer => answer.correct == true);
    }

    private bool CheckCountCorrectAnswer()
    {
        return GetCountCorrectAnswer() > 1;
    }
}