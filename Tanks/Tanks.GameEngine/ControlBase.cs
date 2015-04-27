namespace Tanks.GameEngine
{
    public class ControlBase
    {
        public delegate ControlActions Control();

        public Control GetAction;

        public ControlActions OnGetActions()
        {
            if (GetAction != null)
            {
                return GetAction();
            }
            return 0;
        }
    }
}