using Aima.AgentSystems;

namespace Aima.ConstraintSatisfaction
{
    public class AssigmentAction : IAction
    {
        public string Name { get; }

        public AssigmentAction(string variable)
        {
            Name = variable;
        }
    }

    public class AssigmentAction<T> : AssigmentAction
    {
        public AssigmentAction(Assigment<T> assigment)
            : base(assigment.Name)
        {
        }
    }
}