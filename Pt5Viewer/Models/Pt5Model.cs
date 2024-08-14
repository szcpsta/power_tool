using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Parsers;

namespace Pt5Viewer.Models
{
    public class Pt5Model
    {
        public double TimeScaleMax = 60_000;    // s

        public DateTime CaptureDate = DateTime.Now;

        public Pt5Parser parser;

        public bool SetParser(string pt5FilePath)
        {
            return Pt5Parser.TryParse(pt5FilePath, out parser);
        }
    }
}