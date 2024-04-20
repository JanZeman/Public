namespace JanZeman.System.Collections;

public static class ListExtensions
{
    public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static List<T> CloneStruct<T>(this List<T> listToClone) where T : struct
    {
        return listToClone.Select(item => item).ToList();
    }

    public static void MoveIndex<T>(this List<T> list, int oldIndex, int newIndex)
    {
        var removedItem = list[oldIndex];
        if (removedItem == null) return;
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, removedItem);
    }

    public static void MoveItem<T>(this List<T> list, T item, int newIndex)
    {
        if (item == null) return;
        var itemIndex = list.IndexOf(item);
        list.MoveIndex(itemIndex, newIndex);
    }

    public static List<T> EveryNthItems<T>(this List<T> list, int n, int startIndex = 0)
    {
        var count = list.Count;

        if (startIndex > count)
            throw new ArgumentException("Start index must be lower than number of items in the list");

        var range = list;
        if (startIndex != 0)
            range = list.GetRange(startIndex, list.Count - startIndex);

        return range.Where((x, i) => i % n == 0).ToList();
    }

    public static List<T> LastNItems<T>(this List<T> list, int n = 1, int butLast = 0)
    {
        var count = list.Count;

        return n + butLast > count
            ? throw new ArgumentException($"N must be lower than number of items in the list but it was requested {n} from {count} (butLast was {butLast})")
            : list.GetRange(count - n - butLast, n);
    }

    public static List<T> OddItems<T>(this List<T> list)
    {
        return EveryNthItems(list, 2);
    }

    public static List<T> EvenItems<T>(this List<T> list)
    {
        return EveryNthItems(list, 2, 1);
    }
}