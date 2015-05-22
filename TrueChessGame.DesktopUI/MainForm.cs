
//Code Review: використані зайві директиви
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrueChessGame.GameEngine;
using TrueChessGame.DesktopUI.Properties;

namespace TrueChessGame.DesktopUI
{

    public partial class MainForm : Form
    {
        private const int FRAME_WIDTH=47;

        private List<PictureBox> _pieces;

        private Dictionary<sbyte, Image> _icons;

        private Square _startSquare;

        private Square _finishSquare;

        private bool _moveIsBeingChosen;

        private ChessBoard _board;

        private delegate sbyte GetSbyteFromLetter(char letter);

        private string _notation;

       // private bool _pawnPromotionChosen;

      //  private char _pawnPromotionPieceLetter;

        public MainForm()
        {
            InitializeComponent();
            //MainForm.ActiveForm.Text = "Dan's True Chess Game. White move";

        }

        private void InitializeIcons()
        {
            _icons = new Dictionary<sbyte, Image>();
            _icons[1] = Resources.White_P;
            _icons[2] = Resources.White_N;
            _icons[3] = Resources.White_B;
            _icons[4] = Resources.White_R;
            _icons[5] = Resources.White_Q;
            _icons[6] = Resources.White_K;
            _icons[-1] = Resources.Black_P;
            _icons[-2] = Resources.Black_N;
            _icons[-3] = Resources.Black_B;
            _icons[-4] = Resources.Black_R;
            _icons[-5] = Resources.Black_Q;
            _icons[-6] = Resources.Black_K;
        }

        public void DrawGameField()
        {
            //if (DefaultInfo.IsWhiteMove)
            //{
            //    pcbCurrentPlayer.Image = Resources.White_K;
            //}
            //else
            //{
            //    pcbCurrentPlayer.Image = Resources.Black_K;
            //}

            foreach (Control pcb in _pieces)
            {
                this.Controls.Remove(pcb); pcb.Dispose();
            }
            _pieces = new List<PictureBox>();
            DrawSidesStrength();
            //PictureBox temp = new PictureBox();
            //temp.Location = new Point(100, 550);
            //temp.Image = _icons[5];
            //temp.Size = temp.Image.Size;
            //temp.Visible = true;

            for (int trank = 8; trank >= 1; trank--)
            {
                for (char tfile = 'a'; tfile <= 'h'; tfile++)
                {
                    if (_board[tfile, trank]!=0)
                    {
                        PictureBox tempPiece = new PictureBox();
                        tempPiece.Location = new Point(FRAME_WIDTH + (tfile - 97) * 50, FRAME_WIDTH + (8 - trank) * 50);
                        tempPiece.Image = _icons[_board[tfile, trank]];
                        tempPiece.Size = tempPiece.Image.Size;
                        tempPiece.Parent = pcbMainChessboard;
                        tempPiece.BackColor = System.Drawing.Color.Transparent;
                        tempPiece.MouseClick += Chessboard_Click;
                        _pieces.Add(tempPiece);
                        PictureBox tempReversedPiece = new PictureBox();
                        tempReversedPiece.Location = new Point(FRAME_WIDTH + (7 - (tfile - 97) ) * 50, FRAME_WIDTH + (trank - 1) * 50);
                        tempReversedPiece.Image = _icons[_board[tfile, trank]];
                        tempReversedPiece.Size = tempReversedPiece.Image.Size;
                        tempReversedPiece.Parent = pcbReversedChessboard;
                        tempReversedPiece.BackColor = System.Drawing.Color.Transparent;
                        tempReversedPiece.MouseClick += Chessboard_Click;
                        _pieces.Add(tempReversedPiece);
                    }
                }
            }
        }

