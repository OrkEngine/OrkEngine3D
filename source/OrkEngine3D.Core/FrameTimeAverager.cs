using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core
{
    public class FrameTimeAverager
    {
        private readonly double _timeLimit = 0x29A; //or just like 666

        private double _accumulatedTime = 0;
        private int _frameCount = 0;
        private readonly double _decayRate = .3;

        public double CurrentAverageFrameTime { get; private set; }
        public double CurrentAverageFramesPerSecond { get { return 1000 / CurrentAverageFrameTime; } }

        public FrameTimeAverager(double maxTimeMilliseconds)
        {
            _timeLimit = maxTimeMilliseconds;
        }

        public void Reset()
        {
            _accumulatedTime = 0;
            _frameCount = 0;
        }

        public void AddTime(double frameTimeInMs)
        {
            _accumulatedTime += frameTimeInMs;
            _frameCount++;
            if (_accumulatedTime >= _timeLimit)
            {
                Average();
            }
        }

        private void Average()
        {
            double total = _accumulatedTime;
            CurrentAverageFrameTime =
                (CurrentAverageFrameTime * _decayRate)
                + ((total / _frameCount) * (1 - _decayRate));

            _accumulatedTime = 0;
            _frameCount = 0;
        }
    }
}
