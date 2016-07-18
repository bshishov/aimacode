namespace Aima.Search.Methods.Genetic.Operators
{
    public interface ICrossoverOperator<TAlphabet>
    {
        TAlphabet[] Apply(TAlphabet[] x, TAlphabet[] y);
    }
}