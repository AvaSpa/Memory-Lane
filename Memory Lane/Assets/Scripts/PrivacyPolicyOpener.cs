using UnityEngine;

public class PrivacyPolicyOpener : MonoBehaviour
{
    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/1M4Gd8gYxvBLJelFrnDduq6yh1UXJiD5OB8IIm5BzGjQ/edit?usp=sharing");
    }
}
