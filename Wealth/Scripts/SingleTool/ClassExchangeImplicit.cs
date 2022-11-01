using System.Collections;


public class ImplicitClass
{ }

/// <summary>
/// 隐式转换
/// </summary>
public class ClassExchangeImplicit
{
    public static implicit operator ClassExchangeImplicit(ImplicitClass instance)
    {
        return new ClassExchangeImplicit();
    }
    /// <summary>
    /// 隐式转换
    /// </summary>
    void HowToUsedImplicit()
    {
        ImplicitClass class1 = new ImplicitClass();
        ClassExchangeImplicit class2 = class1;
    }
}
