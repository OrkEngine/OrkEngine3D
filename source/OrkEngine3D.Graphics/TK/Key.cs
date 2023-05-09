//
// Keys.cs
//
// Copyright (C) 2019 OpenTK
//
// This software may be modified and distributed under the terms
// of the MIT license. See the LICENSE file for details.
//

namespace OrkEngine3D.Graphics.TK
{
    /// <summary>
    /// Specifies Key codes and modifiers in US keyboard layout.
    /// </summary>
    public enum Key : int
    {
        /// <summary>
        /// An unknown Key.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// The spacebar Key.
        /// </summary>
        Space = 32,

        /// <summary>
        /// The apostrophe Key.
        /// </summary>
        Apostrophe = 39 /* ' */,

        /// <summary>
        /// The comma Key.
        /// </summary>
        Comma = 44 /* , */,

        /// <summary>
        /// The minus Key.
        /// </summary>
        Minus = 45 /* - */,

        /// <summary>
        /// The period Key.
        /// </summary>
        Period = 46 /* . */,

        /// <summary>
        /// The slash Key.
        /// </summary>
        Slash = 47 /* / */,

        /// <summary>
        /// The 0 Key.
        /// </summary>
        D0 = 48,

        /// <summary>
        /// The 1 Key.
        /// </summary>
        D1 = 49,

        /// <summary>
        /// The 2 Key.
        /// </summary>
        D2 = 50,

        /// <summary>
        /// The 3 Key.
        /// </summary>
        D3 = 51,

        /// <summary>
        /// The 4 Key.
        /// </summary>
        D4 = 52,

        /// <summary>
        /// The 5 Key.
        /// </summary>
        D5 = 53,

        /// <summary>
        /// The 6 Key.
        /// </summary>
        D6 = 54,

        /// <summary>
        /// The 7 Key.
        /// </summary>
        D7 = 55,

        /// <summary>
        /// The 8 Key.
        /// </summary>
        D8 = 56,

        /// <summary>
        /// The 9 Key.
        /// </summary>
        D9 = 57,

        /// <summary>
        /// The semicolon Key.
        /// </summary>
        Semicolon = 59 /* ; */,

        /// <summary>
        /// The equal Key.
        /// </summary>
        Equal = 61 /* = */,

        /// <summary>
        /// The A Key.
        /// </summary>
        A = 65,

        /// <summary>
        /// The B Key.
        /// </summary>
        B = 66,

        /// <summary>
        /// The C Key.
        /// </summary>
        C = 67,

        /// <summary>
        /// The D Key.
        /// </summary>
        D = 68,

        /// <summary>
        /// The E Key.
        /// </summary>
        E = 69,

        /// <summary>
        /// The F Key.
        /// </summary>
        F = 70,

        /// <summary>
        /// The G Key.
        /// </summary>
        G = 71,

        /// <summary>
        /// The H Key.
        /// </summary>
        H = 72,

        /// <summary>
        /// The I Key.
        /// </summary>
        I = 73,

        /// <summary>
        /// The J Key.
        /// </summary>
        J = 74,

        /// <summary>
        /// The K Key.
        /// </summary>
        K = 75,

        /// <summary>
        /// The L Key.
        /// </summary>
        L = 76,

        /// <summary>
        /// The M Key.
        /// </summary>
        M = 77,

        /// <summary>
        /// The N Key.
        /// </summary>
        N = 78,

        /// <summary>
        /// The O Key.
        /// </summary>
        O = 79,

        /// <summary>
        /// The P Key.
        /// </summary>
        P = 80,

        /// <summary>
        /// The Q Key.
        /// </summary>
        Q = 81,

        /// <summary>
        /// The R Key.
        /// </summary>
        R = 82,

        /// <summary>
        /// The S Key.
        /// </summary>
        S = 83,

        /// <summary>
        /// The T Key.
        /// </summary>
        T = 84,

        /// <summary>
        /// The U Key.
        /// </summary>
        U = 85,

