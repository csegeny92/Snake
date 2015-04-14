using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_TZWKTT
{
    public partial class ScoreLists
    {
        public ScoreLists(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
       


        int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        string name;

        public string Name1
        {
            get { return name; }
            set { name = value; }
        }
        public override string ToString()
        {
            return name +" "+ score;
        }

    }
}
