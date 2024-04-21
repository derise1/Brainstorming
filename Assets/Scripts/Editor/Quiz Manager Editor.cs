using UnityEditor;

[CustomEditor(typeof(QuizManager))]
public class QuizManagerEditor : Editor
{
    private SerializedProperty currentCountQustion;
    private SerializedProperty allCountQuestion;
    private SerializedProperty questionText;

    private SerializedProperty buttonPrefab;
    private SerializedProperty containerQuestion;
    private SerializedProperty containerButton;
    private SerializedProperty backgroundQuestion;

    private SerializedProperty correctPanel;
    private SerializedProperty endGamePanel;

    private QuizManager quizManager;

    private void OnEnable()
    {
        if(quizManager != null)
        {
            currentCountQustion = serializedObject.FindProperty("currentCountQustion");
            allCountQuestion = serializedObject.FindProperty("allCountQuestion");
            questionText = serializedObject.FindProperty("questionText");

            buttonPrefab = serializedObject.FindProperty("buttonPrefab");
            containerQuestion = serializedObject.FindProperty("containerQuestion");
            containerButton = serializedObject.FindProperty("containerButton");
            backgroundQuestion = serializedObject.FindProperty("backgroundQuestion");

            correctPanel = serializedObject.FindProperty("correctPanel");
            endGamePanel = serializedObject.FindProperty("endGamePanel");

            quizManager = target as QuizManager;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("TEXT REFERENCES", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(currentCountQustion);
            EditorGUILayout.PropertyField(allCountQuestion);
            EditorGUILayout.PropertyField(questionText);

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("OBJECT REFERENCES", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(buttonPrefab);
            EditorGUILayout.PropertyField(containerQuestion);
            EditorGUILayout.PropertyField(containerButton);
            EditorGUILayout.PropertyField(backgroundQuestion);

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("PANEL REFERENCES", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(correctPanel);
            EditorGUILayout.PropertyField(endGamePanel);

        serializedObject.ApplyModifiedProperties();
    }
}