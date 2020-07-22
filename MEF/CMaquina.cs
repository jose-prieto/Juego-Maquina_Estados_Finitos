using System;
using System.Drawing;

namespace MEF
{
	// Estructura usada para los objetos y la bateria
	public struct S_objeto
	{
		public bool activo;	// Indica si el objeto es visible o no
		public int x,y;     // Coordenadas del objeto
		public Bitmap img; // Imagen del objeto
	}


	/// <summary>
	/// Esta clase representa a nuestra maquina de estados finitos.
	/// </summary>
	public class CMaquina
	{
		// Enumeracion de los diferentes estados
		public enum  estados
		{
			BUSCANDO,
			ENCONTRO,
			POCABATERIA,
			RECARGANDO,
			MUERTO,
			GANADOR,
		};

		// Esta variable representa el estado actual de la maquina
		private int Estado;

		// Estas variables son las coordenadas del robot
		private int x,y;

		// Arreglo para guardar una copia de los objetos
		private S_objeto[] objetos = new S_objeto[10];
		private S_objeto[] bateria = new S_objeto[0];

		// Variable del indice del objeto que buscamos
		private int indice;

		// Variable para la energia;
		private int energia;

		// Creamos las propiedades necesarias
		public int CoordX 
		{
			get {return x;}
		}

		public int CoordY
		{
			get {return y;}
		}

		public int EstadoM
		{
			get {return Estado;}
		}

		public int getEnergia
        {
			get { return energia; }
        }
			
		public CMaquina()
		{
			// Este es el contructor de la clase

			// Inicializamos las variables

			Estado=(int)estados.ENCONTRO;	// Colocamos el estado de inicio.
			x=320;		// Coordenada X
			y=240;		// Coordenada Y
			indice=-1;	// Empezamos como si no hubiera objeto a buscar
			energia=800;//valor energético inicial
		}

		public void Inicializa(ref S_objeto [] Pobjetos, S_objeto [] Pbateria)
		{
			// Colocamos una copia de los objetos y la bateria
			// para pode trabajar internamente con la informacion

			objetos=Pobjetos;
			bateria=Pbateria;

		}

		public void Control()
		{
			// Esta funcion controla la logica principal de la maquina de estados
			
			switch(Estado)
			{
				case (int)estados.BUSCANDO:
					// Llevamos a cabo la accion del estado
					Busqueda();

					// Verificamos por transicion
					if(x==objetos[indice].x && y==objetos[indice].y)
					{
						// Desactivamos el objeto encontrado
						objetos[indice].activo=false;
						
						// Cambiamos de estado
						Estado=(int)estados.ENCONTRO;

					}
					else if(energia<400) // Checamos condicion de transicion
						Estado=(int)estados.POCABATERIA;

					break;

				case (int)estados.ENCONTRO:
					// Revisamos si hay o no hay objetos
					NuevaBusqueda();

					// Verificamos por transicion
					if(indice==-1)	// Si ya no hay objetos, entonces GANADOR
						Estado=(int)estados.GANADOR;
					else
						Estado=(int)estados.BUSCANDO;  //si hay objetos, entonces BUSCANDO

					break;
					
				case (int)estados.POCABATERIA:
					// Llevamos a cabo la accion del estado
					IrBateria();

					// Verificamos por transicion
					if(x==bateria[0].x && y==bateria[0].y)	
						Estado=(int)estados.RECARGANDO;

					if(energia==0)
						Estado=(int)estados.MUERTO;

					break;

				case (int)estados.RECARGANDO:
					// Llevamos a cabo la accion del estado
					Recargar();

					// Hacemos la transicion
					Estado=(int)estados.BUSCANDO;

					break;

				case (int)estados.MUERTO:
					//termina el juego (Derota)
					break;

				case (int)estados.GANADOR:
					//termina el juego (victoria)
					break;

			}

		}

		public void Busqueda()
		{
			// En esta funcion colocamos la logica del estado Busqueda
			
			// Nos dirigimos hacia el objeto actual
			if(x<objetos[indice].x) //cambiamos el valor de x dependiendo si el
				x++;                //objeto está más a la derecha o izquierda
			else if(x>objetos[indice].x)//del objeto ninja
				x--;

			if(y<objetos[indice].y) //cambiamos el valor de y dependiendo si el
				y++;                //objeto está más a la arriba o abajo
			else if(y>objetos[indice].y)//del objeto ninja
				y--;

			// Disminuimos la energia
			energia--;

		}

		public void NuevaBusqueda()
		{
			// En esta funcion colocamos la logica del estado Nueva Busqueda
			// iniciamos indice en -1, si cambia de valor hay objeto
			indice=-1;
			// Recorremos el arreglo buscando algun objeto activo
			for (int n = 0; n < 10; n++)
			{
				if (objetos[n].activo == true)
				{
					if (indice == -1) //si encuentra objeto activo y aun indice no tiene nada
                    {                 //asignado toma directamente el valor de n
						indice = n;
					}//en caso contrario revisa si el nuevo objeto activo esta mas cerca
					else if (Math.Abs(objetos[n].x - x) + Math.Abs(objetos[n].y - y) <
							Math.Abs(objetos[indice].x - x) + Math.Abs(objetos[indice].y - y))
                    {
						//de estarlo cambiar el valor del indice a la posicion del nuevo objeto
						indice = n;
                    }
                }
			}
		}

		public void IrBateria()
		{
			// En esta funcion colocamos la logica del estado Ir Bateria
			// Nos dirigimos hacia la bateria
			if(x<bateria[0].x)
				x++;
			else if(x>bateria[0].x)
				x--;

			if(y<bateria[0].y)
				y++;
			else if(y>bateria[0].y)
				y--;
			// Disminuimos la energia
			energia--;
		}

		public void Recargar()
		{
			// En esta funcion colocamos la logica del estado Recargar
			energia=800;
			//vuelve a calcular "NuevaBusqueda()" para saber cual fruta esta mas cerca
			NuevaBusqueda();
		}
	}
}
