﻿using Microsoft.CodeAnalysis;

namespace TestUpdaterAnalyzers
{
    public static class RhinoRecognizer
    {
        private const string RhinoMocksExtensions = nameof(RhinoMocksExtensions);
        private const string IMethodOptions = nameof(IMethodOptions);
        private const string MockRepository = nameof(MockRepository);
        private const string OutRefArgDummy = nameof(OutRefArgDummy);
        private const string Arg = nameof(Arg);
        private const string IsArg = nameof(IsArg);
        private const string IRepeat = nameof(IRepeat);
        private const string RhinoMocks = "Rhino.Mocks";

        public static bool IsReturnMethod(IMethodSymbol memberSymbol) =>
               IsSymbol(memberSymbol, "Return", IMethodOptions);

        public static bool IsGenerateMockMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "GenerateMock", MockRepository);

        public static bool IsExpectMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Expect", RhinoMocksExtensions);

        public static bool IsStubMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Stub", RhinoMocksExtensions);

        public static bool IsAssertWasCalledMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "AssertWasCalled", RhinoMocksExtensions);

        public static bool IsAssertWasNotCalledMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "AssertWasNotCalled", RhinoMocksExtensions);

        public static bool IsIsArgProperty(IPropertySymbol propertySymbol) =>
            IsSymbol(propertySymbol, "Is", Arg);

        public static bool IsOutArgMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Out", Arg);

        public static bool IsAnythingProperty(IPropertySymbol propertySymbol) =>
            IsSymbol(propertySymbol, "Anything", IsArg);

        public static bool IsNullArgProperty(IPropertySymbol propertySymbol) =>
            IsSymbol(propertySymbol, "Null", IsArg);

        public static bool IsNotNullArgProperty(IPropertySymbol propertySymbol) =>
            IsSymbol(propertySymbol, "NotNull", IsArg);

        public static bool IsEqualArgMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Equal", IsArg);

        public static bool IsSameArgMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Same", IsArg);

        public static bool IsMatchesArgMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Matches", Arg);

        public static bool IsGenerateStubMethod(IMethodSymbol memberSymbol) =>
          IsSymbol(memberSymbol, "GenerateStub", MockRepository);

        public static bool IsThrowMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Throw", IMethodOptions);

        public static bool IsIgnoreArgumentsMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "IgnoreArguments", IMethodOptions);

        public static bool IsWhenCalledMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "WhenCalled", IMethodOptions);

        public static bool IsRepeatProperty(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "Repeat", IMethodOptions);

        public static bool IsOutRefProperty(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "OutRef", IMethodOptions);

        public static bool IsPropertyBehavior(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "PropertyBehavior", IMethodOptions);

        public static bool IsVerifyAllExpectationsMethod(IMethodSymbol memberSymbol) =>
            IsSymbol(memberSymbol, "VerifyAllExpectations", RhinoMocksExtensions);

        public static bool IsDummyField(IFieldSymbol fieldSymbol) =>
            IsSymbol(fieldSymbol, "Dummy", OutRefArgDummy);

        public static bool IsAnyRepeatOptionsMethod(IMethodSymbol memberSymbol) =>
            IsAnySymbol(memberSymbol, IRepeat);

        public static bool IsSymbol(ISymbol symbolsType, string name, string type, string assembly = RhinoMocks)
        {
            return symbolsType.Name == name
                && symbolsType.OriginalDefinition.ContainingAssembly.MetadataName == assembly
                && symbolsType.OriginalDefinition.ContainingType.Name == type;
        }

        public static bool IsAnySymbol(ISymbol symbolsType, string type, string assembly = RhinoMocks)
        {
            return symbolsType.OriginalDefinition.ContainingAssembly.MetadataName == assembly
                && symbolsType.OriginalDefinition.ContainingType.Name == type;
        }
    }
}
