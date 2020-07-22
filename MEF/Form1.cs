using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace MEF
{
    
    public class Form1 : Form
    {
        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private MenuItem mnuSalir;
        private MenuItem menuItem3;
        private MenuItem mnuInicio;
        private MenuItem mnuParo;
        private Timer timer1;
        private IContainer components;

        // Creamos un objeto para la maquina de estados finitos
        private CMaquina maquina = new CMaquina();

        // Objetos necesarios
        public S_objeto[] ListaObjetos = new S_objeto[10];
        public S_objeto[] MiBateria = new S_objeto[1];
        public S_objeto MiNinja;
        public bool gameState = true;
        public bool play_low_battery = true;
        public bool play_found = false;
        public Form1()
        {
            //
            // Necesario para admitir el Dise�ador de Windows Forms
            //
            InitializeComponent();

            //
            // TODO: agregar c�digo de constructor despu�s de llamar a InitializeComponent
            //

            // Inicializamos los objetos

            // Cremos un objeto para tener valores aleatorios
            Random random = new Random();

            // Recorremos todas las frutas
            for (int n = 0; n < 10; n++)
            {
                // Colocamos las coordenadas
                ListaObjetos[n].x = random.Next(0, 639);
                ListaObjetos[n].y = random.Next(50, 479);
                // se verifica que la fruta que se esta creando no se solape con alguna otra
                check_exist(ListaObjetos, random, n);
                // Lo indicamos activo
                ListaObjetos[n].activo = true;

                // Asignamos la imagen que llevar� la fruta
                Bitmap[] imgResource = { Properties.Resources.apple, Properties.Resources.cherry, 
                                        Properties.Resources.raspberry, Properties.Resources.strawberry };
                int randomFruit = random.Next(imgResource.Length);
                ListaObjetos[n].img = new Bitmap(imgResource[randomFruit]);
            }

            // Colocamos las coordenadas de la bateria
            MiBateria[0].x = random.Next(0, 639);
            MiBateria[0].y = random.Next(50, 479);
            //se verifica que la bateria no se solape con otro objeto
            check_exist(ListaObjetos, random, 9);
            // activamos el objeto bateria
            MiBateria[0].activo = true;
            //colocamos la respectiva imagen a bater�a
            MiBateria[0].img = new Bitmap(Properties.Resources.battery);

            //iniciamos la m�quina de estados finitos
            maquina.Inicializa(ref ListaObjetos, MiBateria);

        }

        private void check_exist(S_objeto[] ListaObjetos, Random random, int n)
        {
            bool exist = false;

            while (!exist)
            {
                for (int k = 0; k < n + 1; k++)
                {//con esto se evita que los objetos se solapen entre s� evitando errores en el aut�mata finito
                    if (ListaObjetos[k].x - 30 <= ListaObjetos[n].x && ListaObjetos[n].x <= ListaObjetos[k].x + 30
                        && ListaObjetos[k].y - 30 <= ListaObjetos[n].y && ListaObjetos[n].y <= ListaObjetos[k].y + 30)
                    {
                        //en caso de solapar cambia su posici�n
                        ListaObjetos[n].x = random.Next(0, 639);
                        ListaObjetos[n].y = random.Next(50, 479);
                    }
                    else
                        exist = true;
                }
                exist = true;
            }
        }

        #region C�digo generado por el Dise�ador de Windows Forms
        /// <summary>
        /// M�todo necesario para admitir el Dise�ador. No se puede modificar
        /// el contenido del m�todo con el editor de c�digo.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuSalir = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuInicio = new System.Windows.Forms.MenuItem();
            this.mnuParo = new System.Windows.Forms.MenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSalir});
            this.menuItem1.Text = "Archivo";
            // 
            // mnuSalir
            // 
            this.mnuSalir.Index = 0;
            this.mnuSalir.Text = "Salir";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuInicio,
            this.mnuParo});
            this.menuItem3.Text = "Aplicacion";
            // 
            // mnuInicio
            // 
            this.mnuInicio.Index = 0;
            this.mnuInicio.Text = "Inicio";
            this.mnuInicio.Click += new System.EventHandler(this.mnuInicio_Click);
            // 
            // mnuParo
            // 
            this.mnuParo.Index = 1;
            this.mnuParo.Text = "Paro";
            this.mnuParo.Click += new System.EventHandler(this.mnuParo_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(692, 500);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Fruit Ninja";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Punto de entrada principal de la aplicaci�n.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void mnuSalir_Click(object sender, System.EventArgs e)
        {
            // Cerramos la ventana y finalizamos la aplicacion
            this.Close();
        }

        private void mnuInicio_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void mnuParo_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            // Esta funcion es el handler del timer
            // Aqui tendremos la logica para actualizar nuestra maquina de estados

            if (gameState)
            {
                // Actualizamos a la maquina
                maquina.Control();

                // Mandamos a redibujar la pantalla

                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Creamos la fuente y la brocha para el texto
            Font fuente = new Font("Arial", 16);
            SolidBrush brocha = new SolidBrush(Color.White);
            SolidBrush brochaAmarilla = new SolidBrush(Color.Yellow);
            SolidBrush brochaNegra = new SolidBrush(Color.Black);
            SolidBrush brochaRoja = new SolidBrush(Color.Red);
            SolidBrush brochaNaranja = new SolidBrush(Color.Orange);
            MiNinja.activo = true;
            MiNinja.img = new Bitmap(Properties.Resources.cesta);

            // Dibujamos las frutas
            for (int n = 0; n < 10; n++)
            {
                if (ListaObjetos[n].activo == true) //de estar activo dibuja la fruta
                    e.Graphics.DrawImage(ListaObjetos[n].img, ListaObjetos[n].x - 4, ListaObjetos[n].y - 4, 20, 20);
                else //de lo contrario dibuja un arbol seco en lugar de la fruta
                    e.Graphics.DrawImage(Properties.Resources.dry_tree, ListaObjetos[n].x - 4, ListaObjetos[n].y - 4, 20, 20);
            }

            // Dibujamos la bateria
            e.Graphics.DrawImage(MiBateria[0].img, MiBateria[0].x - 4, MiBateria[0].y - 4, 20, 20);

            

            // Indicamos la energia del ninja
            e.Graphics.DrawImage(Properties.Resources.energy, this.Width - 130, 10, 20, 20);
            if (maquina.getEnergia <= 200) //barra de energ�a roja si <= 200
            {
                e.Graphics.DrawRectangle(Pens.Red, this.Width - 110, 12, 80, 10);
                e.Graphics.FillRectangle(brochaRoja, this.Width - 110, 12, maquina.getEnergia / 10, 10);
            }
            else if (maquina.getEnergia <= 400) //barra de energ�a anaranjada si <= 400
            {
                e.Graphics.DrawRectangle(Pens.Orange, this.Width - 110, 12, 80, 10);
                e.Graphics.FillRectangle(brochaNaranja, this.Width - 110, 12, maquina.getEnergia / 10, 10);
            }
            else //barra de energ�a amarilla si ninguna de las anteriores se cumplen
            {
                e.Graphics.DrawRectangle(Pens.Yellow, this.Width - 110, 12, 80, 10);
                e.Graphics.FillRectangle(brochaAmarilla, this.Width - 110, 12, maquina.getEnergia / 10, 10);
            }

            e.Graphics.DrawImage(MiNinja.img, maquina.CoordX - 4, maquina.CoordY - 4); //Dibujamos al ninja

            switch (maquina.EstadoM)
            {
                case (int)CMaquina.estados.MUERTO:
                    //se dibuja rectangulo negro
                    e.Graphics.FillRectangle(brochaNegra, 0, 0, this.Width, this.Height);
                    //se escribe "you died"
                    e.Graphics.DrawString("YOU DIED", new Font("Times New Roman", 30), new SolidBrush(Color.Red),
                                                                this.Width / 2 - 100, this.Height / 2 - 60);
                    // Sonamos un beep de la computadora
                    System.Media.SoundPlayer gameover = new System.Media.SoundPlayer(Properties.Resources.died);
                    gameover.Play();
                    //se le indica a la m�quina que el juego ha terminado
                    gameState = false;
                    break;

                case (int)CMaquina.estados.GANADOR:
                    //se escribe estado actual
                    e.Graphics.DrawString("Estado: Ganador", fuente, brocha, 10, 10);
                    // sonamos beep de victoria
                    System.Media.SoundPlayer winner = new System.Media.SoundPlayer(Properties.Resources.victory);
                    winner.Play();
                    //se le indica a la m�quina que el juego ha terminado
                    gameState = false;
                    break;

                case (int)CMaquina.estados.BUSCANDO:
                    //se escribe estado actual
                    e.Graphics.DrawString("Estado: Buscando", fuente, brocha, 10, 10);
                    play_found = true;
                    break;

                case (int)CMaquina.estados.ENCONTRO:
                    //se escribe estado actual
                    e.Graphics.DrawString("Estado: Encontr�", fuente, brocha, 10, 10);
                    if (play_found)
                    {
                        System.Media.SoundPlayer found = new System.Media.SoundPlayer(Properties.Resources.found);
                        found.PlaySync();
                        play_found = false;
                    }
                    break;

                case (int)CMaquina.estados.POCABATERIA:
                    //se escribe estado actual
                    e.Graphics.DrawString("Estado: Poca energ�a", fuente, brocha, 10, 10);
                    if (play_low_battery)
                    {
                        System.Media.SoundPlayer low_battery = new System.Media.SoundPlayer(Properties.Resources.low_battery);
                        low_battery.PlaySync();
                        play_low_battery = false;
                    }
                    break;

                case (int)CMaquina.estados.RECARGANDO:
                    //se escribe estado actual
                    e.Graphics.DrawString("Estado: Recargando", fuente, brocha, 10, 10);
                    System.Media.SoundPlayer charging = new System.Media.SoundPlayer(Properties.Resources.charging);
                    charging.PlaySync();
                    play_low_battery = true;
                    break;
            }
        }
    }
}
