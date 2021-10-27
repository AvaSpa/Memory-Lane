using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Camera Camera;

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
        if (string.IsNullOrEmpty(tag)) return;

        if (tag == "Play")
            Play(objectHit);
        else
        {
            //TODO: Get level number from generated tag and load it just like it was loaded from 2D UI
        }
    }

    private void Play(Transform objectHit)
    {
        var continueScript = objectHit.GetComponent<ContinueButtonHandler>();
        continueScript?.Continue();
    }
}
