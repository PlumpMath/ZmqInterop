﻿using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle( "ZmqInterop" )]
[assembly: AssemblyProduct( "ZmqInterop" )]
[assembly: AssemblyCopyright( "Copyright © 2013 Alex Forster" )]
[assembly: ComVisible( false )]
[assembly: AssemblyVersion( "1.0.1.13102" )]

#if DEBUG

[assembly: AssemblyConfiguration( "Debug" )]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
