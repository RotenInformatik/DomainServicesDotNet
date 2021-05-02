using System;
using System.Reflection;
using System.Runtime.InteropServices;




[assembly: AssemblyTitle("RI.DomainServices.ServiceCollection"),]
[assembly: AssemblyDescription("RI.DomainServices.ServiceCollection"),]
[assembly: Guid("2FA00068-8994-44FC-ACFE-4848F75E34B7"),]

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
