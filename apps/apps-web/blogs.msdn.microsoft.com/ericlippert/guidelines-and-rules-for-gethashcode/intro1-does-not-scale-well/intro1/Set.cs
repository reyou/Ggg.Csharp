using System.Collections.Generic;

namespace intro1
{
    class Set<T>
    {
        private List<T> list = new List<T>();

        public void Insert(T item)
        {
            if (!Contains(item))
                list.Add(item);
        }

        public bool Contains(T item)
        {
            foreach (T member in list)
                if (member.Equals(item))
                    return true;
            return false;
        }
    }
}