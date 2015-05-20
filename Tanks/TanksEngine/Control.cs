namespace TanksGame.TanksEngine
{
    public class Control
    {
        #region Public delegate
        public delegate GameControl Action();

        public event Action GetAction;
        #endregion

        #region Handler
        public GameControl OnGetAction()
        {
            if (GetAction != null)
            {
                return GetAction();
            }
            return GameControl.DafaultAction;
        }
        #endregion 
    }
}