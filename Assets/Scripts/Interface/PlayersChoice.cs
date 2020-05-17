namespace Assets.Scripts.Interface
{
    public class PlayersChoice : BaseInterface
    {
        public static PlayersChoice Instance;

        public void Awake()
        {
            Instance = this;
        }
    }
}