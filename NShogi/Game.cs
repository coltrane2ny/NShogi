using System;

namespace NShogi
{
    public class Game
    {
        private IGameUI _ui;
        private Position currentPosition = new Position();
        private Score score;

        public string BlackPlayerName { get; set; }
        public string WhitePlayerName { get; set; }

        public Game(IGameUI ui)
        {
            _ui = ui;
        }

        public void Start(Position initial)
        {
            _ui.Start();
            score = new Score(initial);
            while(true)
            {
                Move move = _ui.WaitMove(currentPosition, score);
                if (move == null)
                    continue;

                if (move.Finished)
                    break;

                if (move.IsDrop)
                    currentPosition = currentPosition.Drop(move.DstIndex, move.PieceType);
                else
                    currentPosition = currentPosition.Move(move.SrcIndex, move.DstIndex, move.Promote);

                score.AddMove(move);
            }
        }
    }

    public interface IGameUI
    {
        void Start();
        Move WaitMove(Position position, Score score);
    }
}