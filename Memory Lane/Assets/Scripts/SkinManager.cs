using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public GameObject[] ListItems;
    public Transform ButtonContainer;
    public GameObject SkinButtonPrefab;

    private void Start()
    {
        const int buttonOffset = 20;

        for (var i = 0; i < ListItems.Length; i++)
        {
            var listItem = GameObject.Instantiate(SkinButtonPrefab, ButtonContainer, false);
            var position = listItem.transform.position;
            //TODO: tweak position and offset
            listItem.transform.position = new Vector3(position.x, position.y + buttonOffset - i * 10, position.z);

            var skinButtonHandler = listItem.GetComponent<SkinButtonHandler>();
            skinButtonHandler.SkinId = i;
            skinButtonHandler.PossibleSkins = ListItems;

            SetVisibility(listItem, i);
        }
    }

    private void SetVisibility(GameObject listItem, int index)
    {
        var enabler = GetComponentInChildren<ButtonEnabler>();

        if (index > 4) enabler.SetEnabled(listItem, false);
    }
}
