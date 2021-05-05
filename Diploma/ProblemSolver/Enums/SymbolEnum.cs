using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProblemSolver.Enums
{
    public enum SymbolEnum
    {
        [Description("=")]
        Equal,

        [Description(">=")]
        MoreOrEqual,

        [Description("<=")]
        LessOrEqual
    }
}
