using System.Collections.Generic;
using UnityEngine;

namespace QuizGame.Service
{
    public class QuestionsService
    {
        private const string JSON_FILE_NAME = "quiz_data.json";
        private string pathToFile = Application.streamingAssetsPath + "/" + JSON_FILE_NAME;

        private readonly List<Question> _questions = new();

        public IReadOnlyCollection<Question> Questions => _questions;
        
        public QuestionsService()
        {
            _questions = JsonReader.FromJson<Question>(pathToFile);
        }
    }
}