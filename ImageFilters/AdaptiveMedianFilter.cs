using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class AdaptiveMedianFilter
    {
        public static int flag = 0;
        public static Byte[,] newImage;
        public static Byte[,] ApplyFilter(Byte[,] ImageMatrix, int MaxWindowSize, int UsedAlgorithm, int size = 3, int x = -1, int y = -1)
        {
            int k;
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);
            Byte[] Window = new Byte[(size) * (size)];
            Byte min;
            Byte max;
            if (x != -1 || y != -1)
            {
                Window = SortHelper.GetWindow(x, y, ImageMatrix, size);
                if (UsedAlgorithm == 0) // Quick Sort
                {
                    Window = SortHelper.QuickSort(Window, 0, Window.Length - 1);
                }
                else // Counting Sort
                {
                    Window = SortHelper.CountingSort(Window);
                }
                min = Window[0];
                max = Window[Window.Length - 1];
                int Zxy = ImageMatrix[x, y];
                int med = Window[(Window.Length / 2)];
                int A1 = (med - min), A2 = max - med;
                //step 1
                if (A1 > 0 && A2 > 0)
                {
                    //step 2
                    int B1 = Zxy - min;
                    int B2 = max - Zxy;
                    size = 3;
                    
                    if (B1 > 0 && B2 > 0)
                    {
                        newImage[x, y] = ImageMatrix[x, y];
                    }
                    else
                    {
                        newImage[x, y] = (byte)med;
                    }
                }
                else
                {
                    size += 2;
                    if (size <= MaxWindowSize)
                    {
                        
                        ApplyFilter(ImageMatrix, MaxWindowSize, UsedAlgorithm, size, x, y);
                        x = -1;
                        y = -1;
                        size = 3;
                    }
                    else
                    {
                        size = 3;
                        newImage[x, y] = (byte)med;
                    }
                }
            }
            else
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            newImage = new byte[height, width];
                        }
                        Window = SortHelper.GetWindow(i, j, ImageMatrix, size);

                        if (UsedAlgorithm == 0) // Quick Sort
                        {
                            Window = SortHelper.QuickSort(Window, 0, Window.Length - 1);

                        }
                        else // Counting Sort
                        {
                            Window = SortHelper.CountingSort(Window);

                        }
                        min = Window[0];
                        max = Window[Window.Length - 1];

                        int Zxy = ImageMatrix[i, j];
                        int med = Window[(Window.Length / 2)];
                        int A1 = med - min, A2 = max - med;

                        if (A1 > 0 && A2 > 0) // True median
                        {
                            int B1 = Zxy - min;
                            int B2 = max - Zxy;
                            size = 3;
                            if (B1 > 0 && B2 > 0)
                            {
                                newImage[i, j] = ImageMatrix[i, j];
                            }
                            else
                            {
                                newImage[i, j] = (byte)med;
                            }
                        }
                        else
                        {
                            size += 2;
                            if (size <= MaxWindowSize)
                            {
                                
                                ApplyFilter(ImageMatrix, MaxWindowSize, UsedAlgorithm, size, i, j);
                                x = -1;
                                y = -1;
                                size = 3;
                                //j = j - 1;
                            }
                            else
                            {
                                size = 3;
                                newImage[i, j] = (byte)med;
                            }
                        }
                    }
                }
            }
          
            return newImage;
        }
    }
}
