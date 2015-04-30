namespace Tanks.GameEngine
{
    public class ControlBase
    {
        public delegate ControlActions Control();

        public Control GetAction;                       //Можливо, це мало бути автопроперті. Якщо це поле, то варто його зробити приватним і назвати по code convention

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