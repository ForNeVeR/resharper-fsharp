﻿using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.FSharp.Impl.Cache2.Declarations;
using JetBrains.ReSharper.Psi.FSharp.Tree;

namespace JetBrains.ReSharper.Psi.FSharp.Impl.Cache2
{
  /// <summary>
  /// Used for type abbreviations and abstract types
  /// </summary>
  public class HiddenTypePart : FSharpClassLikePart<IFSharpTypeParametersOwnerDeclaration>, Class.IClassPart
  {
    public HiddenTypePart(IFSharpTypeParametersOwnerDeclaration declaration, bool isHidden)
      : base(declaration, ModifiersUtil.GetDecoration(declaration.AccessModifiers), isHidden,
        declaration.TypeParameters.Count)
    {
    }

    public HiddenTypePart(IReader reader) : base(reader)
    {
    }

    public override TypeElement CreateTypeElement()
    {
      return new Class(this);
    }

    public MemberPresenceFlag GetMemberPresenceFlag()
    {
      return MemberPresenceFlag.NONE;
    }

    public override MemberDecoration Modifiers =>
      MemberDecoration.FromModifiers(Psi.Modifiers.INTERNAL); // should not be accessible from other languages

    protected override byte SerializationTag => (byte) FSharpPartKind.HiddenType;
  }
}