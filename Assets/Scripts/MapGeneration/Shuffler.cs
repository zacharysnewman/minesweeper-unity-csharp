using System.Collections.Generic;

public static class Shuffler
{
    public static List<T> Shuffle<T>(List<T> list)
    {
        List<T> temp = new List<T>(list);
        int n = temp.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = temp[k];
            temp[k] = temp[n];
            temp[n] = value;
        }
        return temp;
    }
}
