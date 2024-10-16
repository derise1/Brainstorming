using System;
using System.Collections.Generic;

[Serializable]
public class Question
{
    public string question;
    public List<Answer> answers;
    public string background;
}