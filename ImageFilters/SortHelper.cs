using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class SortHelper
    {

        public static byte Kth_element(Byte[] Array, int TrimValue)
        {
            int sum = 0;
            List<Byte> list = new List<Byte>();

            byte min;
            byte max;
            for (int i = 0; i < Array.Length; i++)
            {
                list.Add(Array[i]);
            }                            
            while (TrimValue > 0) 
            {
                min = 255;
                max = 0;
                
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] < min)
                    {
                        min = list[j];
                            
                    }
                    if (list[j] > max)
                    {
                        max = list[j];
                            
                    }
                }
                list.Remove(max);
                list.Remove(min);
                TrimValue--;
            }

            for(int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }
            int mean = sum / list.Count;
            return (byte)mean; 
        }

        public static Byte[] CountingSort(Byte[] Array)
        {
            Byte[] sortedarr = new Byte[Array.Length];
            int[] count = new int[256];

            for (int u = 0; u < 256; u++)
            {
                count[u] = 0;
            }
            for (int a = 0; a < Array.Length; a++)
            {
                count[Array[a]]++;
            }
            for (int b = 1; b < count.Length; b++)
            {
                count[b] = count[b] + count[b - 1];
            }
            for (int c = (Array.Length) - 1; c >= 0; c--) 
            {
                sortedarr[count[Array[c]] - 1] = Array[c];
                --count[Array[c]];
            }
           
            return sortedarr;

        }

        public static Byte[] QuickSort(Byte[] Array, int begin, int end)
        {
            int b = begin , e = end, pivot = Array[begin];
            while (b <= e)
            {
                while (Array[b] < pivot)
                {
                    b++;
                }

                while (Array[e] > pivot)
                {
                    e--;
                }
                if (b <= e)
                {
                    Swap(ref Array[b], ref Array[e]);
                    b++;
                    e--;
                }
            }
            if (b < end)
            {
                QuickSort(Array, b, end);
            }
            if (begin < e)
            {
                QuickSort(Array, begin, e);
            }
               
            return Array;
        }
         public static  void Swap( ref byte x, ref byte y)
        {
            byte temp = x;
            x = y;
            y = temp;
        }
        public static Byte[] GetWindow(int i, int j, Byte[,] ImageMatrix, int size)
        {
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);
            byte[] Window = new byte[size*size];
            int k = 0;
            for (int neighbour_x = i - (size / 2); neighbour_x <= (i + size / 2); ++neighbour_x)
            {

                for (int neighbour_y = j - (size / 2); neighbour_y <= (j + size / 2); ++neighbour_y)
                {

                    if (neighbour_x < 0 || neighbour_y < 0 || neighbour_y >= width || neighbour_x >= height)
                    {


                        Window[k] = 0;
                    }
                    else
                    {
                        Window[k] = ImageMatrix[neighbour_x, neighbour_y];
                    }
                    k++;

                }
            }


            return Window;
        }
    }
}
