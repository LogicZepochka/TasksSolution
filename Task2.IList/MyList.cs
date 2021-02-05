using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.IList
{
    public class MyList<T> : IList<T>
    {
        T[] list = new T[0];
        int _count = 0;


        public T this[int index] { get => list[index]; set => list[index] = value; }

        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if(_count == list.Length)
            {
                CalculateNewSize();
            }
            list[_count++] = item;
        }

        public void Clear()
        {
            _count = 0;
            list = new T[0];
        }

        public bool Contains(T item)
        {
            foreach(T check in list)
            {
                if(check.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(int i =0;i<_count && arrayIndex+i<array.Length;i++)
            {
                array[i + arrayIndex] = list[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i=0;i<_count;i++)
            {
                yield return list[i];
            }
        }

        public int IndexOf(T item)
        {
            for(int i=0;i<_count;i++)
            {
                if(list[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index >= _count || index < 0) throw new IndexOutOfRangeException();
            if(list.Length <= _count+1)
                CalculateNewSize();

            _count++;
            for (int i=_count;i>index;i--)
            {
                list[i] = list[i-1];
            }
            list[index] = item;
        }

        public bool Remove(T item)
        {
            
            for(int i=0;i<_count;i++)
            {
                if(list[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= _count || index < 0) throw new IndexOutOfRangeException();
            for(int i= index; i<_count-1;i++)
            {
                list[i] = list[i + 1];
            }
            _count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return list[i];
            }
        }


        private int CalculateNewSize()
        {
            int newSize = list.Length == 0 ? 8 : (list.Length * 3) / 2 + 1;
            T[] newArray = new T[newSize];
            list.CopyTo(newArray,0);
            list = newArray;
            return newSize;
        }
    }
}
