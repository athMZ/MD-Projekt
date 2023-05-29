namespace GraphMatrix.Entites;

public class UniqueList<T> : List<T>
{
    public new void Add(T item)
    {
        if (!Contains(item))
        {
            base.Add(item);
        }
    }

    public new void AddRange(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Add(item);
        }
    }
}
