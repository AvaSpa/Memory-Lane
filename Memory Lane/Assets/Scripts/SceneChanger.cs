using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator Animator;

    private SceneIdentity _sceneToLoad;

    public void FadeToScene(SceneIdentity scene)
    {
        _sceneToLoad = scene;
        Animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene((int)_sceneToLoad);
    }
}
