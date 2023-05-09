/*
* Copyright (c) 2007-2010 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
* ------------------------------------------------------------------------------
* Licensed under the MIT/X11 license.
* Copyright (c) 2006-2008 the OpenTK Team.
* This notice may not be removed from any source distribution.
* See license.txt for licensing detailed licensing details.
*
* Contributions by Andy Gill, James Talton and Georg Wächter.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;


namespace OrkEngine3D.Mathematics;

/// <summary>
/// Contains mathmeatical methods and constants that extend the System.Math class.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// A value specifying the approximation of π which is 180 degrees.
    /// </summary>
    public const float Pi = 3.141592653589793239f;

    /// <summary>
    /// Defines the value of Pi divided by two as a <see cref="float"/>.
    /// </summary>
    public const float PiOver2 = Pi / 2;

    /// <summary>
    /// Defines the value of Pi divided by three as a <see cref="float"/>.
    /// </summary>
    public const float PiOver3 = Pi / 3;

    /// <summary>
    /// Defines the value of  Pi divided by four as a <see cref="float"/>.
    /// </summary>
    public const float PiOver4 = Pi / 4;

    /// <summary>
    /// Defines the value of Pi divided by six as a <see cref="float"/>.
    /// </summary>
    public const float PiOver6 = Pi / 6;

    /// <summary>
    /// Defines the value of Pi multiplied by two as a <see cref="float"/>.
    /// </summary>
    public const float TwoPi = 2 * Pi;

    /// <summary>
    /// Defines the value of Pi multiplied by 3 and divided by two as a <see cref="float"/>.
    /// </summary>
    public const float ThreePiOver2 = 3 * Pi / 2;

    /// <summary>
    /// Defines the value of E as a <see cref="float"/>.
    /// </summary>
    public const float E = 2.7182817f;

    /// <summary>
    /// Defines the base-10 logarithm of E.
    /// </summary>
    public const float Log10E = 0.4342945f;

    /// <summary>
    /// Defines the base-2 logarithm of E.
    /// </summary>
    public const float Log2E = 1.442695f;

    /// <summary>
    /// Clamps a number between a minimum and a maximum.
    /// </summary>
    /// <param name="n">The number to clamp.</param>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <returns>min, if n is lower than min; max, if n is higher than max; n otherwise.</returns>
    public static int Clamp(int n, int min, int max)
    {
        return Math.Max(Math.Min(n, max), min);
    }

    /// <summary>
    /// Clamps a number between a minimum and a maximum.
    /// </summary>
    /// <param name="n">The number to clamp.</param>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <returns>min, if n is lower than min; max, if n is higher than max; n otherwise.</returns>
    public static float Clamp(float n, float min, float max)
    {
        return Math.Max(Math.Min(n, max), min);
    }

    /// <summary>
    /// Clamps a number between a minimum and a maximum.
    /// </summary>
    /// <param name="n">The number to clamp.</param>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <returns>min, if n is lower than min; max, if n is higher than max; n otherwise.</returns>
    public static double Clamp(double n, double min, double max)
    {
        return Math.Max(Math.Min(n, max), min);
    }


    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    /// <param name="degree">The value to convert.</param>
    /// <returns>The converted value.</returns>
    public static float DegreesToRadians(float degree)
    {
        return degree * (Pi / 180.0f);
    }

    /// <summary>
    /// Converts radians to degrees.
    /// </summary>
    /// <param name="radian">The value to convert.</param>
    /// <returns>The converted value.</returns>
    public static float RadiansToDegrees(float radian)
    {
        return radian * (180.0f / Pi);
    }
}
