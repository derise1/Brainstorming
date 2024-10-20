using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question
{
    [SerializeField] private string question;
    [SerializeField] private List<Answer> answers;
    [SerializeField] private string background;

    public string NameQuestion => question;
    public IReadOnlyList<Answer> Answers => answers;
    public string BackgroundPath => background;
}