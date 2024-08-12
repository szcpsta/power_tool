using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Enums;

namespace Pt5Viewer.Common
{
    public interface IScaleSync
    {
        void UpdateTimeScale(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks);

        void UpdateTimeOffset(double offset);

        void UpdateCurrentScale(string unit, double unitsPerTick, int numberOfTicks);

        void UpdateCurrentOffset(double offset);

        void UpdateDisplayFormat(bool isDisplayInTimeFormat);
    }
}
