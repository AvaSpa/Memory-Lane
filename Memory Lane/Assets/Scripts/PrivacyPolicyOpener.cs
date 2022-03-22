using UnityEngine;

public class PrivacyPolicyOpener : MonoBehaviour
{
    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://avaspa.azurewebsites.net/privacypolicy.html");
    }
}
