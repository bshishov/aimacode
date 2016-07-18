namespace Aima.Search.Methods.SimulatedAnnealing
{
    public interface IAnnealingSchedule
    {
        double Temparature(int time);
    }
}