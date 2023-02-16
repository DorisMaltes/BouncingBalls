using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace BouncingBalls
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public int Size { get; set; }
        public double Mass { get; set; }

        private double m; // mass of the ball
        private double r; // radius of the ball
        private double a; // surface area of the ball
        private double c; // air resistance coefficient
        private double k; // drag coefficient

        public Ball(double x, double y, double velocityX, double velocityY, int size, double mass)
        {
            X = x;
            Y = y;
            VelocityX = velocityX;
            VelocityY = velocityY;
            Size = size;
            Mass = mass;
        }


        public void UpdatePosition()
        {
            X += VelocityX;
            Y += VelocityY;

            // Apply air resistance
            double speed = Math.Sqrt(VelocityX * VelocityX + VelocityY * VelocityY);
            double vUnitX = VelocityX / speed;
            double vUnitY = VelocityY / speed;
            double dragForceX = -k * speed * vUnitX;
            double dragForceY = -k * speed * vUnitY;
            double dragAccelX = dragForceX / m;
            double dragAccelY = dragForceY / m;
            VelocityX += (int)dragAccelX;
            VelocityY += (int)dragAccelY;
        }

        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color.White);
            g.FillEllipse(brush, X - Size / 2, Y - Size / 2, Size, Size);
            brush.Dispose();
        }

        public void Move()
        {
            X += VelocityX;
            Y += VelocityY;
        }


    }
}


