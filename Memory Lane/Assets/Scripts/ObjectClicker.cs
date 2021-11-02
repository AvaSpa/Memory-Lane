using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Camera Camera;
    public RotateOnSwipe SwipeRotator;

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
        if (string.IsNullOrEmpty(tag) || SwipeRotator.IsRotating) return;

        if (tag == "Play")
            Play(objectHit);
        else
        {
            //TODO: Get level number from generated tag and load it just like it was loaded from 2D UI
            //PlayerPrefs.SetInt(CurrentLevelKey, buttonLevel);
            //SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
        }
    }

    private void Play(Transform objectHit)
    {
        var continueScript = objectHit.GetComponent<ContinueButtonHandler>();
        continueScript?.Continue();
    }
}
