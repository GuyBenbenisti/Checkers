using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    internal class Board
    {
        internal class BoardSquare
        {
            private Location m_Coordinates;
            private Piece m_Checker;

            internal BoardSquare(eShape i_Shape, Location i_Location, ePlayerColor i_Color)
            {
                m_Checker = new Piece(i_Location, i_Shape, i_Color);
                m_Coordinates = i_Location;
            }

            internal BoardSquare(Location i_Location)
            {
                m_Checker = null;
                m_Coordinates = i_Location;
            }
            
            public Piece BoardSquareChecker
            {
                get { return m_Checker; }
                set { m_Checker = value; }
            }

            public Location Coordinates
            {
                get { return m_Coordinates; }
                set { m_Coordinates = value; }
            }

            public eShape GetShape
            {
                get
                {
                    eShape output;
                    if (m_Checker == null)
                    {
                        output = eShape.Blank;
                    }
                    else
                    {
                        output = m_Checker.Shape;
                    }

                    return output;
                }
            }
        }
       
        private int m_Size;
        private BoardSquare[,] m_BoardSquares;

        internal Board(int i_Size)
        {
            m_Size = i_Size;
            this.m_BoardSquares = initiateBoard(i_Size); 
        }

        internal BoardSquare[,] Matrix
        {
            get { return m_BoardSquares; }
        }    

        internal int Size
        {
            get { return m_Size; }
        }

        private BoardSquare[,] initiateBoard(int i_Size)
        {
            BoardSquare[,] BoardSquares = new BoardSquare[i_Size, i_Size];
            int NumberOfCheckersLine = (i_Size / 2) - 1;
            for(int i = 0; i < NumberOfCheckersLine; i++)
            {
                for(int j = 0; j < i_Size; j++)
                {
                    if (i % 2 == 0 && j % 2 != 0)
                    {
                        BoardSquares[i, j] = new BoardSquare(eShape.Black, new Location(i, j), ePlayerColor.Black);
                        BoardSquares[i_Size - i - 1, i_Size - j - 1] = new BoardSquare(eShape.White, new Location(i_Size - i - 1, i_Size - j - 1), ePlayerColor.White);
                    }
                    else if (i % 2 != 0 && j % 2 == 0)
                    {
                        BoardSquares[i, j] = new BoardSquare(eShape.Black, new Location(i, j), ePlayerColor.Black);
                        BoardSquares[i_Size - i - 1, i_Size - j - 1] = new BoardSquare(eShape.White, new Location(i_Size - i - 1, i_Size - j - 1), ePlayerColor.White);
                    }
                    else
                    {
                        BoardSquares[i, j] = new BoardSquare(new Location(i, j));
                        BoardSquares[i_Size - i - 1, i_Size - j - 1] = new BoardSquare(new Location(i_Size - i - 1, i_Size - j - 1));
                    }
                }
            }

            for (int i = 0; i < i_Size; i++)
            {
                BoardSquares[NumberOfCheckersLine, i] = new BoardSquare(new Location(NumberOfCheckersLine, i));
                BoardSquares[NumberOfCheckersLine + 1, i] = new BoardSquare(new Location(NumberOfCheckersLine + 1, i));
            }            

            return BoardSquares;
        }

        internal BoardSquare getBoardSquare(int i_Row, int i_Col)
        {
            return m_BoardSquares[i_Row, i_Col];
        }

        internal void setBoardSquare(int i_Row, int i_Col, eShape i_Shape, ePlayerColor i_Color)
        {
            m_BoardSquares[i_Row, i_Col] = new BoardSquare(i_Shape, new Location(i_Row, i_Col), i_Color);
        }

        internal void PrintBoard()
        {
            StringBuilder output = new StringBuilder();
            StringBuilder LineSeperator = createLineSeperator();
            string newLine = Environment.NewLine;            
            char RowIndex = 'a';
            string CurrentSquareToString;

            output.Append(createColIndexes());
            output.Append(newLine + LineSeperator + newLine);
            for(int i = 0; i < m_Size; i++)
            {
                output.Append(RowIndex++).Append('|');
                for (int j = 0; j < m_Size; j++)
                {
                   switch (m_BoardSquares[i, j].GetShape)
                    {
                        case eShape.Black:
                            CurrentSquareToString = " O |";
                            break;
                        case eShape.White:
                            CurrentSquareToString = " X |";
                            break;
                        case eShape.BlackKing:
                            CurrentSquareToString = " U |";
                            break;
                        case eShape.WhiteKing:
                            CurrentSquareToString = " K |";
                            break;
                        default:
                            CurrentSquareToString = "   |";
                            break;
                    }

                    output.Append(CurrentSquareToString);
                }

                output.Append(newLine + LineSeperator + newLine);
            }

            Console.Write(output.ToString());
        }

        private StringBuilder createColIndexes()
        {
            char ColIndex = 'A';
            StringBuilder output = new StringBuilder();
            output.Append("   ");
            for(int i = 0; i < m_Size; i++)
            {
                output.Append(ColIndex + "   ");
                ColIndex++;
            }

            return output;
        }

        private StringBuilder createLineSeperator()
        {
            StringBuilder LineSeperator = new StringBuilder();
            LineSeperator.Append(' ');
            for (int i = 0; i < (m_Size * 4) + 1; i++)
            {
                LineSeperator.Append('=');
            }

            return LineSeperator;
        }

        private int getNumberOfCheckersLines(int i_Size)
        {
            int output = 0;

            if(i_Size == 6)
            {
                output = 2;
            }

            if(i_Size == 8)
            {
                output = 3;
            }

            if(i_Size == 10)
            {
                output = 4;
            }

            return output;
        }
    }
}