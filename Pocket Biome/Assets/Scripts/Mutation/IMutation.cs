public interface IMutation
{
    string Name { get; }
    string Description { get; }
    void Apply();
}
