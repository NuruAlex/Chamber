using Chamber.Core;

namespace Chamber.Collections;

public abstract class UniqeCollection<T> where T : BaseEntity
{
    private readonly List<T> _list;
    private readonly string? _path;
    public List<T> Items
    {
        get
        {
            return _list;
        }
    }

    public int Count
    {
        get
        {
            return _list.Count;
        }
    }

    public UniqeCollection()
    {
        _list = DataBase.Serializer.LoadFromFile<T>();
        _path = null;
    }

    public UniqeCollection(string path)
    {
        _list = DataBase.Serializer.LoadFromFile<T>(path);
        _path = path;
    }

    public void Add(params T[] items)
    {
        foreach (T item in items)
        {
            Add(item);
        }
    }

    public void Add(T item)
    {
        if (item == null || Contains(item))//не допускаем null и такой же элемент в коллекции
        {
            return;
        }

        _list.Add(item);
        Refresh();
    }


    public void Refresh()
    {
        if (_path == null)
            DataBase.Serializer.SaveToFile(_list);
        else
            DataBase.Serializer.SaveToFile(_list, _path);
    }

    public void Clear()
    {
    }


    public T? Find(Predicate<T> match)
    {
        return _list.Find(match);
    }


    public bool TryFind(Predicate<T> match, out T? result)
    {
        result = Find(match);

        return result != null;
    }

    public List<T> FindAll(Predicate<T> match)
    {
        return _list.FindAll(match);
    }


    public bool Contains(Predicate<T> match)
    {
        return Find(match) != null;
    }


    public void Delete(Predicate<T> match)
    {
        _list.RemoveAll(match);
        Refresh();
    }


    public abstract bool Contains(T item);
    public abstract void Delete(T item);
}
