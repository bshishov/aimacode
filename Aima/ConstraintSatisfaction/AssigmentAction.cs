using Aima.AgentSystems;

namespace Aima.ConstraintSatisfaction
{
    public class AssigmentAction : IAction
    {
        public AssigmentAction(string variable)
        {
            Name = variable;
        }

        public string Name { get; }
    }

    public class AssigmentAction<T> : AssigmentAction
    {
        public AssigmentAction(Assigment<T> assigment)
            : base(assigment.Name)
        {
        }
    }
}