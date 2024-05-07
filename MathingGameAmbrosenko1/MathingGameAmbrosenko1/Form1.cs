using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathingGameAmbrosenko1
{
    public partial class Form1 : Form
    {

        // первый щелчок указывает на первый элемент управления меткой 
        // что игрок нажимает, но это значение будет нулевым 
        // если игрок еще не нажал на ярлык
        Label firstClicked = null;

        // второй щелчок указывает на второй элемент управления меткой 
        // что игрок нажимает
        Label secondClicked = null;
        public Form1()
        {
            InitializeComponent();
            // Подготовка к игре
            AssignIconsToSquares();
        }
        // Используйте этот случайный объект, чтобы выбрать случайные значки для квадратов
        Random random = new Random();

        // Каждая из этих букв представляет собой интересный значок
        // в шрифте Webdings,
        // и каждый значок появляется в этом списке дважды
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };
        
        private void AssignIconsToSquares()
        {
            // Панель TableLayoutPanel содержит 16 меток,
            // а в списке значков всего 16 значков,
            // таким образом, значок выбирается случайным образом из списка
            // и добавляется к каждой этикетке
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    // iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            // Таймер включается только после двух несовпадающих 
            // игроку были показаны значки, 
            // поэтому игнорируйте любые щелчки, если таймер запущен
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                //  Если метка, на которую был сделан щелчок, черная, игрок нажал на значок, который уже был показан - игнорируйте щелчок
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Если значение firstClicked равно null, это первый значок в паре,
                // по которому щелкнул игрок, поэтому установите значение Firstclicked на метку,
                // по которой щелкнул игрок, измените ее цвет на черный и верните
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // Если игрок заходит так далеко, значит, таймер не запущен,
                // а значение первого нажатия не равно нулю, так что, должно быть,
                // это второй значок, на который игрок нажал, установив его цвет на черный
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Проверьте, выиграл ли игрок
                CheckForWinner();


                // Если игрок нажал на два одинаковых значка,
                // оставьте их черными и сбросьте первый и второй щелчки,
                // чтобы игрок мог нажать на другой значок
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Если игрок зашел так далеко,
                // значит, он нажал на две разные иконки,
                // поэтому запустите таймер (который будет ждать три четверти секунды, а затем скроет иконки).
                timer1.Start();
            }
        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Остановить таймер
            timer1.Stop();

            // Скрыть оба значка
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //Сбросьте первый и второй щелчки, чтобы при следующем нажатии на метку программа знала, что это был первый щелчок
            firstClicked = null;
            secondClicked = null;
        }

       
        private void CheckForWinner()
        {
            // Просмотривает все ярлыки на панели TableLayoutPanel,
            // проверяя каждый из них на соответствие значку
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Если цикл не вернулся, это означает, что пользователь выиграл. Отобразите сообщение и закройте форму.
            MessageBox.Show("Вы нашли все пары!", "Поздравляю!");
            Close();
        }


    }
 
}

