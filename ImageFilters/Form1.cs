using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;
        int Wmax = 3;
        int T = 1;
        int SelectedFilterID = 0;
        int UsedAlgorithm = 0;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }

        private void btnZGraph_Click(object sender, EventArgs e)
        {
            double[] x_values = new double[Wmax / 2];
            double[] y_values_N = new double[Wmax / 2];
            double[] y_values_NLogN = new double[Wmax / 2];
            
            int index = 0;
            for (int i = 3; i <= Wmax; i += 2)
            {
                x_values[index] = i;
                index++;
            }
            if (cbFilter.SelectedIndex == 0)
            {
                index = 0;
                for (int i = 3; i <=Wmax; i += 2)
                {
                    if ((T * 2) < (i*i))
                    {
                        int T1 = System.Environment.TickCount;
                        AlphaTrimFilter.ApplyFilter(ImageMatrix, i, 0, T);
                        int T2 = System.Environment.TickCount;
                        y_values_N[index] = T2 - T1;
                        index++;
                    }
                }
                index = 0;
                for (int i = 3; i <=Wmax; i += 2)
                {
                    if ((T * 2) < (i * i))
                    {
                        int T1 = System.Environment.TickCount;
                        AlphaTrimFilter.ApplyFilter(ImageMatrix, i, 1, T);
                        int T2 = System.Environment.TickCount;
                        y_values_NLogN[index] = T2 - T1;
                        index++;
                    }
                }
                Console.WriteLine(y_values_NLogN[y_values_NLogN.Length - 1]);
                ZGraphForm ZGF = new ZGraphForm("Sample Graph", "Maximum Window Size", "Time");
                ZGF.add_curve("Count Sort", x_values, y_values_N, Color.Red);
                ZGF.add_curve("Kth Element", x_values, y_values_NLogN, Color.Blue);
                ZGF.Show();
            }
            else
            {
                index = 0;
                for (int i = 3; i <=Wmax; i += 2)
                {
                        int T1 = System.Environment.TickCount;
                        AdaptiveMedianFilter.ApplyFilter(ImageMatrix, i, 0);
                        int T2 = System.Environment.TickCount;
                        y_values_N[index] = T2 - T1;
                        index++;
                }
                index = 0;
                for (int i = 3; i <=Wmax; i += 2)
                {

                        int T1 = System.Environment.TickCount;
                        AdaptiveMedianFilter.ApplyFilter(ImageMatrix, i, 1);
                        int T2 = System.Environment.TickCount;
                        y_values_NLogN[index] = T2 - T1;
                        index++;
                }
                ZGraphForm ZGF = new ZGraphForm("Sample Graph", "Maximum Window Size", "Time");
                ZGF.add_curve("Quick Sort", x_values, y_values_N, Color.Red);
                ZGF.add_curve("Count Sort", x_values, y_values_NLogN, Color.Blue);
                ZGF.Show();
            }
           
        }

            private void btnGen_Click(object sender, EventArgs e)
        {
            if (SelectedFilterID == 0)
            {
                ImageOperations.DisplayImage(AlphaTrimFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm, T), pictureBox2);
            }
            else
            {
                ImageOperations.DisplayImage(AdaptiveMedianFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm), pictureBox2);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAlgorithm.Visible = true;
            lbl_algorithm.Visible = true;
            if (cbFilter.SelectedIndex == 0)
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = true;
                trimmingValue.Visible = true;
                SelectedFilterID = 0;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Counting Sort");
                cbAlgorithm.Items.Add("Kth Smallest/Largest");
            }
            else
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = false;
                trimmingValue.Visible = false;
                SelectedFilterID = 1;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Quick Sort");
                cbAlgorithm.Items.Add("Counting Sort");
            }
        }

        private void cbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsedAlgorithm = cbAlgorithm.SelectedIndex;
        }

        private void maxWindowSize_ValueChanged(object sender, EventArgs e)
        {
            Wmax = (int)maxWindowSize.Value;
        }

        private void trimmingValue_ValueChanged(object sender, EventArgs e)
        {
            T = (int)trimmingValue.Value;
        }
    }
}