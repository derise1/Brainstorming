using System;
using System.Collections.Generic;

[Serializable]
public class Question
{
    public string question {get; set;}
    public List<Answer> answers {get; set;}
    public string background {get; set;}
}
