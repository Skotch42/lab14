using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp11111
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public class Mechanic
        {
            public int car = 0;
            public int n;
            double mu = 1.5;

            public Mechanic(int i)
            {
                n = i;
            }

            public double work_time(Random random)
            {
                double a = random.NextDouble();

                return -Math.Log(a) / mu;
            }
        }

        public class Autoservise
        {
            public List<Mechanic> mechanics = new List<Mechanic>();
            Random random = new Random();
            int N, car_number;
            public int queue;
            double work_intensity;
            double car_intensity;

            public Autoservise(int n, double intensity2, double intensity1)
            {
                N = n;
                work_intensity = intensity2;
                car_intensity = intensity1;

                for (int i = 0; i < n; i++)
                {
                    mechanics.Add(new Mechanic(i));
                }
            }

            public double Time(double b = -1)
            {
                if (b == -1)
                {
                    b = car_intensity;
                }

                double a = random.NextDouble();

                return -Math.Log(a) / b;
            }

            public void mTime(double t1)
            {
                double work_time;
                int working = 0;

                for (int i = 0; i < N; i++)
                {
                    if (mechanics[i].car != 0)
                    {
                        working++;
                    }
                }

                if (working > 0)
                {
                    work_time = Time(work_intensity * working);
                }
                else
                {
                    work_time = Int64.MaxValue;
                }

                if (t1 < work_time)
                {
                    if (working < N)
                    {
                        mechanics[Vacant()].car = car_number;
                        car_number++;
                    }
                    else
                    {
                        queue++;
                    }
                }
                else
                {
                    int i = 0;
                    double t = Int64.MaxValue / 2;

                    foreach (Mechanic vasya in mechanics)
                    {
                        if (vasya.car != 0)
                        {
                            double new_t = vasya.work_time(random);

                            if (new_t < t)
                            {
                                i = vasya.n; 
                                t = new_t;
                            }
                        }
                    }

                    int car = 0;

                    if (queue > 0)
                    {
                        car = car_number;
                        car_number++;
                        queue--;
                    }

                    mechanics[i].car = car;
                }
            }

            public int Vacant()
            {
                int i = 0;

                for (i = 0; i < N; i++)
                {
                    if (mechanics[i].car == 0)
                    {
                        break;
                    }
                }

                return i;
            }
        }

        Autoservise autoservise;
        double intensity1, intensity2;
        int n;

        private void button1_Click_1(object sender, EventArgs e)
        {

            n = (int)numericUpDown1.Value;
            intensity1 = (double)numericUpDown3.Value;
            intensity2 = (double)numericUpDown2.Value;

            autoservise = new Autoservise(n, intensity2, intensity1);

            button1.Text = "Обновить входные данные";

            timer1.Start();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            autoservise.mTime(autoservise.Time());

            label1.Text = "Машин в очереди на починку: " + autoservise.queue.ToString();

            foreach (Mechanic mechanic in autoservise.mechanics)
            {
                if (mechanic.car != 0)
                {
                    dataGridView1.Rows.Add("Механик №" + (mechanic.n + 1), "Обслуживает машину № " + mechanic.car.ToString());
                }
                else
                {
                    dataGridView1.Rows.Add("Механик №" + (mechanic.n + 1), "Ничего не делает");
                }
            }
        }
    }
}