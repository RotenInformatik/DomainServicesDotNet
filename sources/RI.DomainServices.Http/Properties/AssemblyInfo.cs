using System;
using System.Reflection;
using System.Runtime.InteropServices;




[assembly: AssemblyTitle("RI.DomainServices.Http"),]
[assembly: AssemblyDescription("RI.DomainServices.Http"),]
[assembly: Guid("8C0CBA9E-0530-43CA-8DF8-33AFF4D7A726"),]

[assembly: AssemblyProduct(".NET Domain Services"),]
[assembly: AssemblyCompany("Roten Informatik"),]
[assembly: AssemblyCopyright("Copyright (c) 2011-2020 Roten Informatik"),]
[assembly: AssemblyTrademark(""),]
[assembly: AssemblyCulture(""),]
[assembly: CLSCompliant(false),]
[assembly: AssemblyVersion("0.0.0.0"),]
[assembly: AssemblyFileVersion("0.0.0.0"),]
[assembly: AssemblyInformationalVersion("0.0.0.0"),]

#if DEBUG
[assembly: AssemblyConfiguration("DEBUG"),]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#if !RELEASE
#warning "RELEASE not specified"
#endif
#endif
