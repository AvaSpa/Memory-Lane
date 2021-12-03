public class ContinueButtonHandler : ClickAction
{
    public SceneChanger SceneChanger;

    private void Continue()
    {
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
    }

    protected override void Act()
    {
        Continue();
    }
}
