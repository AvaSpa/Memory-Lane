using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Camera Camera;
    public RotateOnSwipe SwipeRotator;
    public ScrollOnSwipe SwipeScroller;
    public SceneChanger SceneChanger;

    private const string CurrentLevelKey = "CurrentLevel";

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        RaycastHit hit;
        var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var objectHit = hit.transform;

            ActOnObject(objectHit);
        }
    }

    private void ActOnObject(Transform objectHit)
    {
        var tag = objectHit.tag;
        if (string.IsNullOrEmpty(tag) || SwipeRotator.IsRotating || SwipeScroller.IsScrolling) return;

        switch (tag)
        {
            case "Play":
                Play(objectHit);
                break;
            case "Level":
                LoadLevel(objectHit);
                break;
        }
    }

    private void LoadLevel(Transform objectHit)
    {
        var tagger = objectHit.GetComponent<NumberTagger>();
        if (!tagger.ClickEnabled) return;
        PlayerPrefs.SetInt(CurrentLevelKey, tagger.Number + 1);
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
    }

    private void Play(Transform objectHit)
    {
        var continueScript = objectHit.GetComponent<ContinueButtonHandler>();
        continueScript?.Continue();
    }
}
