using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class AlphaTrimFilter
    {
        public static Byte[,] ApplyFilter(Byte[,] ImageMatrix, int MaxWindowSize, int UsedAlgorithm, int TrimValue)
        {
            
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);
            Byte[,] newImage = new byte[height, width];
            Byte[] Window = new Byte[(MaxWindowSize) * (MaxWindowSize)];

          
            for (int i = 0; i < height; i++)
            {

                for (int j = 0; j < width; j++)
                {

                    Window = SortHelper.GetWindow(i, j, ImageMatrix, MaxWindowSize);
                 
                    if (UsedAlgorithm == 0) //Counting sort
                    {
                        Byte[] sortedarr = SortHelper.CountingSort(Window);
                        int sum = 0;
                        int mean;
                        for (int m = TrimValue; m < (sortedarr.Length - TrimValue); m++)
                        {
                            sum += (int)sortedarr[m];
                        }
                        mean = sum / (Window.Length - (TrimValue * 2));
                        newImage[i, j] = (byte)mean;
                    }
                    else
                    {
                        newImage[i, j] = SortHelper.Kth_element(Window,TrimValue);
                    }

                }
            }

            return newImage;
        }
    }
}

