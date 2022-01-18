// Copyright (c) Silk.NET
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Silk.NET.Core.Loader;

namespace OrkEngine3D.Graphics.OpenGL
{
    /// <summary>
    /// Contains the library name of OPENGL.
    /// </summary>
    internal class GLCoreLibraryNameContainer : SearchPathContainer
    {
        public override string Windows64 => "opengl32.dll";

        public override string Windows86 => "opengl32.dll";

        public override string Linux => "libGL.so.1";

        public override string MacOS => "/System/Library/Frameworks/OpenGL.framework/OpenGL";

        public override string IOS => "/System/Library/Frameworks/OpenGL.framework/OpenGL";

        public override string Android => "libGL.so.1";
    }
}
