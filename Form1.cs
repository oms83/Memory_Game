using Memory_Game.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game
{
    public partial class Form1 : Form
    {
        Button []arrCurrentButtons = new Button[2];
        Button []arrButtons = new Button[16];

        public stPlayerInfo PlayerInfo;
        public stGameInfo GameInfo;
        SoundPlayer SoundEffect;

        public struct stPlayerInfo
        {
            public string Name;
        }

        public struct stGameInfo
        {
            public short Score;
            public short Time;
            public short Move;
            public bool IsFirstClick;
        }
        
        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        public void FillArrayButtons()
        {
            arrButtons[0] = button1;
            arrButtons[1] = button2;
            arrButtons[2] = button3;
            arrButtons[3] = button4;
            arrButtons[4] = button5;
            arrButtons[5] = button6;
            arrButtons[6] = button7;
            arrButtons[7] = button8;
            arrButtons[8] = button9;
            arrButtons[9] = button10;
            arrButtons[10] = button11;
            arrButtons[11] = button12;
            arrButtons[12] = button13;
            arrButtons[13] = button14;
            arrButtons[14] = button15;
            arrButtons[15] = button16;
        }

        public void Swap(ref Button btn1, ref Button btn2)
        {
            Button Temp = new Button();
            Temp.Tag = btn1.Tag;
            btn1.Tag = btn2.Tag;
            btn2.Tag = Temp.Tag;
        }

        public void ShufflegameButtons(ref Button[] GameButtons)
        {
            Random rnd = new Random();
            for(short i=0 ; i<GameButtons.Length; i++)
            {
                Swap(ref GameButtons[rnd.Next(0, GameButtons.Length - 1)], ref GameButtons[rnd.Next(0, GameButtons.Length - 1)]);
            }
        }

        public void ShowImage(Button btn)
        {
            switch (btn.Tag)
            {
                case "loin":
                    btn.BackgroundImage = Resources.lion;
                    break;
                case "hen":
                    btn.BackgroundImage = Resources.hen;
                    break;
                case "elephant":
                    btn.BackgroundImage = Resources.elephant;
                    break;
                case "cat":
                    btn.BackgroundImage = Resources.cat;
                    break;
                case "dog":
                    btn.BackgroundImage = Resources.dog;
                    break;
                case "whale":
                    btn.BackgroundImage = Resources.whale;
                    break;
                case "giraffe":
                    btn.BackgroundImage = Resources.giraffe;
                    break;
                case "monkey":
                    btn.BackgroundImage = Resources.monkey;
                    break;
            }       

        }

        public void PlayEndSoundEffect(bool PlayerWon)
        {
            if(PlayerWon)
            {
                SoundEffect = new SoundPlayer(@"C:\Users\omerm\Downloads\tone\win.wav");
                SoundEffect.Play();
                Thread.Sleep(2000);
                SoundEffect.Stop();

            }
            else
            {
                SoundEffect = new SoundPlayer(@"C:\Users\omerm\Downloads\tone\lose.wav");
                SoundEffect.Play();
                Thread.Sleep(2000);
                SoundEffect.Stop();
            }
        }
        public void HideScorePictures(bool PictureBocVisabelStatus)
        {
            foreach(PictureBox pb in panel1.Controls)
            {
                pb.Visible = PictureBocVisabelStatus;
            }
        }

        public void DisableButtons(bool ButtonStatus)
        {
            foreach (Button btn in pnlButtons.Controls)
            {
                btn.Enabled = ButtonStatus;
            }
        }

        public void RestButtonsPicture()
        {
            foreach(Button btn in pnlButtons.Controls)
            {
                btn.BackgroundImage = Resources.question;
            }
        }
        
        public void EndGame()
        {
                if(GameInfo.Score == 8)
                {
                    PlayEndSoundEffect(true);
                    timer1.Stop();
                    DisableButtons(false);
                    btnRestart.Enabled = true;
                    btnStart.Enabled = false;

                }

                if (GameInfo.Time == 0)
                {
                    PlayEndSoundEffect(false);
                    timer1.Stop();
                    DisableButtons(false);
                    btnRestart.Enabled = true;
                    btnStart.Enabled = false;
                }
        }
        
        public void SetScore()
        {
            switch(++GameInfo.Score)
            {
                case 1:
                    pb1.Visible = true;
                    break;
                case 2:
                    pb2.Visible = true;
                    break;
                case 3:
                    pb3.Visible = true;
                    break;
                case 4:
                    pb4.Visible = true;
                    break;
                case 5:
                    pb5.Visible = true;
                    break;
                case 6:
                    pb6.Visible = true;
                    break;
                case 7:
                    pb7.Visible = true;
                    break;
                case 8:
                    pb8.Visible = true;
                    break;
            }

        }

        public void CheckIfImagesMatch(Button btn1, Button btn2)
        {
            if (btn1.Tag == btn2.Tag)
            {
                SetScore();
                btn1.Enabled = false;
                btn2.Enabled = false;
                EndGame();

            }
            else
            {
                btn1.BackgroundImage = Resources.question;
                btn2.Refresh();
                Thread.Sleep(500);
                btn2.BackgroundImage = Resources.question;
            }
        }
        
        private void ClickButton(object sender, EventArgs e)
        {
            GameInfo.Move++;
            lblMove.Text = GameInfo.Move.ToString();
            
            if(GameInfo.IsFirstClick)
            {
                arrCurrentButtons[0] = (Button)sender;
                ShowImage(arrCurrentButtons[0]);
                GameInfo.IsFirstClick = false;
            }
            else
            {
                arrCurrentButtons[1] = (Button)sender;

                if (arrCurrentButtons[1] != arrCurrentButtons[0])
                {
                    ShowImage(arrCurrentButtons[1]);
                    CheckIfImagesMatch(arrCurrentButtons[0], arrCurrentButtons[1]);
                    GameInfo.IsFirstClick = true;
                }

            }
        }

        public void RestGameInfo()
        {
            GameInfo.Move = 0;
            GameInfo.IsFirstClick = true;
            GameInfo.Time = 60;
            GameInfo.Score = 0;
            lblMove.Text = 0.ToString();
            lblName.Text = string.Empty;
        }
        public void RestTimeLebals()
        {
            lbl1.Text = string.Empty;
            lbl2.Text = string.Empty;
            lbl3.Text = string.Empty;
            lbl4.Text = string.Empty;
            lbl5.Text = string.Empty;
            lbl6.Text = string.Empty;
        }

        public void RestartGame()
        {
            RestButtonsPicture();
            RestGameInfo();
            HideScorePictures(false);
            DisableButtons(false);
            RestTimeLebals();

            btnStart.Enabled = true;
            btnRestart.Enabled = false;

        }

        public void StartGame()
        {
            DisableButtons(true);
            FillArrayButtons();
            ShufflegameButtons(ref arrButtons);
            btnRestart.Enabled = false;
            btnStart.Enabled = false;
            timer1.Start();
        }

        public void GetPlayerInfo()
        {
            frmPlayerInfo frm = new frmPlayerInfo();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                PlayerInfo.Name = frm.FullName;
                lblName.Text = PlayerInfo.Name;
            }
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {

            GetPlayerInfo();
            StartGame();
        }
        
        private void SetlblTimeForeColor(Label lbl)
        {
            if (GameInfo.Time >= 45)
                lbl.ForeColor = Color.GreenYellow;

            else if (GameInfo.Time >= 30)
                lbl.ForeColor = Color.Yellow;

            else if (GameInfo.Time >= 15)
                lbl.ForeColor = Color.Orange;

            else if (GameInfo.Time >= 0)
                lbl.ForeColor = Color.Red;
        }
        
        public void ShowTime(Label lbl)
        {
            SetlblTimeForeColor(lbl);

            //lbl.Visible = true;
            lbl.Text = GameInfo.Time.ToString();
            //lbl.Refresh();
            //Thread.Sleep(1000);
            //lbl.Visible = false;
        }

        public void EditTime()
        {

            switch (GameInfo.Time % 6)
            {
                case 0:
                    ShowTime(lbl1);
                    break;
                case 1:
                    ShowTime(lbl6);
                    break;
                case 2:
                    ShowTime(lbl5);
                    break;
                case 3:
                    ShowTime(lbl4);
                    break;
                case 4:
                    ShowTime(lbl3);
                    break;
                case 5:
                    ShowTime(lbl2);
                    break;
            }

            if (GameInfo.Time >= 0)
            {
                GameInfo.Time--;
                EndGame();

            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            EditTime();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    
    
    }
}
