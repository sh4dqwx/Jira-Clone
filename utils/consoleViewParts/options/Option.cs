﻿using System;
using System.Text;

namespace JiraClone.utils.consoleViewParts.options
{
    public abstract class Option : Printable, IOption
    {
        private bool _selected;
        private string _error;
        private readonly string _name;
        public bool Selected { get => _selected; set { _selected = value; Print(); } }
        public string Error { get => _error; set => _error = value; }
        public string Name { get => _name; }

        public Option(string name) : base()
        {
            _name = name;
            _error = "";
        }

        public abstract void UseKey(char c);
	}
}
