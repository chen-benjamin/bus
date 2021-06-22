using System;
using System.Collections.Generic;
using System.Text;

namespace bus.Model
{
    public class Bus
    {
        public string Title { get; set; }
        public List<string> Times = new List<string>();
        public string Next {
            get {
                if (Times.Count > 0) {
                    return Times[0];
                }
                return null;
            }
        }
    }
}
