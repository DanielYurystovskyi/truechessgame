using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrueChessGame.GameEngine;

namespace TrueChessGame.UnitTest
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void TestHashCode()
        {
            ChessBoard board = new ChessBoard();
            board.SetDefaultChessBoard();
            ChessBoard sndboard = board.ShallowCopy();
            Assert.AreEqual(board.GetHashCode(), sndboard.GetHashCode());
            sndboard = new ChessBoard(5);
            Assert.AreNotEqual(board.GetHashCode(), sndboard.GetHashCode());
        }

        [TestMethod]
        public void TestReverseChessboard()
        {
            ChessBoard board = new ChessBoard();
            board.SetDefaultChessBoard();
            board = FIDEnotation.PerformWhiteMove(board, "e4");
            ChessBoard sndboard = new ChessBoard();
            sndboard.SetDefaultChessBoard();
            board.ReverseSides();
            sndboard = FIDEnotation.PerformBlackMove(sndboard, "e5");
            board.DebugConsoleSimpleDraw();
            Console.WriteLine();
            sndboard.DebugConsoleSimpleDraw();
            Assert.AreEqual(board, sndboard);
        }

        //[TestMethod]
        //public void TestList()
        //{
        //    ChessBoard board = new ChessBoard();
        //    board.SetDefaultChessBoard();
        //    //board = Piece.PerformMove(board, new Square('e', 2), new Square('e', 4));
        //    board=FIDEnotation.PerformWhiteMove(board, "e4");
        //    board = FIDEnotation.PerformBlackMove(board, "d5");
        //    board.DebugConsoleSimpleDraw();
        //}

        [TestMethod]
        public void TestSetDefaultValues()
        {
            DefaultInfo.SetDefaultValues();
            Assert.IsTrue(DefaultInfo.IsWhiteMove);
            Assert.IsFalse(DefaultInfo.WhiteWin);
            Assert.IsFalse(DefaultInfo.BlackWin);
            Assert.IsTrue(DefaultInfo.WhiteAsideRookIsUnMoved);
            Assert.IsTrue(DefaultInfo.WhiteKingIsUnMoved);
            Assert.IsTrue(DefaultInfo.WhiteHsideRookIsUnMoved);
            Assert.IsTrue(DefaultInfo.BlackKingIsUnMoved);
            Assert.IsTrue(DefaultInfo.BlackAsideRookIsUnMoved);
            Assert.IsTrue(DefaultInfo.BlackHsideRookIsUnMoved);
            Assert.IsFalse(DefaultInfo.WhiteEnPassantEndangered);
            Assert.IsFalse(DefaultInfo.BlackEnPassantEndangered);
        }

        [TestMethod]
        public void TestPossibleMoves()
        {
            ChessBoard board = new ChessBoard();
            board.SetDefaultChessBoard();
            Assert.IsTrue(FIDEnotation.CheckIfArePossibleMoves(board, true));
            Assert.IsTrue(FIDEnotation.CheckIfArePossibleMoves(board, false));
        }

        [TestMethod]
        public void TestPawnMove()
        {
            ChessBoard board = new ChessBoard();
            board.SetDefaultChessBoard();
            ChessBoard sndboard = new ChessBoard();
            sndboard.SetDefaultChessBoard();
            sndboard['e', 2] = 0;
            sndboard['e', 4] = 1;
            sndboard['d', 7] = 0;
            sndboard['d', 5] = -1;
            sndboard['c', 2] = 0;
            sndboard['c', 3] = 1;
            sndboard['e', 7] = 0;
            sndboard['e', 6] = -1;
            board = FIDEnotation.PerformWhiteMove(board, "e4");
            board = FIDEnotation.PerformBlackMove(board, "d5");
            board = FIDEnotation.PerformWhiteMove(board, "c3");
            board = FIDEnotation.PerformBlackMove(board, "e6");
            board.DebugConsoleSimpleDraw();
            Assert.AreEqual(board, sndboard);
        }

        [TestMethod]
        public void TestPawnCapture()
        {
            ChessBoard board = new ChessBoard();
            board.SetDefaultChessBoard();
            ChessBoard sndboard = new ChessBoard();
            sndboard.SetDefaultChessBoard();
            sndboard['e', 2] = 0;
            sndboard['e', 7] = 0;
            sndboard['d', 5] = -1;
            sndboard['d', 7] = 0;
            sndboard['d', 2] = 0;
            sndboard['d', 4] = 1;
            board = FIDEnotation.PerformWhiteMove(board, "e4");
            board = FIDEnotation.PerformBlackMove(board, "d5");
            board = FIDEnotation.PerformWhiteMove(board, "d4");
            board = FIDEnotation.PerformBlackMove(board, "e6");
            board = FIDEnotation.PerformWhiteMove(board, "ed5");
            board = FIDEnotation.PerformBlackMove(board, "ed5");
            sndboard['a', 2] = 0;
            sndboard['b', 7] = 0;
            sndboard['b', 5] = 1;
            board = FIDEnotation.PerformWhiteMove(board, "a4");
            board = FIDEnotation.PerformBlackMove(board, "b5");
            board = FIDEnotation.PerformWhiteMove(board, "ab5");
            Assert.AreEqual(board, sndboard);
            board = FIDEnotation.PerformBlackMove(board, "a6");
            board = FIDEnotation.PerformBlackMove(board, "ab5");
            sndboard['a', 7] = 0;
            sndboard['b', 5] = -1;
            Assert.AreEqual(board, sndboard);
            sndboard['h', 2] = 0;
            sndboard['g', 7] = 0;
            sndboard['g', 5] = 1;
            board = FIDEnotation.PerformWhiteMove(board, "h4");
            board = FIDEnotation.PerformBlackMove(board, "g5");
            board = FIDEnotation.PerformWhiteMove(board, "hg5");
            Assert.AreEqual(board, sndboard);
            board = FIDEnotation.PerformBlackMove(board, "h6");
            board = FIDEnotation.PerformBlackMove(board, "hg5");
            sndboard['h', 7] = 0;
            sndboard['g', 5] = -1;
            Assert.AreEqual(board, sndboard);
        }

        [TestMethod]
        public void TestPawnPromotions()
        {
            ChessBoard board = new ChessBoard();
            board['e', 1] = (sbyte)DefaultPieces.WhiteKing;
            board['e', 8] = (sbyte)DefaultPieces.BlackKing;
            board['a', 7] = (sbyte)DefaultPieces.WhitePawn;
            board['a', 2] = (sbyte)DefaultPieces.BlackPawn;
            board['c', 7] = (sbyte)DefaultPieces.WhitePawn;
            board['c', 2] = (sbyte)DefaultPieces.BlackPawn;
            board['b', 8]=(sbyte)DefaultPieces.BlackRook;
            board['b', 1]=(sbyte)DefaultPieces.WhiteRook;
            ChessBoard sndboard = board.ShallowCopy();
            board = FIDEnotation.PerformWhiteMove(board, "a8N");
            board = FIDEnotation.PerformBlackMove(board, "a1N");
            sndboard['a', 7] = 0;
            sndboard['a', 2] = 0;
            sndboard['a', 8] = (sbyte)DefaultPieces.WhitekNight;
            sndboard['a', 1] = (sbyte)DefaultPieces.BlackkNight;
            Assert.AreEqual(board, sndboard);
            board = FIDEnotation.PerformWhiteMove(board, "cb8B");
            board = FIDEnotation.PerformBlackMove(board, "cb1B");
            sndboard['c', 7] = 0;
            sndboard['c', 2] = 0;
            sndboard['b', 8] = (sbyte)DefaultPieces.WhiteBishop;
            sndboard['b', 1] = (sbyte)DefaultPieces.BlackBishop;
            Assert.AreEqual(board, sndboard);
        }

        [TestMethod]
        public void TestPawnEnPassant()
        {
            ChessBoard board = new ChessBoard();
            board.SetDefaultChessBoard();
            board = FIDEnotation.PerformWhiteMove(board, "e4");
            board = FIDEnotation.PerformBlackMove(board, "d5");
            board = FIDEnotation.PerformWhiteMove(board, "e5");
            board = FIDEnotation.PerformBlackMove(board, "d4");
            board = FIDEnotation.PerformWhiteMove(board, "c4");
            board = FIDEnotation.PerformBlackMove(board, "dc3");
            board = FIDEnotation.PerformWhiteMove(board, "a4");
            board = FIDEnotation.PerformBlackMove(board, "f5");
            board = FIDEnotation.PerformWhiteMove(board, "ef6");
            board.DebugConsoleSimpleDraw();
            ChessBoard sndboard = new ChessBoard();
            sndboard.SetDefaultChessBoard();
            sndboard['d', 7] = 0;
            sndboard['f', 7] = 0;
            sndboard['f', 6] = 1;
            sndboard['c', 3] = -1;
            sndboard['c', 2] = 0;
            sndboard['e', 2] = 0;
            sndboard['a', 4] = 1;
            sndboard['a', 2] = 0;
            Assert.AreEqual(board, sndboard);
        }

        //[TestMethod]
        //public void TestPawnPromotion()
        //{
        //    ChessBoard board = new ChessBoard();
        //    board['a', 7] = 1;
        //    board['a', 2] = -1;
        //    board['d', 2] = (sbyte)DefaultPieces.WhiteKing;
        //    board['e', 7] = (sbyte)DefaultPieces.BlackKing;
        //   ChessBoard sndboard = new ChessBoard();
        //    board = FIDEnotation.PerformWhiteMove(board, "a8Q");
        //    board = FIDEnotation.PerformBlackMove(board, "a1R");
        //    sndboard['a', 8] = (sbyte)DefaultPieces.WhiteQueen;
        //    sndboard['a', 1] = (sbyte)DefaultPieces.BlackRook;
        //    sndboard['d', 2] = (sbyte)DefaultPieces.WhiteKing;
        //    sndboard['e', 7] = (sbyte)DefaultPieces.BlackKing;
        //    Assert.AreEqual(board, sndboard);
        //}

        [TestMethod]
        public void TestPiecesMove()
        {
            ChessBoard board = new ChessBoard();
            board.SetDefaultChessBoard();
            ChessBoard sndboard = board.ShallowCopy();
            board = FIDEnotation.PerformWhiteMove(board, "e4");
            board = FIDEnotation.PerformBlackMove(board, "e5");
            board = FIDEnotation.PerformWhiteMove(board, "Nc3");
            board = FIDEnotation.PerformBlackMove(board, "Nc6");
            board = FIDEnotation.PerformWhiteMove(board, "Bc4");
            board = FIDEnotation.PerformBlackMove(board, "Bc5");
            board = FIDEnotation.PerformWhiteMove(board, "d4");
            board = FIDEnotation.PerformBlackMove(board, "d5");
            board = FIDEnotation.PerformWhiteMove(board, "Bf4");
            board = FIDEnotation.PerformBlackMove(board, "Bf5");
            board = FIDEnotation.PerformWhiteMove(board, "Qd3");
            board = FIDEnotation.PerformBlackMove(board, "Qd6");
            board = FIDEnotation.PerformWhiteMove(board, "Ke2");
            board = FIDEnotation.PerformBlackMove(board, "Ke7");
            board=FIDEnotation.PerformWhiteMove(board, "Rd1");
            board=FIDEnotation.PerformBlackMove(board, "Rd8");
            for (char tfile = 'a'; tfile <= 'f'; tfile++ )
            {
                sndboard[tfile, 8] = 0;
                sndboard[tfile, 1] = 0;
            }
            
// 0  0  0 -4  0  0 -2 -4
//-1 -1 -1  0 -6 -1 -1 -1
// 0  0 -2 -5  0  0  0  0
// 0  0 -3 -1 -1 -3  0  0
// 0  0 +3 +1 +1 +3  0  0
// 0  0 +2 +5  0  0  0  0
//+1 +1 +1  0 +6 +1 +1 +1
// 0  0  0 +4  0  0 +2 +4

            sndboard['d', 1] = 4; sndboard['d', 8] = -4;
            sndboard['d', 2] = 0; sndboard['e', 2] = 6;
            sndboard['d', 7] = 0; sndboard['e', 7] = -6;
            sndboard['c', 3] = 2; sndboard['c', 6] = -2;
            sndboard['d', 3] = 5; sndboard['d', 6] = -5;
            sndboard['f', 4] = 3; sndboard['f', 5] = -3;
            sndboard['c', 4] = 3; sndboard['c', 5] = -3;
            sndboard['d', 3] = 5; sndboard['d', 6] = -5;
            sndboard['d', 4] = 1; sndboard['d', 5] = -1;
            sndboard['e', 2] = 6; sndboard['e', 7] = -6;
            sndboard['e', 4] = 1; sndboard['e', 5] = -1;
            sndboard['f', 4] = 3; sndboard['f', 5] = -3;
            //sndboard.DebugConsoleSimpleDraw();
            Assert.AreEqual(board, sndboard);
        }

        [TestMethod]
        public void TestRooksMove()
        {
            ChessBoard board = new ChessBoard();
            DefaultInfo.SetDefaultValues();
           // board.SetDefaultChessBoard();
            board['a', 1] = (sbyte)DefaultPieces.WhiteRook;
            board['h', 1]=(sbyte)DefaultPieces.WhiteRook;
            board['c', 2]=(sbyte)DefaultPieces.WhiteKing;
            board['a', 8] = (sbyte)DefaultPieces.BlackRook;
            board['h', 8] = (sbyte)DefaultPieces.BlackRook;
            board['c', 7] = (sbyte)DefaultPieces.BlackKing;
            board = FIDEnotation.PerformWhiteMove(board,"Rae1");
            Assert.IsFalse(DefaultInfo.WhiteAsideRookIsUnMoved);
            board = FIDEnotation.PerformBlackMove(board, "Rhe8");
            Assert.IsFalse(DefaultInfo.BlackHsideRookIsUnMoved);
            board = FIDEnotation.PerformWhiteMove(board, "Rhf1");
            Assert.IsFalse(DefaultInfo.WhiteHsideRookIsUnMoved);
            board = FIDEnotation.PerformBlackMove(board, "Rab8");
            Assert.IsFalse(DefaultInfo.BlackAsideRookIsUnMoved);
            board.DebugConsoleSimpleDraw();
            board = FIDEnotation.PerformWhiteMove(board, "Rf6");
            board = FIDEnotation.PerformWhiteMove(board, "Rfe6");
            board = FIDEnotation.PerformWhiteMove(board, "R6e3");
        }

        [TestMethod]
        public void TestWhiteKingIsSafe()
        {
            ChessBoard board = new ChessBoard();
            board['e', 8] = (sbyte)DefaultPieces.WhiteKing;
            board['e', 1] = (sbyte)DefaultPieces.BlackKing;
            Assert.IsTrue(WhiteKing.IsSafe(board));
            board['e', 2] = (sbyte)DefaultPieces.BlackQueen;
            Assert.IsTrue(!WhiteKing.IsSafe(board));
            board['e', 7] = 1;
            Assert.IsTrue(WhiteKing.IsSafe(board));
            board['c', 6] = (sbyte)DefaultPieces.BlackQueen;
            Assert.IsTrue(!WhiteKing.IsSafe(board));
            board['d', 7] = -1;
            board.DebugConsoleSimpleDraw();
            Assert.IsTrue(WhiteKing.IsSafe(board));
            board['a', 8] = (sbyte)DefaultPieces.BlackRook;
            Assert.IsTrue(!WhiteKing.IsSafe(board));
            board['b', 8] = (sbyte)DefaultPieces.BlackkNight;
            Assert.IsTrue(WhiteKing.IsSafe(board));
            board['c', 8] = (sbyte)DefaultPieces.BlackQueen;
            Assert.IsFalse(WhiteKing.IsSafe(board));
            board['d', 8] = (sbyte)DefaultPieces.WhiteQueen;
            Assert.IsTrue(WhiteKing.IsSafe(board));

        }

        [TestMethod]
        public void TestBlackKingIsSafe()
        {
            ChessBoard board = new ChessBoard();
            board['e', 1] = (sbyte)DefaultPieces.BlackKing;
            board['e', 8] = (sbyte)DefaultPieces.WhiteKing;
            Assert.IsTrue(BlackKing.IsSafe(board));
            board['e', 7] = (sbyte)DefaultPieces.WhiteQueen;
            Assert.IsTrue(!BlackKing.IsSafe(board));
            board['e', 2] = -1;
            Assert.IsTrue(BlackKing.IsSafe(board));
            board['c', 3] = (sbyte)DefaultPieces.WhiteQueen;
            Assert.IsTrue(!BlackKing.IsSafe(board));
            board['d', 2] = 1;
            Assert.IsTrue(BlackKing.IsSafe(board));
            board['a', 1] = (sbyte)DefaultPieces.WhiteRook;
            Assert.IsTrue(!BlackKing.IsSafe(board));
            board['b', 1] = (sbyte)DefaultPieces.WhitekNight;
            Assert.IsTrue(BlackKing.IsSafe(board));
            board['c', 1] = (sbyte)DefaultPieces.WhiteQueen;
            Assert.IsFalse(BlackKing.IsSafe(board));
            board['d', 1] = (sbyte)DefaultPieces.BlackQueen;
            Assert.IsTrue(BlackKing.IsSafe(board));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWhiteQueenCastling()
        {
            DefaultInfo.WhiteAsideRookIsUnMoved = true;
            DefaultInfo.WhiteKingIsUnMoved = true;
            DefaultInfo.BlackAsideRookIsUnMoved = true;
            DefaultInfo.BlackKingIsUnMoved = true;
            ChessBoard board = new ChessBoard();
            board['a', 1] = (sbyte)DefaultPieces.WhiteRook;
            board['e', 1] = (sbyte)DefaultPieces.WhiteKing;
            board['a', 8] = (sbyte)DefaultPieces.BlackRook;
            board['e', 8] = (sbyte)DefaultPieces.BlackKing;
            ChessBoard sboard = board.ShallowCopy();
            board = FIDEnotation.PerformWhiteMove(board, "0-0-0");
            sboard['a', 1] = 0;
            sboard['e', 1] = 0;
            sboard['c', 1] = (sbyte)DefaultPieces.WhiteKing;
            sboard['d', 1] = (sbyte)DefaultPieces.WhiteRook;
            Assert.AreEqual(sboard, board);
            //exception!
            board = FIDEnotation.PerformBlackMove(board, "0-0-0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWhiteKingCastling()
        {
            DefaultInfo.WhiteHsideRookIsUnMoved = true;
            DefaultInfo.WhiteKingIsUnMoved = true;
            DefaultInfo.BlackHsideRookIsUnMoved = true;
            DefaultInfo.BlackKingIsUnMoved = true;
            ChessBoard board = new ChessBoard();
            board['h', 1] = (sbyte)DefaultPieces.WhiteRook;
            board['e', 1] = (sbyte)DefaultPieces.WhiteKing;
            board['h', 8] = (sbyte)DefaultPieces.BlackRook;
            board['e', 8] = (sbyte)DefaultPieces.BlackKing;
            ChessBoard sboard = board.ShallowCopy();
            board = FIDEnotation.PerformWhiteMove(board, "0-0");
            sboard['h', 1] = 0;
            sboard['e', 1] = 0;
            sboard['g', 1] = (sbyte)DefaultPieces.WhiteKing;
            sboard['f', 1] = (sbyte)DefaultPieces.WhiteRook;
            Assert.AreEqual(sboard, board);
            //exception!
            board = FIDEnotation.PerformBlackMove(board, "0-0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBlackQueenCastling()
        {
            DefaultInfo.WhiteAsideRookIsUnMoved = true;
            DefaultInfo.WhiteHsideRookIsUnMoved = true;
            DefaultInfo.WhiteKingIsUnMoved = true;
            DefaultInfo.BlackAsideRookIsUnMoved = true;
            DefaultInfo.BlackKingIsUnMoved = true;
            DefaultInfo.BlackHsideRookIsUnMoved = true;
            ChessBoard board = new ChessBoard();
            board['a', 1] = (sbyte)DefaultPieces.WhiteRook;
            board['e', 1] = (sbyte)DefaultPieces.WhiteKing;
            board['a', 8] = (sbyte)DefaultPieces.BlackRook;
            board['e', 8] = (sbyte)DefaultPieces.BlackKing;
            ChessBoard sboard = board.ShallowCopy();
            board = FIDEnotation.PerformBlackMove(board, "0-0-0");
            sboard['a', 8] = 0;
            sboard['e', 8] = 0;
            sboard['c', 8] = (sbyte)DefaultPieces.BlackKing;
            sboard['d', 8] = (sbyte)DefaultPieces.BlackRook;
            board.DebugConsoleSimpleDraw();
            Console.WriteLine();
            sboard.DebugConsoleSimpleDraw();
            Assert.AreEqual(sboard, board);
            //exception!
            board = FIDEnotation.PerformWhiteMove(board, "0-0-0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBlackKingCastling()
        {
            DefaultInfo.WhiteHsideRookIsUnMoved = true;
            DefaultInfo.WhiteKingIsUnMoved = true;
            DefaultInfo.BlackHsideRookIsUnMoved = true;
            DefaultInfo.BlackKingIsUnMoved = true;
            ChessBoard board = new ChessBoard();
            board['h', 1] = (sbyte)DefaultPieces.WhiteRook;
            board['e', 1] = (sbyte)DefaultPieces.WhiteKing;
            board['h', 8] = (sbyte)DefaultPieces.BlackRook;
            board['e', 8] = (sbyte)DefaultPieces.BlackKing;
            ChessBoard sboard = board.ShallowCopy();
            board = FIDEnotation.PerformBlackMove(board, "0-0");
            sboard['h', 8] = 0;
            sboard['e', 8] = 0;
            sboard['g', 8] = (sbyte)DefaultPieces.BlackKing;
            sboard['f', 8] = (sbyte)DefaultPieces.BlackRook;
            Assert.AreEqual(sboard, board);
            //exception!
            board = FIDEnotation.PerformWhiteMove(board, "0-0");
        }

        [TestMethod]
        public void TestAmbigiousPieces()
        {
            ChessBoard board = new ChessBoard();
            board['e', 1] = (sbyte)DefaultPieces.WhiteKing;
            board['e', 8] = (sbyte)DefaultPieces.BlackKing;
            board['c', 3] = (sbyte)DefaultPieces.WhitekNight;
            board['g', 3] = (sbyte)DefaultPieces.WhitekNight;
            board['c', 6] = (sbyte)DefaultPieces.BlackkNight;
            board['g', 6] = (sbyte)DefaultPieces.BlackkNight;
            board = FIDEnotation.PerformWhiteMove(board, "Nce2");
            board = FIDEnotation.PerformBlackMove(board, "Nce7");
            board['g', 5] = (sbyte)DefaultPieces.WhitekNight;
            board['g', 1] = (sbyte)DefaultPieces.WhitekNight;
            board['g', 4] = (sbyte)DefaultPieces.BlackkNight;
            board['g', 8] = (sbyte)DefaultPieces.BlackkNight;
            board = FIDEnotation.PerformWhiteMove(board, "N5f3");
            board = FIDEnotation.PerformBlackMove(board, "N4f6");
            board['a', 1] = (sbyte)DefaultPieces.WhiteBishop;
            board['a', 3] = (sbyte)DefaultPieces.WhiteBishop;
            board['c', 3] = (sbyte)DefaultPieces.WhiteBishop;
            board = FIDEnotation.PerformWhiteMove(board, "Ba1b2");
            board['a', 8] = (sbyte)DefaultPieces.BlackBishop;
            board['a', 6] = (sbyte)DefaultPieces.BlackBishop;
            board['c', 6] = (sbyte)DefaultPieces.BlackBishop;
            board = FIDEnotation.PerformBlackMove(board, "Ba8b7");
        }
    }
}
