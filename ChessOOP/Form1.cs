using System;

namespace ChessOOP
{
    public partial class Form1 : Form
    {
        Button[,] buttons = new Button[8, 8];

        ChessField chessField = new();

        Dictionary<Button,Figure?> figureOnButton = new();

        List<Button> possibleMovesButtons = new();

        Dictionary<Button,(int,int)> buttonsPositions = new();

        Button? selectedFigureButton = null;

        public Form1()
        {
            InitializeComponent();
            InitializeLabels();
            InitializeGameField();
        }

        public void InitializeLabels()
        {
            for (int i = 0; i < 8; i++)
            {
                var lit = new Label();
                var lit_bottom = new Label();
                lit.Location = new Point(170 + 200 * i, 50);
                lit_bottom.Location = new Point(170 + 200 * i, 50 + 1650);
                Font f = new("Times new Roman", 14, FontStyle.Bold);
                lit.Font = f;
                lit_bottom.Font = f;
                lit.Size = lit_bottom.Size = new Size(100, 100);
                lit.AutoSize = lit_bottom.AutoSize = true;
                lit.Text = lit_bottom.Text = ((char)(97 + i)).ToString().ToUpper();
                Controls.Add(lit);
                Controls.Add(lit_bottom);
                
                var lit_left = new Label();
                var lit_right = new Label();
                lit_left.Location = new Point(50, 170 + 200 * i);
                lit_right.Location = new Point(50 + 1650, 170 + 200 * i);
                lit_left.Font = f;
                lit_right.Font = f;
                lit_left.Size = lit_right.Size = new Size(100, 100);
                lit_left.AutoSize = lit_right.AutoSize = true;
                lit_left.Text = lit_right.Text = (8 - i).ToString();
                Controls.Add(lit_left);
                Controls.Add(lit_right);
            }
        }

        private void InitializeGameField()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var b = buttons[i, j] = new Button();
                    buttonsPositions[b] = (i, j);
                    b.Location = new Point(100 + 200 * j, 100 + 200 * i);
                    b.Name = i.ToString() + j.ToString();
                    b.Size = new Size(200, 200);
                    PaintButton(b);
                    var field = chessField[i, j];
                    if (field != null)
                    { 
                        b.BackgroundImage = field.Img;
                        field.CurrentPosition = (i, j);
                    }
                    figureOnButton[b] = field;
                    Controls.Add(b);
                    b.Click += buttonClick;
                }
            }
        }

        private void PaintButton(Button b)
        {
            var p = buttonsPositions[b];
            b.BackColor = Color.DarkGray;
            if (p.Item1 % 2 == 0 && p.Item2 % 2 == 0)
                b.BackColor = Color.White;
            if (p.Item1 % 2 != 0 && p.Item2 % 2 != 0)
                b.BackColor = Color.White;
        }

        private void UpdateGameField()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var b = buttons[i, j];
                    var field = chessField[i, j];
                    if (field is null)
                    {
                        b.BackgroundImage = null;
                    }
                    if (field != null)
                    { 
                        b.BackgroundImage = field.Img;
                        field.CurrentPosition = (i, j);
                    }
                    figureOnButton[b] = field;
                }
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {
            var s = (Button)sender;
            //—тереть возможные ходы и отменить выбор фигуры
            if (figureOnButton[s] is Figure && figureOnButton[s].player == chessField.CurrentPlayer && s == selectedFigureButton && possibleMovesButtons.Count > 0)
            {
                ErasePossibleMovesButtons();
            }
            //¬ыбрать фигуру и показать возможные ходы
            else if (figureOnButton[s] is Figure && figureOnButton[s].player == chessField.CurrentPlayer && selectedFigureButton == null)
            {
                selectedFigureButton = s;
                SeePossibleMovesButtons(s);
            }
            //¬ыбрать другую фигуру, показать возможные ходы
            else if (figureOnButton[s] is Figure && figureOnButton[s].player == chessField.CurrentPlayer && selectedFigureButton != null)
            {
                ErasePossibleMovesButtons();
                selectedFigureButton = s;
                SeePossibleMovesButtons(s);
            }
            //—делать ход, стереть возможные ходы, проверить мат
            else if ((figureOnButton[s] == null || figureOnButton[s].player != chessField.CurrentPlayer) && possibleMovesButtons.Contains(s) && selectedFigureButton != null)
            {
                figureOnButton[selectedFigureButton].MakeMove(buttonsPositions[s], chessField);
                if (figureOnButton[selectedFigureButton].IsGameOver(chessField))
                {
                    var gameOverForm = new GameOverForm();
                    gameOverForm.Show();
                }
                ErasePossibleMovesButtons();
            }
            //—тереть возможные ходы нажатием на пустое место или не атакуемую фигуру противника
            else if ((figureOnButton[s] == null || figureOnButton[s].player != chessField.CurrentPlayer) && !possibleMovesButtons.Contains(s) && selectedFigureButton != null)
            {
                ErasePossibleMovesButtons();
            }
            UpdateGameField();
        }

        private void SeePossibleMovesButtons(Button b)
        {
            var figureMoving = figureOnButton[b];
            var legalMoves = figureMoving.GetMoves(chessField);

            legalMoves.ForEach(move =>
            {
                possibleMovesButtons.Add(buttons[move.Item1, move.Item2]);
                buttons[move.Item1, move.Item2].BackColor = Color.Green;
            });
            b.BackColor = Color.Yellow;
        }

        private void ErasePossibleMovesButtons()
        {
            foreach (var button in possibleMovesButtons)
            {
                PaintButton(button);
            }
            PaintButton(selectedFigureButton);
            possibleMovesButtons = new();
        }
    }
}