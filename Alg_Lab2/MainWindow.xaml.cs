using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alg_Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Axis first;
        private static Axis second;
        private static Axis third;
        private static TimeSpan time;
        public MainWindow()
        {
            InitializeComponent();

            int start = 1;
            int temp = 2;
            int end = 3;
            int numbersOfDisks = 8;

            first = new Axis(100);
            second = new Axis(500);
            third = new Axis(900);
            first.disks.Add(R1);
            first.disks.Add(R2);
            first.disks.Add(R3);
            first.disks.Add(R4);
            first.disks.Add(R5);
            first.disks.Add(R6);
            first.disks.Add(R7);
            first.disks.Add(R8);

            MoveDisks(first, second, third, numbersOfDisks);
        }

        public async void MoveDisks(Axis start, Axis temp, Axis end, int disks)
        {
            if (disks > 1)
                MoveDisks(start, end, temp, disks - 1);

            Console.WriteLine(ccc(first) + "\n" + ccc(second) + "\n" + ccc(third) + "\n");
            
            await start.MoveTo(end);

            if (disks > 1)
                MoveDisks(temp, start, end, disks - 1);
        }

        public String ccc(Axis a)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var fisk in a.disks)
            {
                sb.Append(fisk.Name + " ");
            }

            return sb.ToString();
        }

        public class Axis
        {
            public List<Rectangle> disks;

            public int _left;

            public Axis(int left)
            {
                disks  =new List<Rectangle>();
                _left = left;
            }

            public async Task MoveTo(Axis to)
            {
                double speed = 300;

                double leftInit = Canvas.GetLeft(disks[disks.Count - 1]);
                double topInit = Canvas.GetTop(disks[disks.Count - 1]);


                this.MoveUp(to);
                this.MoveHorizontal(to);
                this.MoveDown(to);


                to.disks.Add(disks[disks.Count - 1]);
                disks.RemoveAt(disks.Count - 1);

            }
            public void MoveUp(Axis to)
            {
                double topInit = Canvas.GetTop(disks[disks.Count - 1]);

                var top = new DoubleAnimation                                                       //поднимаем с исходной оси
                {
                    From = topInit,
                    To = 120,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };
                top.Completed += Top_Completed;
                disks[disks.Count - 1].BeginAnimation(Canvas.TopProperty, top);
            }

            private void Top_Completed(object sender, EventArgs e)
            {
                MoveHorizontal(to);
            }

            public void MoveHorizontal(Axis to)
            {
                double leftInit = Canvas.GetLeft(disks[disks.Count - 1]);
                var horizontal = new DoubleAnimation()
                {
                    From = leftInit,
                    To = to._left + (200 - disks[disks.Count - 1].Width / 2) - 100,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };

                disks[disks.Count - 1].BeginAnimation(Canvas.LeftProperty, horizontal);
            }

            public void MoveDown(Axis to)
            {
                double topInit = Canvas.GetTop(disks[disks.Count - 1]);

                var down = new DoubleAnimation()
                {
                    From = topInit,
                    To = 369 - (to.disks.Count * 31),
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };

                disks[disks.Count - 1].BeginAnimation(Canvas.TopProperty, down);
            }

            public void Geon(Axis to)
            {
                PathGeometry pathGeometry = new PathGeometry(new PathFigure[]);
            }
                
                
            }
        }
    }
}
