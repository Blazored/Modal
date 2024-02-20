namespace InteractiveServer.Components.Shared;

public class TestClass : IEquatable<TestClass?>
{
    public int MyProperty { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TestClass);
    }

    public bool Equals(TestClass? other)
    {
        return other is not null &&
               MyProperty == other.MyProperty;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MyProperty);
    }
}