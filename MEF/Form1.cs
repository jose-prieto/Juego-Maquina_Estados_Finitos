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
        public S_objeto MiBateria;
        public bool gameState = true;
        public Form1()
        {
            //
            // Necesario para admitir el Diseñador de Windows Forms
            //
            InitializeComponent();

            //
            // TODO: agregar código de constructor después de llamar a InitializeComponent
            //

            // Inicializamos los objetos

            // Cremos un objeto para tener valores aleatorios
            Random random = new Random();

            // Recorremos todos los objetos
            for (int n = 0; n < 10; n++)
            {
                // Colocamos las coordenadas
                ListaObjetos[n].x = random.Next(0, 639);
                ListaObjetos[n].y = random.Next(0, 479);

                // Lo indicamos activo
                ListaObjetos[n].activo = true;

                // Seteamos la imagen
                Bitmap[] imgResource = { Properties.Resources.apple, Properties.Resources.cherry, Properties.Resources.raspberry, Properties.Resources.strawberry };
                int randomFruit = random.Next(imgResource.Length);
                ListaObjetos[n].img = new Bitmap(imgResource[randomFruit]);
            }

            // Colocamos la bateria
            MiBateria.x = random.Next(0, 639);
            MiBateria.y = random.Next(0, 479);
            MiBateria.activo = true;
            MiBateria.img = new Bitmap(Properties.Resources.battery);

            maquina.Inicializa(ref ListaObjetos, MiBateria);


        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms
        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
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
            this.Text = "Maquina de estados finitos";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
        }
        #endregion

        /// <summary>
        /// Punto de entrada principal de la aplicación.
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

            // Dibujamos los objetos
            for (int n = 0; n < 10; n++)
                if (ListaObjetos[n].activo == true)
                    e.Graphics.DrawImage(ListaObjetos[n].img, ListaObjetos[n].x - 4, ListaObjetos[n].y - 4, 20, 20);

            // Dibujamos la bateria
            e.Graphics.DrawImage(MiBateria.img, MiBateria.x - 4, MiBateria.y - 4, 20, 20);

            // Indicamos el estado en que se encuentra la maquina
            e.Graphics.DrawString("Estado: " + maquina.EstadoM.ToString(), fuente, brocha, 10, 10);

            // Indicamos la energia de la culebrita
            e.Graphics.DrawImage(Properties.Resources.energy, this.Width - 130, 10, 20, 20);
            e.Graphics.DrawRectangle(Pens.Yellow, this.Width-110, 12, 80, 10);
            e.Graphics.FillRectangle(brochaAmarilla, this.Width - 110, 12, maquina.getEnergia/10, 10);           

            // Dibujamos el robot
            if (maquina.EstadoM == (int)CMaquina.estados.MUERTO)
            {
                e.Graphics.FillRectangle(brochaNegra, 0, 0, this.Width, this.Height);
                e.Graphics.DrawString("YOU DIED", new Font("Times New Roman", 30), new SolidBrush(Color.Red), this.Width / 2 - 100, this.Height / 2 - 60);
                // Sonamos un beep de la computadora
                System.Media.SoundPlayer gameover = new System.Media.SoundPlayer(Properties.Resources.died);
                gameover.Play();
                gameState = false;
            }
            else
                e.Graphics.DrawRectangle(Pens.Green, maquina.CoordX - 4, maquina.CoordY - 4, 20, 20);

            switch(maquina.EstadoM)
            {
                case (int)CMaquina.estados.MUERTO:
                    e.Graphics.FillRectangle(brochaNegra, 0, 0, this.Width, this.Height);
                    e.Graphics.DrawString("YOU DIED", new Font("Times New Roman", 30), new SolidBrush(Color.Red), this.Width / 2 - 100, this.Height / 2 - 60);
                    // Sonamos un beep de la computadora
                    System.Media.SoundPlayer gameover = new System.Media.SoundPlayer(Properties.Resources.died);
                    gameover.Play();
                    gameState = false;
                    break;

                case (int)CMaquina.estados.ALEATORIO:
                    System.Media.SoundPlayer winner = new System.Media.SoundPlayer(Properties.Resources.victory);
                    winner.Play();
                    gameState = false;
                    break;

                default:
                    e.Graphics.DrawRectangle(Pens.Green, maquina.CoordX - 4, maquina.CoordY - 4, 20, 20);
                    break;

            }
        }
    }
}
