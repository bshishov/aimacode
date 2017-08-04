using Aima.AgentSystems;

namespace Aima.Domain.NQueens
{
    public class NQueensAction : IAction
    {
        public uint QueenPosition;

        public NQueensAction(uint queenPosition)
        {
            QueenPosition = queenPosition;
        }

        public string Name => QueenPosition.ToString();
    }
}