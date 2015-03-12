﻿using System;
using System.Windows.Controls;
using System.Windows.Input;


namespace System.Windows.Controls
{
    public class NumberTextBox : TextBox
    {
        #region Constructors
        /// <summary> 
        /// The default constructor
        /// </summary>
        public NumberTextBox()
        {
            TextChanged += new TextChangedEventHandler(OnTextChanged);
            KeyDown += new KeyEventHandler(OnKeyDown);
        }
        #endregion

        #region Properties
        new public String Text
        {
            get { return base.Text; }
            set
            {
                base.Text = LeaveOnlyNumbers(value);
            }
        }

        #endregion

        #region Functions
        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsActionKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab || inKey == Key.Return || Keyboard.Modifiers.HasFlag(ModifierKeys.Alt);
        }

        private string LeaveOnlyNumbers(String inString)
        {
            String tmp = inString;
            foreach (char c in inString.ToCharArray())
            {
                if (!IsDigit(c))
                {
                    tmp = tmp.Replace(c.ToString(), "");
                }
            }
            return tmp;
        }

        public bool IsDigit(char c)
        {
            return (c >= '0' && c <= '9');
        }
        #endregion

        #region Event Functions
        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsActionKey(e.Key);
        }

        protected void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            base.Text = LeaveOnlyNumbers(Text);
            try
            {
                if (Convert.ToInt32(base.Text) > 255)
                {
                    base.Text = "255";
                }
            }catch(FormatException)
            {}
        }
        #endregion
    }
}   