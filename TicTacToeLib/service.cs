using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeNET
{
    public struct Move
    {
        public int x; public int y;
    }
    public class BestMove
    {
        public Move m;
        public int score;
    }
    public class State
    {
        public int[,] board = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        public State() { }
        public State(int[,] _b, int player)
        {
            board = _b;
            currentPlayer = player;

        }
        public bool IsGameOver()
        {
            if (Win()) return true;

            //no more choices available meass draw
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (board[i, y] == 0) return false;
                }
            }
            return true;
        }

        public int currentPlayer = 0; //1 or2
        public int nextPlayer = 0;
        public int score()
        {
            if (currentPlayer == 1 && this.Win())
            {
                return -10;
            }
            else if (currentPlayer == 2 && Win())
            {
                return 10;
            }
            else
                return 0;

        }
        public bool Win()
        {
            //for p;layer 1 check if there are continoous three 1 present
            //for player 2 check if there  are continuos three 2 present
            bool win = false;
            //check row wise
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == currentPlayer) win = true;
                    else { win = false; break; }

                }
                if (win == true) { break; }
            }
            if (!win)
            {
                //check column wise
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[j, i] == currentPlayer) win = true;
                        else { win = false; break; }

                    }
                    if (win == true) { break; }
                }
            }

            if (!win)
            {
                if ((board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] == currentPlayer) || (board[2, 0] == board[1, 1] && board[2, 0] == board[0, 2] && board[2, 0] == currentPlayer))
                {
                    win = true;
                }
            }

            return win;
        }
        /// <summary>
        /// return moves where current board has 0 
        /// </summary>
        /// <returns></returns>
        public Move[] GetAvailableMoves()
        {
            List<Move> moves = new List<Move>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0) moves.Add(new Move() { x = i, y = j });
                }
            }
            return moves.ToArray();

        }

        public void UpdateBoard(Move mv)
        {
            this.board[mv.x, mv.y] = currentPlayer;
        }

        public void Print()
        {
            Console.WriteLine("-----------------");
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Console.Write(board[i, y] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------");
        }
    }

    public class Service
    {
        public static State GetChildState(State s, Move mv)
        {

            var state = new State();

            state.board = (int[,])s.board.Clone();
            state.currentPlayer = s.currentPlayer;
            state.board[mv.x, mv.y] = s.currentPlayer;
            return state;

        }
        public static BestMove MiniMax(State state)
        {
            //state.Print();
            //Console.WriteLine(state.currentPlayer + "state.currentPlayer ");
            if (state.IsGameOver())
            {
                //state.currentPlayer= state.currentPlayer == 1 ? 2 : 1;
                return new BestMove() { score = state.score(), m = new Move() { x = -1, y = -1 } };
            }
            else
            {
                List<int> scores = new List<int>();
                List<Move> moves = new List<Move>();

                var availableMoves = state.GetAvailableMoves();
                foreach (var move in availableMoves)
                {
                    moves.Add(move);
                    var nextState = GetChildState(state, move);
                    if (nextState.IsGameOver())
                    {
                        scores.Add(nextState.score());

                    }
                    else
                    {
                        //nextState.currentPlayer = state.currentPlayer;
                        //nextState.nextPlayer = state.currentPlayer == 1 ? 2 : 1;
                        nextState.currentPlayer = state.currentPlayer == 1 ? 2 : 1;
                        var bestMove = MiniMax(nextState);

                        scores.Add(bestMove.score);

                    }
                }

                if (state.currentPlayer == 2)
                {
                    int max = scores.Max();
                    int maxInd = scores.IndexOf(max);
                    return new BestMove() { m = moves[maxInd], score = max };
                }
                else
                {
                    int min = scores.Min();
                    int minInd = scores.IndexOf(min);
                    return new BestMove() { m = moves[minInd], score = min };
                }
            }

        }
    }
}
