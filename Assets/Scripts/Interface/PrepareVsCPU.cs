namespace Assets.Scripts.Interface
{
    public class PrepareVsCPU : BaseInterface
    {
        public static PrepareVsCPU Instance;

        public void Awake()
        {
            Instance = this;
        }
    }
}