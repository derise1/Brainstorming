using System;
using UnityEngine;

[Serializable]
public class Answer
{
    [SerializeField] private string text;
    [SerializeField] private bool correct;

    public string Text => text;
    public bool IsCorrect => correct;
}