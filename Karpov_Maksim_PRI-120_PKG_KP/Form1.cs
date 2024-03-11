using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Karpov_Maksim_PRI_120_PKG_KP
{
    enum Scene
    {
        First,
        SecondGameNotStarted,
        SecondGameStarted
    }

    enum RotateBibi
    {
        Left,
        Right,
        Forward,
        Back
    }
    public partial class Form1 : Form
    {

        //Управление положением пользователя
        double angle = 3, angleX = -96, angleY = 0, angleZ = -30;
        double sizeX = 1, sizeY = 1, sizeZ = 1;
        double translateX = -9, translateY = -60, translateZ = -10;
        double cameraSpeed;

        //Переменные для работы с текстурами
        string spaceTexture = "background.jpg";
        uint spaceSign;
        int imageId;

        //Сцена 2. Игра
        bool isGameStarted, isGameWon, isGameLost = false;
        bool isSpaceshipIntact = true, isSpaceshipTranclatingAfterCrash = true;
        double deltaXMeteorit, translateZMeteorit = 0;
        int countOfMeteorits = 0;
        double power = 50;

        Spaceship spaceship = new Spaceship();
        Random random = new Random();
        RotateBibi rotateBibi = RotateBibi.Forward;
        //Проигрывание аудио
        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();


        //Сцена 3. Би-Би
        double deltaXBibi, deltaYBibi;

        //Delta-переменные
        double deltaZUserSpaceship, deltaRotateSystemSpaceship, deltaXSpaceShip = 0;
        bool isSpaceshipUp = true;

        private readonly Explosion explosion = new Explosion(704, -10, 300, 300, 50);


        public void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            Gl.glRotated(angleX, 1, 0, 0);
            Gl.glRotated(angleY, 0, 1, 0);
            Gl.glRotated(angleZ, 0, 0, 1);
            Gl.glTranslated(translateX, translateY, translateZ);
            Gl.glScaled(sizeX, sizeY, sizeZ);
            explosion.Calculate(global_time);
            spaceship.drawBackground(spaceSign);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    drawFirstScene();
                    break;
                case 1:
                    drawSecondScene();
                    break;
                case 2:
                    drawThirdScene();
                    break;
            }

            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }

        private void drawFirstScene()
        {
            spaceship.drawSpaceship(deltaRotateSystemSpaceship, Scene.First, isSpaceshipIntact, deltaXSpaceShip);
            changeSpaceshipRotate();
        }

        private void drawSecondScene()
        {
            if (!isGameStarted)
            {
                if (!isSpaceshipIntact && isSpaceshipTranclatingAfterCrash)
                {
                    if (deltaXSpaceShip >= 60)
                    {
                        isSpaceshipTranclatingAfterCrash = false;
                    }
                    else
                    {
                        deltaXSpaceShip += 5 * power / 50;
                        power--;
                    }
                }
                spaceship.drawSpaceship(deltaZUserSpaceship, Scene.SecondGameNotStarted, isSpaceshipIntact, deltaXSpaceShip);
                
            }
            else
            {
                spaceship.drawSpaceship(deltaZUserSpaceship, Scene.SecondGameStarted, isSpaceshipIntact, deltaXSpaceShip);
                spaceship.drawMeteorit(deltaXMeteorit, translateZMeteorit);
                checkDeltaMeteorit();
                
            }
        }

        private void drawThirdScene()
        {
            spaceship.drawMoon();
            spaceship.drawBibi(rotateBibi, deltaXBibi, deltaYBibi);
        }

        private void changeSpaceshipRotate()
        {
            if (deltaRotateSystemSpaceship >= 10)
            {
                isSpaceshipUp = false;
                deltaRotateSystemSpaceship = 9;
            }
            else if (deltaRotateSystemSpaceship <= -10)
            {
                isSpaceshipUp = true;
                deltaRotateSystemSpaceship = -9;
            }
            else if (isSpaceshipUp) deltaRotateSystemSpaceship++;
            else deltaRotateSystemSpaceship--;
        }

        private void checkDeltaMeteorit()
        {
            if (deltaXMeteorit > 500)
            {
                countOfMeteorits++;
                if (countOfMeteorits == 4)
                {
                    isGameWon = true;
                    isGameStarted = false;
                    initSecondPosition();

                    Gl.glPushMatrix();
                    explosion.SetNewPosition(40, -280, 15);
                    explosion.SetNewPower(50);
                    explosion.Boooom(global_time);
                    Gl.glPopMatrix();
                }
                else
                {
                    deltaXMeteorit = 0;
                    translateZMeteorit = random.Next(-100, 100);
                }
            }
            else if (deltaXMeteorit > 230 && deltaXMeteorit < 420)
            {
                if ((deltaZUserSpaceship > translateZMeteorit && deltaZUserSpaceship - translateZMeteorit <= 35) ||
                   (deltaZUserSpaceship <= translateZMeteorit) && translateZMeteorit - deltaZUserSpaceship <= 35)
                {
                    isGameLost = true;
                    isGameStarted = false;
                    isSpaceshipIntact = false;
                    isSpaceshipTranclatingAfterCrash = true;
                    initSecondPosition();
                }
                else
                    deltaXMeteorit += 10;
            }
            else
                deltaXMeteorit+=10;
        }



        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            global_time += (float)RenderTimer.Interval / 1000;
            Draw();
        }


        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        float global_time = 0;

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация openGL (glut)
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // цвет очистки окна
            Gl.glClearColor(255, 255, 255, 1);

            // настройка порта просмотра
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(60, (float)AnT.Width / (float)AnT.Height, 0.1, 900);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            cameraSpeed = 5;

            spaceSign = genImage(spaceTexture);

            RenderTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isGameLost = false;
            isGameWon = false;
            isGameStarted = true;
            isSpaceshipIntact = true;
            deltaXSpaceShip = 0;
            isSpaceshipTranclatingAfterCrash = false;
            power = 50;
            deltaZUserSpaceship = 0;
            deltaXMeteorit = 0;
            translateZMeteorit = 0;
            countOfMeteorits = 0;
            initSecondPosition();
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                translateY -= cameraSpeed;

            }
            if (e.KeyCode == Keys.S)
            {
                translateY += cameraSpeed;
            }
            if (e.KeyCode == Keys.A)
            {
                translateX += cameraSpeed;
            }
            if (e.KeyCode == Keys.D)
            {
                translateX -= cameraSpeed;

            }
            if (e.KeyCode == Keys.ControlKey)
            {
                translateZ += cameraSpeed;

            }
            if (e.KeyCode == Keys.Space)
            {
                translateZ -= cameraSpeed;
            }
            if (e.KeyCode == Keys.R)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX += angle;

                        break;
                    case 1:
                        angleY += angle;

                        break;
                    case 2:
                        angleZ += angle;

                        break;
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.E)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX -= angle;
                        break;
                    case 1:
                        angleY -= angle;
                        break;
                    case 2:
                        angleZ -= angle;
                        break;
                    default:
                        break;
                }
            }
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    if (e.KeyCode == Keys.I) deltaZUserSpaceship++; else
                        if (e.KeyCode == Keys.K) deltaZUserSpaceship--;
                    break;
                case 2:
                    if (e.KeyCode == Keys.F && deltaXBibi>-10)
                    {
                        deltaXBibi--;
                        rotateBibi = RotateBibi.Left;
                    }
                    else if (e.KeyCode == Keys.H && deltaXBibi < 10)
                    {
                        deltaXBibi++;
                        rotateBibi = RotateBibi.Right;
                    }
                    else if (e.KeyCode == Keys.G && deltaYBibi > -7)
                    {
                        deltaYBibi--;
                        rotateBibi = RotateBibi.Forward;
                    }
                    else if (e.KeyCode == Keys.T && deltaYBibi < 0)
                    {
                        deltaYBibi++;
                        rotateBibi = RotateBibi.Back;
                    }
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                angle = 3; angleX = -90; angleY = 0; angleZ = 0;
                sizeX = 1; sizeY = 1; sizeZ = 1;
                translateX = -40; translateY = 200; translateZ = -25;
                initFirstPosition();
                WMP.URL = @"kikoriki.mp3";
                WMP.controls.play();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                translateX = -60; translateY = 300; translateZ = -25;
                angle = 3; angleX = -90; angleY = 0; angleZ = 0;
                initSecondPosition();
                WMP.URL = @"ot_vinta.mp3";
                WMP.controls.play();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                translateX = -150; translateY = 100; translateZ = -25;
                angle = 3; angleX = -90; angleY = 0; angleZ = 0;
                initThirdPosition();
                WMP.URL = @"kikoriki.mp3";
                WMP.controls.play();
            }
            AnT.Focus();
        }

        private void initFirstPosition()
        {
            button1.Visible = false;
            label8.Text = "Земля в иллюминаторе...";
        }

        private void initSecondPosition()
        {

            if (!isGameStarted)
            {
                button1.Visible = true;
                button1.Text = "Играть";
                button1.Text = "Играть";
                label8.Text = "Сыграем?";
            }
            else
            {
                button1.Visible = false;
                label8.Text = "Уклоняйся от метеоритов!";
            }
            if (isGameWon) label8.Text = "Поздравляем с победой!";
            else if (isGameLost) label8.Text = "Повезёт в любви...";
        }

        private void initThirdPosition()
        {
            button1.Visible = false;
            label8.Text = "Это Би-Би, он тут живёт";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnT.Focus();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
                cameraSpeed = (double)numericUpDown1.Value;
            AnT.Focus();
        }

        private uint genImage(string image)
        {
            uint sign = 0;
            Il.ilGenImages(1, out imageId);
            Il.ilBindImage(imageId);
            if (Il.ilLoadImage(image))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        sign = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        sign = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(1, ref imageId);
            return sign;
        }

        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            uint texObject;
            Gl.glGenTextures(1, out texObject);
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            switch (Format)
            {

                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

            }
            return texObject;
        }
    }
}
