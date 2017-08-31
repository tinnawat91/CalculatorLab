﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private bool isTwotime;
        private string firstOperand;
        private string operate;
        private string operate2;
        private CalculatorEngine engine;

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            isTwotime = false;
        }
        
        public MainForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater&&isTwotime)
            {
                lblDisplay.Text = "0";
            }
            
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            if (isTwotime)
            {
                string secondOperand = lblDisplay.Text;
                string result = engine.Calculate(operate, firstOperand, secondOperand);
                if (((Button)sender).Text == "%")
                {
                    operate2 = ((Button)sender).Text;
                }
                else
                {
                    operate = ((Button)sender).Text;
                }
                if (operate2 == "%")
                {
                    secondOperand = engine.Calculate(operate2, firstOperand,secondOperand);
                    result = engine.Calculate(operate, firstOperand, secondOperand);
                    lblDisplay.Text = lblDisplay.Text;
                }
                if (operate2 != "%")
                {
                    firstOperand = result;
                }
                lblDisplay.Text = result;
                
                isAfterOperater = true;
                return;
            }
            operate = ((Button)sender).Text;
            
            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    break;
                case "%":
                    /// your code here
                    break;
            }
            isAllowBack = false;
            isTwotime = true;
        }

        private string Sqrt(string firstOperand)
        {
            throw new NotImplementedException();
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = engine.Calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
            isTwotime = false;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void btn1divideX_Click(object sender, EventArgs e)
        {
            int maxout = 8;
            int remainlengt;
            string[] part;
            firstOperand = lblDisplay.Text;
            double result = (1 / (Convert.ToDouble(firstOperand)));
            part = result.ToString().Split('.');
            
            remainlengt = maxout - part[0].Length - 1;
            lblDisplay.Text =result.ToString("N"+remainlengt) ;
        }

        private void btnsquare_Click(object sender, EventArgs e)
        {
            double num;
            firstOperand = lblDisplay.Text;
            num = Convert.ToDouble(firstOperand);
            double result = Math.Sqrt(num);
            lblDisplay.Text = Convert.ToString(result);
        }
    }
}
