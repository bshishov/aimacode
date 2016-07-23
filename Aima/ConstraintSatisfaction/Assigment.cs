namespace Aima.ConstraintSatisfaction
{
    public class Assigment<T>
    {
        public readonly string Name;
        public readonly T Value;

        public Assigment(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name} = {Value}";
        }
    }
}