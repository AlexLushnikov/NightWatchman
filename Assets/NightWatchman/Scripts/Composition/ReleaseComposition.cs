namespace Common
{
    public class ReleaseComposition : IComposition
    {
        public void Destroy()
        {
            DisposeAll();
        }

        private void DisposeAll()
        {

        }
    }
}