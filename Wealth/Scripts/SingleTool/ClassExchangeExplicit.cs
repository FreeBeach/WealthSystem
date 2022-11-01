public class ExplicitClass
{ }

public class ClassExchangeExplicit
{
    // Start is called before the first frame update
    public static explicit operator ClassExchangeExplicit(ExplicitClass instance)
    {
        return new ClassExchangeExplicit();
    }
    /// <summary>
    /// 隐式转换
    /// </summary>
    void HowToUsedImplicit()
    {
        ExplicitClass class1 = new ExplicitClass();
        ClassExchangeExplicit class2 = (ClassExchangeExplicit)class1;
    }
}
