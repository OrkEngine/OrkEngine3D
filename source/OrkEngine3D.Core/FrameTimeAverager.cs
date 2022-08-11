/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace OrkEngine3D.Core;

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