        private void DrawSidesStrength()
        {
            List<sbyte> whitepieces = new List<sbyte>();
            List<sbyte> blackpieces = new List<sbyte>();
            for (char tfile = 'a'; tfile <= 'h'; tfile++)
            {
                for (sbyte trank = 1; trank <= 8; trank++)
                {
                    if (_board[tfile, trank] > 0)
                    {
                        whitepieces.Add(_board[tfile, trank]);
                    }
                    else if (_board[tfile, trank] < 0)
                    {
                        blackpieces.Add(_board[tfile, trank]);
                    }
                }
            }
            //Console.SetCursorPosition(0, 10);
            //Console.Write("White: ");
            //List<string> letters = new List<string>();
            whitepieces.Sort();
            whitepieces.Reverse();
            for (int i = 0; i < whitepieces.Count; i++ )
            {
                PictureBox tempPiece = new PictureBox();
                tempPiece.Location = new Point(pcbMainChessboard.Location.X + i * 55, pcbMainChessboard.Location.Y+ pcbMainChessboard.Height+ 15);
                tempPiece.Image = _icons[whitepieces[i]];
                tempPiece.Size = tempPiece.Image.Size;
                //tempPiece.Parent = panelSideStrength;
                tempPiece.BackColor = System.Drawing.Color.Transparent;
                this.Controls.Add(tempPiece);
                _pieces.Add(tempPiece);
            }
            blackpieces.Sort();
            for (int i = 0; i < blackpieces.Count; i++)
            {
                PictureBox tempPiece = new PictureBox();
                tempPiece.Location = new Point(pcbMainChessboard.Location.X + i * 55, pcbMainChessboard.Location.Y + pcbMainChessboard.Height + 80);
                tempPiece.Image = _icons[blackpieces[i]];
                tempPiece.Size = tempPiece.Image.Size;
                //tempPiece.Parent = groupBox1;
                tempPiece.BackColor = System.Drawing.Color.Transparent;
                this.Controls.Add(tempPiece);
                _pieces.Add(tempPiece);
            }
        }

        //Code Review: зайві відступи
        private void Form1_Load(object sender, EventArgs e)
        {

            InitializeIcons();
            _moveIsBeingChosen = false;
            //_pawnPromotionChosen = false;
            _pieces = new List<PictureBox>();
            _board = new ChessBoard();
            _board.SetDefaultChessBoard();
            DefaultInfo.SetDefaultValues();
            DrawGameField();
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://www.virtualpieces.net/";
            linkLabel1.Links.Add(link);
            //MainForm.ActiveForm.Text = "Dan's True Chess Game. White, your move";
            //buttonChosen.Parent = MainChessboard;

        }
        