        /// <summary>
        /// The V Key.
        /// </summary>
        V = 86,

        /// <summary>
        /// The W Key.
        /// </summary>
        W = 87,

        /// <summary>
        /// The X Key.
        /// </summary>
        X = 88,

        /// <summary>
        /// The Y Key.
        /// </summary>
        Y = 89,

        /// <summary>
        /// The Z Key.
        /// </summary>
        Z = 90,

        /// <summary>
        /// The left bracket(opening bracket) Key.
        /// </summary>
        LeftBracket = 91 /* [ */,

        /// <summary>
        /// The backslash.
        /// </summary>
        Backslash = 92 /* \ */,

        /// <summary>
        /// The right bracket(closing bracket) Key.
        /// </summary>
        RightBracket = 93 /* ] */,

        /// <summary>
        /// The grave accent Key.
        /// </summary>
        GraveAccent = 96 /* ` */,

        /// <summary>
        /// The escape Key.
        /// </summary>
        Escape = 256,

        /// <summary>
        /// The enter Key.
        /// </summary>
        Enter = 257,

        /// <summary>
        /// The tab Key.
        /// </summary>
        Tab = 258,

        /// <summary>
        /// The backspace Key.
        /// </summary>
        Backspace = 259,

        /// <summary>
        /// The insert Key.
        /// </summary>
        Insert = 260,

        /// <summary>
        /// The delete Key.
        /// </summary>
        Delete = 261,

        /// <summary>
        /// The right arrow Key.
        /// </summary>
        Right = 262,

        /// <summary>
        /// The left arrow Key.
        /// </summary>
        Left = 263,

        /// <summary>
        /// The down arrow Key.
        /// </summary>
        Down = 264,

        /// <summary>
        /// The up arrow Key.
        /// </summary>
        Up = 265,

        /// <summary>
        /// The page up Key.
        /// </summary>
        PageUp = 266,

        /// <summary>
        /// The page down Key.
        /// </summary>
        PageDown = 267,

        /// <summary>
        /// The home Key.
        /// </summary>
        Home = 268,

        /// <summary>
        /// The end Key.
        /// </summary>
        End = 269,

        /// <summary>
        /// The caps lock Key.
        /// </summary>
        CapsLock = 280,

        /// <summary>
        /// The scroll lock Key.
        /// </summary>
        ScrollLock = 281,

        /// <summary>
        /// The num lock Key.
        /// </summary>
        NumLock = 282,

        /// <summary>
        /// The print screen Key.
        /// </summary>
        PrintScreen = 283,

        /// <summary>
        /// The pause Key.
        /// </summary>
        Pause = 284,

        /// <summary>
        /// The F1 Key.
        /// </summary>
        F1 = 290,

        /// <summary>
        /// The F2 Key.
        /// </summary>
        F2 = 291,

        /// <summary>
        /// The F3 Key.
        /// </summary>
        F3 = 292,

        /// <summary>
        /// The F4 Key.
        /// </summary>
        F4 = 293,

        /// <summary>
        /// The F5 Key.
        /// </summary>
        F5 = 294,

        /// <summary>
        /// The F6 Key.
        /// </summary>
        F6 = 295,

        /// <summary>
        /// The F7 Key.
        /// </summary>
        F7 = 296,

        /// <summary>
        /// The F8 Key.
        /// </summary>
        F8 = 297,

        /// <summary>
        /// The F9 Key.
        /// </summary>
        F9 = 298,

        /// <summary>
        /// The F10 Key.
        /// </summary>
        F10 = 299,

        /// <summary>
        /// The F11 Key.
        /// </summary>
        F11 = 300,

        /// <summary>
        /// The F12 Key.
        /// </summary>
        F12 = 301,

        /// <summary>
        /// The F13 Key.
        /// </summary>
        F13 = 302,

        /// <summary>
        /// The F14 Key.
        /// </summary>
        F14 = 303,

