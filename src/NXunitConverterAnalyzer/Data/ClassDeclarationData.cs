﻿using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace NXunitConverterAnalyzer.Data
{
    public class ClassDeclarationData
    {
        public List<SyntaxToken> TestCaseSources { get; } = new List<SyntaxToken>();
    }
}