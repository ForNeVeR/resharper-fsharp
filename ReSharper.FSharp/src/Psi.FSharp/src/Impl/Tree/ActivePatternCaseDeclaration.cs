﻿using System.Diagnostics;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.FSharp.Impl.DeclaredElement;
using JetBrains.ReSharper.Psi.Tree;
using Microsoft.FSharp.Compiler.SourceCodeServices;

namespace JetBrains.ReSharper.Psi.FSharp.Impl.Tree
{
  internal partial class ActivePatternCaseDeclaration : ICachedTypeMemberDeclaration
  {
    private volatile IDeclaredElement myCachedDeclaredElement;

    protected override void PreInit()
    {
      base.PreInit();
      myCachedDeclaredElement = null;
    }

    IDeclaredElement ICachedTypeMemberDeclaration.CachedDeclaredElement
    {
      get { return myCachedDeclaredElement; }
      set { myCachedDeclaredElement = value; }
    }

    public override string DeclaredName => Identifier.Name;
    public override string SourceName => FSharpImplUtil.GetSourceName(Identifier);

    public override TreeTextRange GetNameRange()
    {
      return Identifier.GetNameRange();
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public override IDeclaredElement DeclaredElement
    {
      get
      {
        this.AssertIsValid("Asking declared element from invalid declaration");
        var cache = GetPsiServices().Caches.SourceDeclaredElementsCache;
        return cache.GetOrCreateDeclaredElement(this, DeclaredElementFactory);
      }
    }

    private IDeclaredElement DeclaredElementFactory(ActivePatternCaseDeclaration arg)
    {
      var patternCase = GetFSharpSymbol() as FSharpActivePatternCase;
      return patternCase != null ? new ActivePatternCase(this, patternCase) : null;
    }

    public IDeclaredElement CachedDeclaredElement { get; set; }
  }
}