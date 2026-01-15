using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIKA_CPS_V1.Codeplug
{
    public class Channel
    {
        private string _name;
        private uint _rxFrequency;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public uint RxFrequency
        {
            get => _rxFrequency;
            set => _rxFrequency = value;
        }
        public Channel()
        {
            _name = "Вызывной 2 м";
            _rxFrequency = 145500000;
        }

        public Channel (string name, uint rxFrequency)
        {
            _name = name;
            _rxFrequency = rxFrequency;
        }
    }
}
