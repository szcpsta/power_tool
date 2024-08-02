using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Common
{
    public interface IScaleSync
    {
        void UpdateTimeScale(string unit, int unitsPerTick, int numberOfTicks);

        void UpdateTimeOffset(double offset);

        void UpdateCurrentScale(string unit, int unitsPerTick, int numberOfTicks);

        void UpdateCurrentOffset(double offset);
    }
}
