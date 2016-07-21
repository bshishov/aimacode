namespace Aima.Search.Methods.Genetic.MutationOperators
{
    public interface IMutationOperator<in TAlphabet>
    {
        void Apply(TAlphabet[] genome);
    }
}
