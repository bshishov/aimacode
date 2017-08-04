using Aima.AgentSystems;

namespace Aima.Domain.TSP
{
    public struct TravelingSalesPersonAction : IAction
    {
        public uint CityId;
        public string Name => CityId.ToString();
    }
}