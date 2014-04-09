// Guids.cs
// MUST match guids.h
using System;

namespace Cement.VisualStudio.Project
{
    static class GuidList
    {
        public const string guidCement_VisualStudio_ProjectPkgString = "72427bba-d3d2-43be-a579-c538a1848572";
        public const string guidCement_VisualStudio_ProjectCmdSetString = "7efff1df-6c8b-42dd-98c4-fd650019660e";
        public const string guidCement_VisualStudio_ProjectFactory = "5B71B09D-EFE5-4246-ADD0-FE020E7C255D";
        public static readonly Guid guidCement_VisualStudio_ProjectCmdSet = new Guid(guidCement_VisualStudio_ProjectCmdSetString);
    };
}