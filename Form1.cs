using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncingBalls
{
    public partial class Form1 : Form
    {
        private List<Ball> balls;
        private Ball ball1;
        private Ball ball2;
        private Ball ball3;
        private Ball ball4;
        public Form1()
        {
            InitializeComponent();

            Random random = new Random();
            int size = 50;
            int velocityX = random.Next(5, 10);
            int velocityY = random.Next(5, 10);
            ball1 = new Ball(50, 50, size, velocityX, velocityY);
            velocityX = random.Next(5, 10);
            velocityY = random.Next(5, 10);
            ball2 = new Ball(Width - 50, 50, size, velocityX, velocityY);
            velocityX = random.Next(5, 10);
            velocityY = random.Next(5, 10);
            ball3 = new Ball(50, Height - 50, size, velocityX, velocityY);
            velocityX = random.Next(5, 10);
            velocityY = random.Next(5, 10);
            ball4 = new Ball(Width - 50, Height - 50, size, velocityX, velocityY);

            balls = new List<Ball>();
            balls.Add(new Ball(50, 50, 5, 5, 30, Color.White));
            balls.Add(new Ball(100, 100, 2, 2, 20, Color.White));
            balls.Add(new Ball(150, 150, 8, 8, 40, Color.White));
            balls.Add(new Ball(200, 200, 3, 3, 25, Color.White));

            Timer timer = new Timer();
            timer.Interval = 20; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Ball ball in balls)
            {
                // Update ball position
                ball.Move();

                // Check for collision with screen borders
                if (ball.X < 0 || ball.X + ball.Size > pictureBox1.Width)
                {
                    ball.VelocityX = -ball.VelocityX;
                }
                if (ball.Y < 0 || ball.Y + ball.Size > pictureBox1.Height)
                {
                    ball.VelocityY = -ball.VelocityY;
                }

                // Check for collision with other balls
                foreach (Ball otherBall in balls)
                {
                    if (ball != otherBall)
                    {
                        int distance = (int)Math.Sqrt(Math.Pow(ball.X - (otherBall.X - ball.X) * (otherBall.X - ball.X) + (otherBall.Y - ball.Y) * (otherBall.Y - ball.Y)));

                        if (distance < ball.Size / 2 + otherBall.Size / 2)
                        {
                            // Balls have collided, reverse velocities
                            int tempX = ball.VelocityX;
                            int tempY = ball.VelocityY;
                            ball.VelocityX = otherBall.VelocityX;
                            ball.VelocityY = otherBall.VelocityY;
                            otherBall.VelocityX = tempX;
                            otherBall.VelocityY = tempY;
                        }
                    }
                }
            }

            // Redraw balls on screen
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Ball ball in balls)
            {
                ball.Draw(g);
            }
        }

        private void CalculateNewVelocity(Ball ball, int collisionAngle)
        {
            double angleInRadians = collisionAngle * Math.PI / 180;
            double newVelocityX = ball.Velocity * Math.Cos(angleInRadians);
            double newVelocityY = ball.Velocity * Math.Sin(angleInRadians);

            if (collisionAngle > 0 && collisionAngle < 180)
            {
                // Collision with top or bottom wall
                newVelocityY *= -1;
            }
            else
            {
                // Collision with left or right wall
                newVelocityX *= -1;
            }

            // Update the ball's velocity
            ball.VelocityX = (int)Math.Round(newVelocityX);
            ball.VelocityY = (int)Math.Round(newVelocityY);
        }

        private void DrawBalls()
        {
            foreach (Ball ball in balls)
            {
                g.FillEllipse(Brushes.White, ball.X, ball.Y, ball.Size, ball.Size);
            }
        }

        private int GetCollisionAngle(Ball ball1, Ball ball2)
        {
            Graphics g = e.Graphics;

            int collisionAngle = (int)Math.Atan2(ball2.Y - ball1.Y, ball2.X - ball1.X) * 180 / Math.PI;
            if (collisionAngle < 0)
            {
                collisionAngle += 360;
            }
            return collisionAngle;
        }

        private void CreateBalls()
        {
            Ball ball1 = new Ball(10, 10, 5, 5, 20, 1);
            Ball ball2 = new Ball(50, 50, -4, 3, 30, 2);
            Ball ball3 = new Ball(100, 100, 2, -5, 25, 1.5);
            Ball ball4 = new Ball(150, 150, -3, -4, 15, 0.5);

            balls = new List<Ball> { ball1, ball2, ball3, ball4 };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
