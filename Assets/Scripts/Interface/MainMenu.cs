namespace Assets.Scripts.Interface
{
    public class MainMenu : BaseInterface
    {
        public static MainMenu Instance;

        public void Awake()
        {
            Instance = this;
        }
    }
}
