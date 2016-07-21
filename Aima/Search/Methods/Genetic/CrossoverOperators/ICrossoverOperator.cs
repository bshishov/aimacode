namespace Aima.Search.Methods.Genetic.CrossoverOperators
{
    public interface ICrossoverOperator<TAlphabet>
    {
        TAlphabet[] Apply(TAlphabet[] x, TAlphabet[] y);
    }
}