        //Code Review:  дуже великий метод, краще розбити на менші
        private void Chessboard_Click(object sender, EventArgs e)
        {
            //this.OnClick(new EventArgs());
            Point position = this.PointToClient(Control.MousePosition);
            bool inMainChessboard;
            bool inBorders = CheckIfCursorInBorders(ref position, out inMainChessboard);
            if (inBorders && !DefaultInfo.EndOfGame)
            {
                char file; int rank;
                if (inMainChessboard)
                {
                    file = (char)((int)((position.X - FRAME_WIDTH - pcbMainChessboard.Location.X) / 50) + 97);
                    rank = 8 - ((position.Y - FRAME_WIDTH - pcbMainChessboard.Location.Y) / 50);
                }
                else
                {
                    file = (char)((int)(7 - (position.X - FRAME_WIDTH - pcbReversedChessboard.Location.X) / 50) + 97);
                    rank = ((position.Y - FRAME_WIDTH - pcbReversedChessboard.Location.Y) / 50)+1;
                   // MessageBox.Show(file.ToString() + rank.ToString());

                }
                if (file<'a' || file >'h' || rank<1 || rank>8)
                {
                    return;
                }
                if (!(_board[file, rank] > 0 ^ DefaultInfo.IsWhiteMove) && _board[file, rank]!=0)
                {
                    _startSquare = new Square(file, rank);
                    _moveIsBeingChosen = true;
                    btnChosen.Location = new Point(pcbMainChessboard.Location.X+ FRAME_WIDTH+ (file-97)*50 + 15, pcbMainChessboard.Location.Y+ FRAME_WIDTH + (8-rank) *50 + 30);
                    btnReversedChosen.Location = new Point(pcbReversedChessboard.Location.X + FRAME_WIDTH + (7 - (file - 97)) * 50 + 15, pcbReversedChessboard.Location.Y + FRAME_WIDTH + (rank-1) * 50 + 30);
                    btnChosen.Visible = true;
                    btnReversedChosen.Visible = true;
                }
                else if (_moveIsBeingChosen)
                {
                    _finishSquare = new Square(file, rank);
                    _notation = GetFIDENotation(_startSquare, _finishSquare, _board);
                    //MessageBox.Show(GetFIDENotation(_startSquare, _finishSquare, _board));
                    if (DefaultInfo.IsWhiteMove)
                    {
                        WhiteMove();
                    }
                    else
                    {
                        BlackMove();
                    }
                    _moveIsBeingChosen = false;
                    btnChosen.Visible = false;
                    btnReversedChosen.Visible = false;
                    DrawGameField();
                    if (DefaultInfo.IsWhiteMove)
                    {
                        if (!FIDEnotation.CheckIfArePossibleMoves(_board, true))
                        {
                            if (!WhiteKing.IsSafe(_board))
                            {
                                DefaultInfo.BlackWin = true;
                                DefaultInfo.EndOfGame = true;
                                MainForm.ActiveForm.Text = "Dan's True Chess Game. Black win";
                                return;
                            }
                            DefaultInfo.EndOfGame = true;
                            MainForm.ActiveForm.Text = "Dan's True Chess Game. Drawn";
                            return;
                        }
                        MainForm.ActiveForm.Text = "Dan's True Chess Game. White, your move";
                    }
                    else
                    {
                        if (!FIDEnotation.CheckIfArePossibleMoves(_board, false))
                        {
                            if (!BlackKing.IsSafe(_board))
                            {
                                DefaultInfo.WhiteWin = true;
                                DefaultInfo.EndOfGame = true;
                                MainForm.ActiveForm.Text = "Dan's True Chess Game. White win";
                                return;
                            } 
                            DefaultInfo.EndOfGame = true;
                            MainForm.ActiveForm.Text = "Drawn";
                            return;

                        }
                        MainForm.ActiveForm.Text = "Dan's True Chess Game. Black, your move";
                    }
                    //if (DefaultInfo.IsWhiteMove)
                    //{
                    //    MainForm.ActiveForm.Text = "Dan's True Chess Game. White move";
                    //}
                    //else
                    //{
                    //    MainForm.ActiveForm.Text = "Dan's True Chess Game. Black move";
                    //}
                }
            }
        }

        private void WhiteMove()
        {
            DefaultInfo.WhiteEnPassantEndangered = false;
            try
            {
                _board = FIDEnotation.PerformWhiteMove(_board, _notation);
                DefaultInfo.IsWhiteMove = !DefaultInfo.IsWhiteMove;

            }
            catch (ArgumentException)
            {
                return;
            }
        }
        
        //Code Review: погана обробка виключення
        private void BlackMove()
        {
            DefaultInfo.BlackEnPassantEndangered = false;
            try
            {
                _board = FIDEnotation.PerformBlackMove(_board, _notation);
                DefaultInfo.IsWhiteMove = !DefaultInfo.IsWhiteMove;

            }
            catch (ArgumentException)
            {
                return;
            }
        }

