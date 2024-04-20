using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Loader : Singleton<Scene_Loader>
{
    [SerializeField] private float transitionEffectTime = 1f;
    [SerializeField] private Image transitionImage;

    public void LoadNextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount)
        {
            LoadNextSceneAsync(0).Forget();
        }
        else
        {
            LoadNextSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).Forget();
        }
    }

    private async UniTaskVoid LoadNextSceneAsync(int sceneIndex)
    {
        TransitionEffect(true);
        await UniTask.WaitForSeconds(transitionEffectTime);
        await SceneManager.LoadSceneAsync(sceneIndex);
        TransitionEffect(false);
    }

    private void TransitionEffect(bool value)
    {
        transitionImage.raycastTarget = true;
        DOTween.Sequence()
            .Append(value ? transitionImage.DOFade(1f, 0.5f) : transitionImage.DOFade(0f, 0.5f))
            .Play();
        transitionImage.raycastTarget = false;
    }
}