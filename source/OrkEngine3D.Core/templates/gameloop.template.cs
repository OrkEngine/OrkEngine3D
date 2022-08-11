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

using System;

namespace OrkEngine3D.Core.templates;

/// <summary>
/// Inherits from IGame(or IOrkGame) and IOrk<etc>
/// This is class that people put inside of there code to reference from as game loop goes
/// </summary>
public class OrkGame
{
    /// <summary>
    /// On before load, so when window opens up. (Semi used?)
    /// </summary>
    public void OnBeforeLoad()
    {

    }

    /// <summary>
    /// On load, during time most stuff loads. (Always used?)
    /// </summary>
    public void OnLoad()
    {

    }

    /// <summary>
    /// On After load, after most stuff is loaded. (Semi used?)
    /// </summary>
    public void OnAfterLoad()
    {

    }

    /// <summary>
    /// Current update
    /// </summary>
    public void OnUpdate()
    {

    }

    /// <summary>
    /// Fixed update
    /// </summary>
    public void OnFixedUpdate()
    {

    }

    /// <summary>
    /// Late update
    /// </summary>
    public void OnLateUpdate()
    {

    }

    /// <summary>
    /// On Timespan update
    /// </summary>
    public void OnTSUpdate(TimeSpan span)
    {

    }

    public void OnCleanup(CleanupTime ct)
    {

    }

    /// <summary>
    /// Clean up code using enum shows another way to do same thing.
    /// </summary>
    /// <param name="ct">Cleanup enum</param>
    /// <param name="disposeAutomatically">Does user handle when stuff is disposed or not</param>
    public void OnCleanup(CleanupTime ct, bool disposeAutomatically)
    {

    }
}
//temp class
public enum CleanupTime
{
    Before,
    Current, //During
    After
}
