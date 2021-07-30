using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Calculator
{
    public partial class Form1 : Form
    {

        bool _isNewEntry, _isInfinityException, _isRepeatLastOperation;
        double _dblResult, _dblOperand;
        char _chPreviousOperator;

        public Form1()
        {
            InitializeComponent();
        }

        private void ClearOperator(object sender, EventArgs e)
        {
            _isInfinityException = false;
            resultLbl.Text = "0";
        }
        private void ChangeSign(object sender, EventArgs e)
        {
            if (!_isInfinityException)
                resultLbl.Text = (double.Parse(resultLbl.Text) * -1).ToString();
        }
        private void OperatorFound(object sender, EventArgs e)
        {
            Guna2TileButton b = sender as Guna2TileButton;
            if (!_isInfinityException)
            {
                if (_chPreviousOperator == '\0')
                {
                    _chPreviousOperator = (b).Text[0];
                    _dblResult = double.Parse(resultLbl.Text);
                }
                else if (_isNewEntry)
                    _chPreviousOperator = (b).Text[0];
                else
                {
                    Operate(_dblResult, _chPreviousOperator, double.Parse(resultLbl.Text));
                    _chPreviousOperator = (b).Text[0];
                }
                _isNewEntry = true;
                _isRepeatLastOperation = false;
            }
        }
        void Operate(double dblPreviousResult, char chPreviousOperator, double dblOperand)
        {
            switch (chPreviousOperator)
            {
                case '+':
                    resultLbl.Text = (_dblResult = (dblPreviousResult + dblOperand)).ToString();
                    break;
                case '-':
                    resultLbl.Text = (_dblResult = (dblPreviousResult - dblOperand)).ToString();
                    break;
                case '*':
                    resultLbl.Text = (_dblResult = (dblPreviousResult * dblOperand)).ToString();
                    break;
                case '/':
                    if (dblOperand == 0)
                    {
                        resultLbl.Text = "Cannot divide by zero";
                        _isInfinityException = true;
                    }
                    else
                        resultLbl.Text = (_dblResult = (dblPreviousResult / dblOperand)).ToString();
                    break;
            }
        }
        private void Equals(object sender, EventArgs e)
        {
            if (!_isInfinityException)
            {
                if (!_isRepeatLastOperation)
                {
                    _dblOperand = double.Parse(resultLbl.Text);
                    _isRepeatLastOperation = true;
                }
                Operate(_dblResult, _chPreviousOperator, _dblOperand);
                _isNewEntry = true;
            }
        }
        private void UpdateSign(object sender, EventArgs e)
        {
            Guna2TileButton b = sender as Guna2TileButton;
            switch (_isInfinityException)
            {
                case false:
                    {
                        switch (_isNewEntry)
                        {
                            case true:
                                resultLbl.Text = "0";
                                _isNewEntry = false;
                                break;
                        }
                        switch (_isRepeatLastOperation)
                        {
                            case true:
                                _chPreviousOperator = '\0';
                                _dblResult = 0;
                                break;
                        }
                        switch (resultLbl.Text == "0" && b == btn0)
                        {
                            case false when !(b == dotBtn && resultLbl.Text.Contains(".")):
                                resultLbl.Text = resultLbl.Text == "0" && b == dotBtn ? "0." : resultLbl.Text == "0" ? (b).Text : resultLbl.Text + (b).Text;
                                break;
                        }
                        break;
                    }
            }
        }
        private void ClearAll(object sender, EventArgs e)
        {
            _isInfinityException = _isRepeatLastOperation = false;
            _dblOperand = _dblResult = 0; resultLbl.Text = "0";
            _isNewEntry = true;
            _chPreviousOperator = '\0';
        }
        private void minBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
