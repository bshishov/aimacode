using Aima.AgentSystems;

namespace Aima.Domain.NQueens
{
    public class NQueensAction : IAction
    {
        public uint QueenPosition;
        public string Name => QueenPosition.ToString();

        public NQueensAction(uint queenPosition)
        {
            QueenPosition = queenPosition;
        }
    }
}