        private bool CheckIfCursorInBorders(ref Point position, out bool inMainChessboard)
        {
            int maxPossibleX = pcbMainChessboard.Location.X + pcbMainChessboard.Width - FRAME_WIDTH;
            int minPossibleX = pcbMainChessboard.Location.X + FRAME_WIDTH;
            int maxPossibleY = pcbMainChessboard.Location.Y + pcbMainChessboard.Height - FRAME_WIDTH;
            int minPossibleY = pcbMainChessboard.Location.Y + FRAME_WIDTH;

            int maxReversedPossibleX = pcbReversedChessboard.Location.X + pcbReversedChessboard.Width - FRAME_WIDTH;
            int minReversedPossibleX = pcbReversedChessboard.Location.X + FRAME_WIDTH;
            int maxReversedPossibleY = pcbReversedChessboard.Location.Y + pcbReversedChessboard.Height - FRAME_WIDTH;
            int minReversedPossibleY = pcbReversedChessboard.Location.Y + FRAME_WIDTH;

            inMainChessboard = position.X <= maxPossibleX && position.X >= minPossibleX && position.Y >= minPossibleY && position.Y <= maxPossibleY;
            bool inReversedChessboard = position.X <= maxReversedPossibleX && position.X >= minReversedPossibleX && position.Y >= minReversedPossibleY && position.Y <= maxReversedPossibleY;
            //if (position.X <= maxReversedPossibleX && position.X >= minReversedPossibleX)
            //{
            //    MessageBox.Show("rev");
            //}
            return inMainChessboard || inReversedChessboard;
        }
        
        //Code Review: неправильна назва методу -> GetFideNotation
        private string GetFIDENotation(Square start, Square end, ChessBoard board)
        {
            string result="";
            GetSbyteFromLetter getSbyteFromLetter;
            if (DefaultInfo.IsWhiteMove)
            {
                getSbyteFromLetter = FIDEnotation.GetSbyteFromWhitePieceLetter;
            }
            else
            {
                getSbyteFromLetter = FIDEnotation.GetSbyteFromBlackPieceLetter;
            }
            //if no piece chosen
            if (board[start]==0)
            {
                return result;
            }
            //for pawn. TODO promotion
            if (board[start]==getSbyteFromLetter('P'))
            {
                GetPawnFIDENotation(ref start, ref end, ref result);
            }
            //King
            else if (board[start]==getSbyteFromLetter('K'))
            {
                GetKingFIDENotation(ref start, ref end, ref result);
            }
            //other pieces
            else if (!(DefaultInfo.IsWhiteMove ^ board[start] > 0))
            {               
                result += FIDEnotation.GetLetter(board[start]);
                result += start._file + start._rank.ToString();
                result += end._file + end._rank.ToString();
            }
            return result;
        }
        
        //Code Review: неправильна назва методу
        private static void GetKingFIDENotation(ref Square start, ref Square end, ref string result)
        {
            if (start._rank == end._rank && start._file - end._file == -2)
            {
                result = "0-0";
            }
            else if (start._rank == end._rank && start._file - end._file == 2)
            {
                result = "0-0-0";
            }
            else if (Math.Abs(start._file - end._file) <= 1 && Math.Abs(start._rank - end._rank) <= 1)
            {
                result = "K" + end._file + end._rank.ToString();
            }
        }
        
        //Code Review: неправильна назва методу
        private void GetPawnFIDENotation(ref Square start, ref Square end, ref string result)
        {
            result += start._file;
            //e4
            if (start._file == end._file)
            {
                result += end._rank.ToString();
            }
            //ed4
            else
            {
                result += end._file;
                result += end._rank.ToString();
            }
            //promotion
            if (DefaultInfo.IsWhiteMove && start._rank == 7 && _board[end] <= 0 && Math.Abs(start._file - end._file) <= 1)
            {
                result += "Q";
                // WhitePawnPromotion ff = new WhitePawnPromotion();
                //// gbChooseWhitePromotion.Visible = true;Installer Project

            }
            if (!DefaultInfo.IsWhiteMove && start._rank == 2 && _board[end] <= 0 && Math.Abs(start._file - end._file) <= 1)
            {
                result += "Q";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }
        
        //Code Review: невідомо до якого батона відбувається клік, потрібно вказати імя.
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control pcb in _pieces)
            {
                this.Controls.Remove(pcb); pcb.Dispose();
            }
            MainForm.ActiveForm.Text = "Dan's True Chess Game. White, your move";
            _pieces = new List<PictureBox>();
            _moveIsBeingChosen = false;
           // _pawnPromotionChosen = false;
            _pieces = new List<PictureBox>();
            _board = new ChessBoard();
            _board.SetDefaultChessBoard();
            DefaultInfo.SetDefaultValues();
            DrawGameField();
        }

    }
}
