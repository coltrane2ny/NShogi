using System;

namespace NShogi
{
    public class Game
    {
        private IGameUI _ui;
        private Position currentPosition = new Position();

        public string BlackPlayerName { get; set; }
        public string WhitePlayerName { get; set; }

        public Game(IGameUI ui)
        {
            _ui = ui;
        }

        public void Start()
        {
            _ui.Start();
            while(true)
            {
                Move move = _ui.WaitMove(currentPosition);
                if (move == null)
                    continue;

                if (move.Finished)
                    break;

                if (move.IsDrop)
                    currentPosition = currentPosition.Drop(move.DstIndex, move.PieceType);
                else
                    currentPosition = currentPosition.Move(move.SrcIndex, move.DstIndex, move.Promote);
            }
        }
    }

    public interface IGameUI
    {
        void Start();
        Move WaitMove(Position position);
    }
}