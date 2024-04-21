using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    private const string JSON_FILE_NAME = "quiz_data.json";
    private string pathToFile = Application.streamingAssetsPath + "/" + JSON_FILE_NAME;
    
    [SerializeField] private TextMeshProUGUI currentCountQustion;
    [SerializeField] private TextMeshProUGUI allCountQuestion;
    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private Button_Answer buttonPrefab;
    [SerializeField] private GameObject containerQuestion;
    [SerializeField] private GameObject containerButton;
    [SerializeField] private Image backgroundQuestion;

    [SerializeField] private Correct_Panel correctPanel;
    [SerializeField] private EndGamePanel endGamePanel;

    private List<Question> questionsList = new ();
    private int currentIndexQuestion = -1;

    private int allCountCorrectAnswer = 0;
    private int currentCorrectAnswer = -1;

    private void Awake()
    {
        containerQuestion.SetActive(true);
        correctPanel.NextButton.onClick.AddListener(NextQuestion);

        questionsList = JsonReader.FromJson<Question>(pathToFile);

        allCountQuestion.text = questionsList.Count.ToString();

        NextQuestion();
    }

    public void CheckCorrectAnswer(Button_Answer buttonAnswer)
    {
        if(!buttonAnswer.Answer.correct)
        {
            ShowCorrectPanel(buttonAnswer);
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
                        ShowCorrectPanel(buttonAnswer);
                    }
                }
            }
            else
            {
                allCountCorrectAnswer++;
                ShowCorrectPanel(buttonAnswer);
            }
        }
    }

    private void NextQuestion()
    {
        currentIndexQuestion++;

        currentCountQustion.text = (currentIndexQuestion + 1).ToString();
        questionText.text = questionsList[currentIndexQuestion].question;

        LoadBackground(questionsList[currentIndexQuestion].background);

        CreateButtonAnswer();
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

    private void ClearQuestion(Answer answer)
    {
        questionText.text = "";
        
        if(containerButton.transform.childCount != 0)
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {   
                    foreach(RectTransform child in containerButton.transform)
                    {
                        child.DOScale(0.1f, 0.7f).SetEase(Ease.InSine).OnComplete(() => {
                            Destroy(child.gameObject);
                        });
                    }
                })
                .OnComplete(() => correctPanel.ShowCorrectPanel(answer.correct))
                .Play();
        }
    }

    private void ShowCorrectPanel(Button_Answer buttonAnswer)
    {
        if(currentIndexQuestion >= questionsList.Count - 1)
        {
            containerQuestion.SetActive(false);
            endGamePanel.ShowEndPanel(allCountCorrectAnswer);
            backgroundQuestion.sprite = null;
        }
        else
        {
            ClearQuestion(buttonAnswer.Answer);
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