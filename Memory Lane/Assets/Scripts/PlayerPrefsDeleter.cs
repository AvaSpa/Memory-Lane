using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class PlayerPrefsDeleter : MonoBehaviour
    {
        public SceneChanger SceneChanger;

        public void Clear()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKeys.CurrentLevelKey);
            PlayerPrefs.DeleteKey(PlayerPrefsKeys.MaxLevelKey);
            PlayerPrefs.DeleteKey(PlayerPrefsKeys.CurrentSkinKey);

            SceneChanger.FadeToScene(Enums.SceneIdentity.Menu);
        }
    }
}