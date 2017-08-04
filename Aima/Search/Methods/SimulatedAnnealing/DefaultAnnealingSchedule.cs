using System;

namespace Aima.Search.Methods.SimulatedAnnealing
{
    public class DefaultAnnealingSchedule : IAnnealingSchedule
    {
        private readonly int _k;
        private readonly double _lam;
        private readonly int _limit;

        public DefaultAnnealingSchedule(int k, double lam, int limit)
        {
            _k = k;
            _lam = lam;
            _limit = limit;
        }

        public DefaultAnnealingSchedule()
            : this(20, 0.045, 100)
        {
        }

        public double Temparature(int time)
        {
            if (time < _limit)
            {
                var res = _k*Math.Exp(-1*_lam*time);
                return res;
            }

            return 0.0;
        }
    }
}