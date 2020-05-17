namespace Assets.Scripts.Interface
{
    public class MainPlayPanel : BaseInterface
    {
        public static MainPlayPanel Instance;

        public void Awake()
        {
            Instance = this;
        }
    }
}