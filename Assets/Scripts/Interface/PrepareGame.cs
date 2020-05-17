namespace Assets.Scripts.Interface
{
    public class PrepareGame : BaseInterface
    {
        public static PrepareGame Instance;

        public void Awake()
        {
            Instance = this;
        }
    }
}