namespace Aima.AgentSystems
{
    public class SimpleAction : IAction
    {
        public string Name { get; }

        public SimpleAction(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var action = obj as IAction;
            if (action != null)
                return action.Name.Equals(Name);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}