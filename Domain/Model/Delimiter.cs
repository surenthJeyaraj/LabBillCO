using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public enum DelimiterType
    {
        Default = 1,
        Custom = 2
    }
    public struct Delimiter
    {
        private DelimiterType _delimiter;
        public DelimiterType DelimiterType
        {
            get { return _delimiter; }
            set { _delimiter = value; }
        }
        private char _segementSeperator;
        public char SegementSeperator
        {
            get { return _segementSeperator; }
            set { _segementSeperator = value; }
        }

        private char _elementSeperator;
        public char ElementSeperator
        {
            get { return _elementSeperator; }
            set { _elementSeperator = value; }
        }

        private char _compositeSeperator;
        private char _repetitionSeperator;

        public char CompositeSeperator
        {
            get { return _compositeSeperator; }
            set { _compositeSeperator = value; }
        }

        public char RepetitionSeperator
        {
            get
            {
                return _repetitionSeperator;
            }
            set
            {
                _repetitionSeperator = value;
            }
        }

        public Delimiter(char segementSeperator, char elementSeperator, char compositeSeperator, char repetitionSeperator, DelimiterType delimiterType)
        {
            _segementSeperator = segementSeperator;
            _elementSeperator = elementSeperator;
            _compositeSeperator = compositeSeperator;
            _repetitionSeperator = repetitionSeperator;
            _delimiter = delimiterType;
        }
        public Delimiter(string segementSeperator, string elementSeperator, string componentSeperator, string repetitionSeperator)
            : this()
        {
            _segementSeperator = segementSeperator.ToCharArray()[0];
            _elementSeperator = elementSeperator.ToCharArray()[0];
            _compositeSeperator = componentSeperator.ToCharArray()[0];
            _repetitionSeperator = repetitionSeperator.ToCharArray()[0];
        }
        public Delimiter(string segementSeperator, string elementSeperator, string componentSeperator)
            : this()
        {
            _segementSeperator = segementSeperator.ToCharArray()[0];
            _elementSeperator = elementSeperator.ToCharArray()[0];
            _compositeSeperator = componentSeperator.ToCharArray()[0];
            _repetitionSeperator = '^';
        }
    }
}
