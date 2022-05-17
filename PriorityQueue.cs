namespace N_Puzzle
{
    class PriorityQueue
    {

        internal sealed class MinPriorityQueue<T> where T : FindNode
        {
            #region Fields

            private T[] mArray;
            private int mCount;

            #endregion Fields

            #region Methods
            // Initializing The Priorty Queue
            internal MinPriorityQueue(int capacity)
            {
                mArray = new T[capacity + 1];
                mCount = 0;
            }
            // Expanding The Priorty Queue
            private void Expand(int capacity)
            {
                T[] temp = new T[capacity + 1];
                int i = 0;
                while (++i <= mCount)
                {
                    temp[i] = mArray[i];
                    mArray[i] = default(T);
                }

                mArray = temp;
            }
            // Comparing Lesser Priorties
            private bool Less(int i, int j)
            {
                return mArray[i].Priorty < mArray[j].Priorty;
            }
            // Swapping Values
            private void Swap(int i, int j)
            {
                T temp = mArray[j];
                mArray[j] = mArray[i];
                mArray[i] = temp;
            }
            // Exchanging with a Child
            private void Sink(int index)
            {

                int k;
                while (index * 2 <= mCount)
                {
                    k = index * 2;

                    if (k + 1 <= mCount && Less(k + 1, k))
                    {
                        k = k + 1;
                    }

                    if (!Less(k, index))
                    {
                        break;
                    }

                    Swap(index, k);
                    index = k;
                }
            }
            // Exchanging With a Parent 
            private void Swim(int index)
            {
                int k;

                while (index / 2 > 0)
                {
                    k = index / 2;

                    if (!Less(index, k))
                    {
                        break;
                    }

                    Swap(index, k);
                    index = k;
                }
            }
            // Checking if The Priorty Queue is Empty
            internal bool IsEmpty()
            {
                return mCount == 0;
            }
            // Inserting New Values in Priorty Queue
            internal void Enqueue(T item)
            {
                if (mCount == mArray.Length - 1)
                {
                    Expand(mArray.Length * 3);
                }

                mArray[++mCount] = item;
                Swim(mCount);
            }
            // Popping Values from Priorty Queue
            internal T Dequeue()
            {
                if (!IsEmpty())
                {
                    T item = mArray[1];
                    mArray[1] = mArray[mCount];
                    mArray[mCount--] = default(T);

                    Sink(1);

                    return item;
                }

                return default(T);
            }
            // Finding an Item in The Priorty Queue
            internal T Find(T item, out int index)
            {
                index = -1;
                if (!IsEmpty())
                {
                    int i = 0;

                    while (++i <= mCount)
                    {
                        if (mArray[i].Equals(item))
                        {
                            index = i;
                            return mArray[i];
                        }
                    }
                }

                return default(T);
            }
            //Removing Values from The Priorty Queue
            internal void Remove(int index)
            {
                if (index > 0 && index <= mCount)
                {
                    mArray[index] = mArray[mCount];
                    mArray[mCount--] = default(T);
                    Sink(index);
                }
            }

            #endregion Methods
        }
    }
}
