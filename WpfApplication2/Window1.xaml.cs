using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Snake_TZWKTT;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace SnakeGame
{
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        private List<Point> bonusPoints = new List<Point>();
        private List<Point> snakePoints = new List<Point>();

        private readonly Brush snakeColor = Brushes.Green;

        public enum SIZE
        {
            THIN = 4,
            NORMAL = 6,
            THICK = 8
        }
        public enum PACE
        {
            VERYSLOW,
            SLOW,
            NORMAL,
            FAST
        }
        public enum MOVINGDIRECTION
        {
            UP = 8,
            DOWN = 2,
            LEFT = 4,
            RIGHT = 6
        }

        private readonly Point startingPoint = new Point(200, 200);
        private Point currentPosition = new Point();

        private int direction = 0;

        private int previousDirection = 0;
        private int headSize;
        private int length = 10;
        private int score = 0;


        private Random rnd = new Random();
        private DispatcherTimer timer;

        private int thickness;
        private int pace;

        public DispatcherTimer Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; OnPropertyChanged("Score"); }
        }
        
        public Window1(int thickness, int pace)
        {
            InitializeComponent();
            timer = new DispatcherTimer();

            switch (thickness)
            {
                case 1:
                    headSize = (int)SIZE.THIN;
                    break;
                case 2:
                    headSize = (int)SIZE.NORMAL;
                    break;
                case 3:
                    headSize = (int)SIZE.THICK;
                    break;
                default:
                    break;
            }
            switch (pace)
            {
                case 0:
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                    break;
                case 1:
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
                    break;
                case 2:
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                    break;
                case 3:
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                    break;
                default:
                    break;
            }


            this.DataContext = this;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"background.jpg", UriKind.Relative));
            paintCanvas.Background = ib;

            this.thickness = thickness;
            this.pace = pace;

            
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintSnake(startingPoint);
            currentPosition = startingPoint;

            
            for (int n = 0; n < 1; n++)
            {
                paintBonus(n);
            }
        }

        
       
        private void paintSnake(Point currentposition)
        {

                Ellipse newEllipse = new Ellipse();
                newEllipse.Fill = snakeColor;
                newEllipse.Width = headSize;
                newEllipse.Height = headSize;

                Canvas.SetTop(newEllipse, currentposition.Y);
                Canvas.SetLeft(newEllipse, currentposition.X);

                int count = paintCanvas.Children.Count;
                paintCanvas.Children.Add(newEllipse);
                snakePoints.Add(currentposition);


                if (count > length)
                {
                    paintCanvas.Children.RemoveAt(count - length);
                    snakePoints.RemoveAt(count - length);
                }
        }


        private void paintBonus(int index)
        {
            Point bonusPoint = new Point(rnd.Next(5, 620), rnd.Next(5, 330));
            byte[] buffer = File.ReadAllBytes("apple.png");
            MemoryStream memoryStream = new MemoryStream(buffer);
            BitmapImage bitmap = new BitmapImage();

            ImageSource imgs = bitmap;
            
            bitmap.BeginInit();
            bitmap.DecodePixelWidth = headSize*2;
            bitmap.DecodePixelHeight = headSize*2;
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            bitmap.Freeze();


            Image apple = new Image();
            apple.Source = imgs;

            Canvas.SetTop(apple, bonusPoint.Y);
            Canvas.SetLeft(apple, bonusPoint.X);
            paintCanvas.Children.Insert(index, apple);
            bonusPoints.Insert(index, bonusPoint);
            
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            
                switch (direction)
                {
                    case (int)MOVINGDIRECTION.DOWN:
                        currentPosition.Y += 5;
                        paintSnake(currentPosition);
                        break;
                    case (int)MOVINGDIRECTION.UP:
                        currentPosition.Y -= 5;
                        paintSnake(currentPosition);
                        break;
                    case (int)MOVINGDIRECTION.LEFT:
                        currentPosition.X -= 5;
                        paintSnake(currentPosition);
                        break;
                    case (int)MOVINGDIRECTION.RIGHT:
                        currentPosition.X += 5;
                        paintSnake(currentPosition);
                        break;
                }


                if ((currentPosition.X < 0) || (currentPosition.X > 620) ||
                    (currentPosition.Y < 0) || (currentPosition.Y > 340))
                {
                    GameOver();
                }
                   

               
                int n = 0;
                foreach (Point point in bonusPoints)
                {

                    if ((Math.Abs(point.X - currentPosition.X) < headSize*2) && 
                        (Math.Abs(point.Y - currentPosition.Y) < headSize*2))
                    {
                        length += 10;
                        Score += 10;

                        
                        bonusPoints.RemoveAt(n);
                        paintCanvas.Children.RemoveAt(n);
                        paintBonus(n);
                        break;
                    }
                    n++;
                }

                for (int q = 0; q < (snakePoints.Count - headSize*2); q++)
                {
                    Point point = new Point(snakePoints[q].X, snakePoints[q].Y);
                    if ((Math.Abs(point.X - currentPosition.X) < (headSize/1.5)) &&
                         (Math.Abs(point.Y - currentPosition.Y) < (headSize/1.5)) )
                    {
                        GameOver();
                        break;
                    }

                }

        }
        
        
        
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Down:
                    if (previousDirection != (int)MOVINGDIRECTION.UP)
                        direction = (int)MOVINGDIRECTION.DOWN;
                    break;
                case Key.Up:
                    if (previousDirection != (int)MOVINGDIRECTION.DOWN)
                        direction = (int)MOVINGDIRECTION.UP;
                    break;
                case Key.Left:
                    if (previousDirection != (int)MOVINGDIRECTION.RIGHT)
                        direction = (int)MOVINGDIRECTION.LEFT;
                    break;
                case Key.Right:
                    if (previousDirection != (int)MOVINGDIRECTION.LEFT)
                        direction = (int)MOVINGDIRECTION.RIGHT;
                    break;

            }
            previousDirection = direction;
            
        }

        List<int> scorelist = new List<int>();
        
        StreamWriter sw;
        FileStream fs;
        Name name = new Name();
        ScoreLists player;
        private void GameOver()
        {
            timer.Stop();
            
            name.ShowDialog();
            
            if (name.DialogResult.HasValue && name.DialogResult.Value)
            {
                player = new ScoreLists(name.TextBox_Name.Text, Score);
                fs = new FileStream("scorelist.txt", FileMode.Append);
                sw = new StreamWriter(fs);

                sw.WriteLine(player.ToString());
                sw.Close();
                fs.Close();
                MessageBox.Show("You Lose! Your score is " + Score.ToString(), "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
           
            this.Close();
        }


       
    }
}

