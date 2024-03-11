using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Karpov_Maksim_PRI_120_PKG_KP
{
    class Spaceship
    {
        private float deltaColor;

        //Фон
        public void drawBackground(uint backgroundTexture)
        {
            Gl.glPushMatrix();
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, backgroundTexture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-200, 5, 400);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(-200, 5, -400);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(200, 5, -400);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(200, 5, 400);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glPopMatrix();
        }

        //Космический корабль
        public void drawSpaceship(double delta, Scene scene, bool isSpaceshipIntact, double deltaX)
        {
            Gl.glPushMatrix();

            switch (scene)
            {
                case Scene.First:
                    Gl.glTranslated(0 - deltaX, -40, 0+delta);
                    Gl.glRotated(delta, 0, 1, 0);
                    Gl.glRotated(delta, 1, 0, 0);
                    Gl.glRotated(delta, 0, 0, 1);
                    break;
                case Scene.SecondGameNotStarted:
                    Gl.glTranslated(0 - deltaX, -40, 0 + delta);
                    break;
                case Scene.SecondGameStarted:
                    Gl.glTranslated(0, -40, 0 + delta);
                    break;
            }

            Gl.glPushMatrix();
            setColor(0.95f, 0.7f, 0.12f);
            Gl.glScalef(2, 1, 1);
            Glut.glutSolidSphere(30, 12, 12);
            Gl.glColor3f(0.6f,0.35f,0.02f);
            Gl.glLineWidth(3f);
            Glut.glutWireSphere(30 , 15, 15);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-50, 0, 0);
            setColor(0.95f, 0.7f, 0.12f);
            Gl.glRotatef(90, 0, 1, 0);
            Glut.glutSolidCylinder(30, 50, 12, 12);
            Gl.glColor3f(0.6f, 0.35f, 0.02f);
            Gl.glLineWidth(3f);
            Glut.glutWireCylinder(30, 50, 15, 15);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-60, 0, -5);
            Gl.glScalef(0.5f, 0.3f, 0.3f);
            for (int i =0; i<3; i++)
            {
                Gl.glRotatef(72*i, 1, 0, 0);
                Gl.glPushMatrix();
                Gl.glTranslated(0, 0, 60);
                setColor(0.4f, 0.09f, 0.6f);
                Glut.glutSolidSphere(30, 12, 12);
                Gl.glColor3f(0.2f, 0f, 0.4f);
                Gl.glLineWidth(3f);
                Glut.glutWireSphere(30, 15, 15);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(-30, 0, 60);
                setColor(0.4f, 0.09f, 0.6f);
                Gl.glRotatef(90, 0, 1, 0);
                Glut.glutSolidCylinder(30, 30, 12, 12);
                Gl.glColor3f(0.2f, 0f, 0.4f);
                Gl.glLineWidth(3f);
                Glut.glutWireCylinder(30, 30, 15, 15);
                Gl.glPopMatrix();

                if (scene == Scene.SecondGameStarted)
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(-30, 0, 60);
                    Gl.glScalef(2, 1, 1);
                    setColor(0.9f, 0.09f, 0.03f);
                    Gl.glRotatef(90, 0, 1, 0);
                    Glut.glutSolidSphere(30, 12, 12);
                    Gl.glColor3f(1,1,1);
                    Gl.glLineWidth(5f);
                    Glut.glutWireSphere(30, 15, 15);
                    Gl.glPopMatrix();
                }
            }            
            
            Gl.glPopMatrix();

            for (int i = 0; i<2; i++)
            {
                Gl.glPushMatrix();
                if (i == 0) Gl.glTranslated(-50, -30, 0); else Gl.glTranslated(-50, 30, 0);
                setColor(0.95f, 0.05f, 0.02f);
                Gl.glRotatef(90, 0, 1, 0);
                Gl.glScalef(0.15f, 0.6f, 1f);
                Glut.glutSolidCone(30, 50, 12, 12);
                Gl.glColor3f(1f, 0.1f, 0.2f);
                Gl.glLineWidth(3f);
                Glut.glutWireCone(30, 50, 15, 15);
                Gl.glPopMatrix();
            }

            for (int i = 0; i < 2; i++)
            {
                Gl.glPushMatrix();
                if (i == 0)
                {
                    Gl.glTranslated(-50, 0, 30);
                    setColor(0.2f, 0.15f, 0.9f);
                }
                else
                {
                    Gl.glTranslated(-50, 0, -30);
                    setColor(0.95f, 0.05f, 0.02f);
                }
                Gl.glRotatef(90, 0, 1, 0);
                Gl.glScalef(0.6f, 0.15f, 1f);
                Glut.glutSolidCone(30, 50, 12, 12);
                Gl.glLineWidth(3f);
                Glut.glutWireCone(30, 50, 15, 15);
                Gl.glPopMatrix();
            }

            //Окно
            for (int i = 0; i < 2; i++)
            {
                Gl.glPushMatrix();

                if (i == 0)
                {
                    Gl.glTranslated(10, -30, 10);
                    Gl.glRotated(-18, 1, 0, 0);
                }
                else
                {
                    Gl.glTranslated(10, 25, 10);
                    Gl.glRotated(-165, 1, 0, 0);
                }

                Gl.glPushMatrix();
                setColor(0.95f, 0.1f, 0.12f);
                Gl.glScalef(1, 0.1f, 1);
                Glut.glutSolidSphere(13, 12, 12);
                Gl.glColor3f(0.6f, 0.35f, 0.02f);
                Gl.glLineWidth(3f);
                Glut.glutWireSphere(13, 15, 15);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(0, -3, 0);
                if (isSpaceshipIntact)
                    setColor(0.8f, 1f, 0.4f);
                else setColor(0.1f, 0.1f, 0.1f);
                Gl.glScalef(1, 0.1f, 1);
                Glut.glutSolidSphere(10, 12, 12);
                Gl.glColor3f(0.6f, 0.35f, 0.02f);
                Gl.glLineWidth(3f);
                Glut.glutWireSphere(10, 15, 15);
                Gl.glPopMatrix();

                Gl.glPopMatrix();
            }
            Gl.glPopMatrix();
        }

        public void drawMeteorit(double deltaX, double translateZ)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(300 - deltaX, -40, translateZ);
            setColor(0.2f,0.2f,0.2f);
            Gl.glScalef(1, 1f, 1);
            Glut.glutSolidSphere(10, 12, 12);
            Gl.glColor3f(0,0,0);
            Gl.glLineWidth(3f);
            Glut.glutWireSphere(10, 15, 15);
            Gl.glPopMatrix();
        }

        //Луна
        public void drawMoon()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(150, -40, 0);

            Gl.glPushMatrix();
            setColor(0.8f, 0.8f, 0.8f);
            Glut.glutSolidSphere(50, 32, 32);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0,-40,32);
            Gl.glRotated(-1, 1, 0, 0);
            setColor(0.5f, 0.5f, 0.5f);
            Gl.glScalef(1, 1f, 0.1f);
            Glut.glutSolidSphere(1, 32, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-7, -41, 28);
            Gl.glRotated(50, 1, 0, 0);
            setColor(0.5f, 0.5f, 0.5f);
            Gl.glScalef(1, 1f, 0.1f);
            Glut.glutSolidSphere(1.3, 32, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-15, -45, 20);
            Gl.glRotated(50, 1, 0, 0);
            setColor(0.5f, 0.5f, 0.5f);
            Gl.glScalef(1, 1f, 0.1f);
            Glut.glutSolidSphere(1.7, 32, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(10, -40, 32);
            Gl.glRotated(40, 1, 0, 0);
            setColor(0.5f, 0.5f, 0.5f);
            Gl.glScalef(1, 1f, 0.1f);
            Glut.glutSolidSphere(1.2, 32, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(10, -45, 20);
            Gl.glRotated(60, 1, 0, 0);
            setColor(0.5f, 0.5f, 0.5f);
            Gl.glScalef(1, 1f, 0.1f);
            Glut.glutSolidSphere(1.5, 32, 12);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Рисуем Би-Би
        public void drawBibi(RotateBibi rotate, double deltaX, double deltaY)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(150 + deltaX, -85 + deltaY, 23);
            switch (rotate)
            {
                case RotateBibi.Back:
                    Gl.glRotated(180, 0, 0, 1);
                    break;
                case RotateBibi.Left:
                    Gl.glRotated(-90, 0, 0, 1);
                    break;
                case RotateBibi.Right:
                    Gl.glRotated(90, 0, 0, 1);
                    break;
            }

            //Тело
            Gl.glPushMatrix();
            setColor(0.7f, 0.7f, 0.05f);
            Glut.glutSolidSphere(1.6, 32, 32);
            Gl.glPopMatrix();

            for (int i = 0; i <2; i++)
            {
                Gl.glPushMatrix();
                if (i==0) Gl.glTranslated(0, 0, 0.85);
                else Gl.glTranslated(0, 0, -0.8);
                Gl.glScalef(1, 1f, 0.45f);
                setColor(0.4f, 0.4f, 0.4f);
                Glut.glutSolidSphere(1.65, 32, 32);
                Gl.glColor3f(0.3f, 0.3f, 0.3f);
                Gl.glLineWidth(3f);
                Glut.glutWireSphere(1.65, 10, 10);
                Gl.glPopMatrix();
            }

            //Колёса
            for (int i = 0; i < 2; i++)
            {
                Gl.glPushMatrix();
                if (i == 0) Gl.glTranslated(-1, 0, -1.5);
                else Gl.glTranslated(1, 0, -1.5);
                Gl.glScalef(0.6f, 1f, 1f);
                setColor(0,0,0);
                Glut.glutSolidSphere(0.5, 32, 32);
                Gl.glPopMatrix();
            }

            //Глаза
            for (int i = 0; i < 2; i++)
            {
                Gl.glPushMatrix();
                if (i == 0) Gl.glTranslated(-0.5, -1.6, 0);
                else Gl.glTranslated(0.5, -1.6, 0);
                Gl.glScalef(1f, 0.1f, 1f);
                setColor(1,1,1);
                Glut.glutSolidSphere(0.4, 32, 32);

                Gl.glTranslated(0, -0.4, 0);
                setColor(0, 1, 0);
                Glut.glutSolidSphere(0.25, 32, 32);

                Gl.glTranslated(0, -0.25, 0);
                setColor(0,0,0);
                Glut.glutSolidSphere(0.2, 32, 32);
                Gl.glPopMatrix();
            }

            //Руки
            for (int i = 0; i < 2; i++)
            {
                Gl.glPushMatrix();
                if (i == 0)
                {
                    Gl.glTranslated(-1.8, 0, -0.7);
                    Gl.glRotated(40, 0, 1, 0);
                }
                else
                {
                    Gl.glTranslated(1.8, 0, -0.7);
                    Gl.glRotated(-40, 0, 1, 0);
                }
                Gl.glPushMatrix();
                Gl.glScalef(0.4f, 0.4f, 0.7f);
                setColor(0.4f, 0.4f, 0.4f);
                Glut.glutSolidSphere(0.5, 32, 32);
                Gl.glColor3f(0.3f, 0.3f, 0.3f);
                Gl.glLineWidth(3f);
                Glut.glutWireSphere(0.5, 10, 10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(0, 0, -0.35);
                setColor(0.4f, 0.4f, 0.4f);
                Glut.glutSolidSphere(0.2, 32, 32);
                Gl.glColor3f(0.3f, 0.3f, 0.3f);
                Gl.glLineWidth(3f);
                Glut.glutWireSphere(0.2, 10, 10);
                Gl.glPopMatrix();

                for (int j = 0; j < 3; j++)
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(0, 0, -0.35);
                    Gl.glRotated(50 * j, 0, 1, 0);
                    Gl.glScalef(0.3f, 0.4f, 0.9f);
                    setColor(0.4f, 0.4f, 0.4f);
                    Glut.glutSolidSphere(0.4, 32, 32);
                    Gl.glColor3f(0.3f, 0.3f, 0.3f);
                    Gl.glLineWidth(3f);
                    Glut.glutWireSphere(0.4, 10, 10);
                    Gl.glPopMatrix();
                }
                Gl.glPopMatrix();
            }

            //Лампочки
            for (int i = 0; i < 2; i++)
            {
                Gl.glPushMatrix();
                if (i == 0)
                {
                    Gl.glTranslated(-1, 0, 1.4);
                    setColor(1, 0,0);
                }
                else
                {
                    Gl.glTranslated(1, 0, 1.4);
                    setColor(0, 1, 0.6f);
                }
                
                Glut.glutSolidSphere(0.3, 32, 32);
                Gl.glPopMatrix();
            }

            Gl.glPopMatrix();
        }

        private void setColor(float R, float G, float B)
        {
            RGB color = new RGB(R - deltaColor, G - deltaColor, B - deltaColor);
            Gl.glColor3f(color.getR(), color.getG(), color.getB());
        }

        public void setDeltaColor(float delta)
        {
            deltaColor = delta;
        }

    }

    class RGB
    {
        private float R;
        private float G;
        private float B;

        public RGB(float R, float G, float B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public float getR()
        {
            return R;
        }

        public float getG()
        {
            return G;
        }

        public float getB()
        {
            return B;
        }
    }
}
