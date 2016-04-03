using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeNET;

namespace TicTacToeUI
{
    public partial class Form1 : Form
    {
        State blankBoard = new State();
          

        private void Run(Move inputMove)
        {
            
                blankBoard.currentPlayer = 1;
                blankBoard.UpdateBoard(inputMove);
                if (blankBoard.Win())
                {
                    var but =MessageBox.Show("You win", "Congratulations!!", MessageBoxButtons.RetryCancel);
                    if (but == DialogResult.Retry)
                    {
                        Form1 f = new Form1();
                        f.Show();
                        this.Hide();
                    }
                    else
                        return;
                    //break;
                }
                blankBoard.currentPlayer = 2;
                blankBoard.nextPlayer = 1;
                var newMoveFromAI = Service.MiniMax(blankBoard);
                if (newMoveFromAI.m.x == -1)
                {
                    var but = MessageBox.Show("It's a draw. Try Again?", "Oops!!", MessageBoxButtons.RetryCancel);
                    if (but == DialogResult.Retry)
                    {
                        Form1 f = new Form1();
                        f.Show();
                        this.Hide();
                    }
                    else
                        return;
                    //MessageBox.Show("DRAW");
                    return;
                    //break;
                }
                blankBoard.UpdateBoard(new Move() { x = newMoveFromAI.m.x, y = newMoveFromAI.m.y });
                DisableButton(newMoveFromAI.m.x, newMoveFromAI.m.y);
                //blankBoard.Print();

                if (blankBoard.Win())
                {
                    var but = MessageBox.Show("You lost. Try Again?", "Oops!!", MessageBoxButtons.RetryCancel);
                    if (but == DialogResult.Retry)
                    {
                        Form1 f = new Form1();
                        f.Show();
                        this.Hide();
                    }
                    else
                        return;
                    //MessageBox.Show("Computer wins");

                }

            
        }

        private void DisableButton(int p1, int p2)
        {
            if (p1 == 0 && p2 == 0) { button1.Text = "X"; button1.Enabled = false; }
            if (p1 == 0 && p2 == 1) { button2.Text = "X"; button2.Enabled = false; }
            if (p1 == 0 && p2 == 2) { button3.Text = "X"; button3.Enabled = false; }
            if (p1 == 1 && p2 == 0) { button4.Text = "X"; button4.Enabled = false; }
            if (p1 == 1 && p2 == 1) { button5.Text = "X"; button5.Enabled = false; }
            if (p1 == 1 && p2 == 2) { button6.Text = "X"; button6.Enabled = false; }
            if (p1 == 2 && p2 == 0) { button7.Text = "X"; button7.Enabled = false; }
            if (p1 == 2 && p2 == 1) { button8.Text = "X"; button8.Enabled = false; }
            if (p1 == 2 && p2 == 2) { button9.Text = "X"; button9.Enabled = false; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 0, y = 0 });
            this.button1.Text = "O";
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 0, y = 1 });
            this.button2.Text = "O";
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 0, y = 2 });
            this.button3.Text = "O";
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 1, y = 0 });
            this.button4.Text = "O";
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 1, y = 1 });
            this.button5.Text = "O";
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 1, y = 2 });
            this.button6.Text = "O";
            button6.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 2, y = 0 });
            this.button7.Text = "O";
            button7.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 2, y = 1 });
            this.button8.Text = "O";
            button8.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Run(new Move() { x = 2, y = 2 });
            this.button9.Text = "O";
            button9.Enabled = false;
        }
    }
}
