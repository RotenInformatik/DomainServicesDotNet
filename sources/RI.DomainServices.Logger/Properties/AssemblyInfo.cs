using System;
using System.Reflection;
using System.Runtime.InteropServices;




[assembly: AssemblyTitle("RI.DomainServices.Logger"),]
[assembly: AssemblyDescription("RI.DomainServices.Logger"),]
[assembly: Guid("1E0878C4-B2DC-4888-B218-3F81C1FCF52D"),]

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
