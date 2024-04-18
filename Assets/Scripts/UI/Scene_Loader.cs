using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader : Singleton<Scene_Loader>
{
    public void LoadNextScene()
    {
        LoadNextSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).Forget();
    }

    private async UniTaskVoid LoadNextSceneAsync(int sceneIndex)
    {
        await UniTask.WaitUntil(() => TransitionEffect());
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    private bool TransitionEffect()
    {
        // DOTween.Sequence()
        //     .Append()
        //     .Play();

        return true;
    }
}