        /// <summary>
        /// The F15 Key.
        /// </summary>
        F15 = 304,

        /// <summary>
        /// The F16 Key.
        /// </summary>
        F16 = 305,

        /// <summary>
        /// The F17 Key.
        /// </summary>
        F17 = 306,

        /// <summary>
        /// The F18 Key.
        /// </summary>
        F18 = 307,

        /// <summary>
        /// The F19 Key.
        /// </summary>
        F19 = 308,

        /// <summary>
        /// The F20 Key.
        /// </summary>
        F20 = 309,

        /// <summary>
        /// The F21 Key.
        /// </summary>
        F21 = 310,

        /// <summary>
        /// The F22 Key.
        /// </summary>
        F22 = 311,

        /// <summary>
        /// The F23 Key.
        /// </summary>
        F23 = 312,

        /// <summary>
        /// The F24 Key.
        /// </summary>
        F24 = 313,

        /// <summary>
        /// The F25 Key.
        /// </summary>
        F25 = 314,

        /// <summary>
        /// The 0 Key on the Key pad.
        /// </summary>
        KeyPad0 = 320,

        /// <summary>
        /// The 1 Key on the Key pad.
        /// </summary>
        KeyPad1 = 321,

        /// <summary>
        /// The 2 Key on the Key pad.
        /// </summary>
        KeyPad2 = 322,

        /// <summary>
        /// The 3 Key on the Key pad.
        /// </summary>
        KeyPad3 = 323,

        /// <summary>
        /// The 4 Key on the Key pad.
        /// </summary>
        KeyPad4 = 324,

        /// <summary>
        /// The 5 Key on the Key pad.
        /// </summary>
        KeyPad5 = 325,

        /// <summary>
        /// The 6 Key on the Key pad.
        /// </summary>
        KeyPad6 = 326,

        /// <summary>
        /// The 7 Key on the Key pad.
        /// </summary>
        KeyPad7 = 327,

        /// <summary>
        /// The 8 Key on the Key pad.
        /// </summary>
        KeyPad8 = 328,

        /// <summary>
        /// The 9 Key on the Key pad.
        /// </summary>
        KeyPad9 = 329,

        /// <summary>
        /// The decimal Key on the Key pad.
        /// </summary>
        KeyPadDecimal = 330,

        /// <summary>
        /// The divide Key on the Key pad.
        /// </summary>
        KeyPadDivide = 331,

        /// <summary>
        /// The multiply Key on the Key pad.
        /// </summary>
        KeyPadMultiply = 332,

        /// <summary>
        /// The subtract Key on the Key pad.
        /// </summary>
        KeyPadSubtract = 333,

        /// <summary>
        /// The add Key on the Key pad.
        /// </summary>
        KeyPadAdd = 334,

        /// <summary>
        /// The enter Key on the Key pad.
        /// </summary>
        KeyPadEnter = 335,

        /// <summary>
        /// The equal Key on the Key pad.
        /// </summary>
        KeyPadEqual = 336,

        /// <summary>
        /// The left shift Key.
        /// </summary>
        LeftShift = 340,

        /// <summary>
        /// The left control Key.
        /// </summary>
        LeftControl = 341,

        /// <summary>
        /// The left alt Key.
        /// </summary>
        LeftAlt = 342,

        /// <summary>
        /// The left super Key.
        /// </summary>
        LeftSuper = 343,

        /// <summary>
        /// The right shift Key.
        /// </summary>
        RightShift = 344,

        /// <summary>
        /// The right control Key.
        /// </summary>
        RightControl = 345,

        /// <summary>
        /// The right alt Key.
        /// </summary>
        RightAlt = 346,

        /// <summary>
        /// The right super Key.
        /// </summary>
        RightSuper = 347,

        /// <summary>
        /// The menu Key.
        /// </summary>
        Menu = 348,

        /// <summary>
        /// The last valid Key in this enum.
        /// </summary>
        LastKey = Menu
    }
}