﻿using System;
using System.IO;  // para poder operar con archivos
using System.Drawing; // para poder operar con fotos
using System.Drawing.Imaging;

namespace PROYECTO_IO
{
    /***********************************************************************************************************
     * Nombre de la clase: CPixel
     * Funcionalidad: almacena los valores R, G y B, correspondientes al color de un pixel de una imagen
     * Parametros de entrada:
     *  --> R: int. Valor numérico de rojo de 0 a 255
     *  --> G: int. Valor numérico de verde de 0 a 255
     *  --> B: int. Valor numérico de azul de 0 a 255
     ***********************************************************************************************************/
    class CPixel
    {
        public int R;
        public int G;
        public int B;

        public CPixel(int red, int green, int blue)
        {
            R = red;
            G = green;
            B = blue;
        }
    }

    public class CUsuario // Creamos la clase en la que almacenamos datos de los usuarios
    {
        public string nombre;
        public string correo;
        public string contraseña;
    }

    public class CLista
    {
        public CUsuario[] usuarios;
    }

    internal class Program
    {
        /************************************************************************************************************************
         * Nombre de la función: PNGtoMATRIZ
         * Funcionalidad: abre una imagen en formato .png o .jpg y la convierte a una matriz de clase Pixel
         * Parametros de entrada:
         *  --> imageName: String que contiene el nombre de la imagen a abrir. Debe incluir el .png o .jpg
         *  Devuelve:
         *  --> Matriz de clase Pixel: donde cada Pixel contiene los valores R, G y B de cada pixel de la imagen original. 
         *         La forma de la matriz es [anchura, altura]. La posición [0,0] representa el valor de arriba a la izquierda y 
         *         la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> Valor null si la imagen no se encuentra.
         * **********************************************************************************************************************/
        static CPixel[,] PNGtoMATRIZ(string fotoelegida) //De .png a matriz inimage
        {
            if (fotoelegida != null)
            {
                string inputfile = fotoelegida;
                Bitmap inimage = new Bitmap(inputfile); // Abrimos la imagen como Bitmap
                CPixel[,] outimage = new CPixel[inimage.Width, inimage.Height]; //Matriz de Pixeles que devolveremos

                int j, i;

                for (i = 0; i < inimage.Width; i++) //Rellenamos el vector con los valores de la imagen
                {
                    for (j = 0; j < inimage.Height; j++)
                    {
                        Color color = inimage.GetPixel(i, j);
                        outimage[i, j] = new CPixel(color.R, color.G, color.B);
                    }
                }
                return outimage; //matriz de pixeles
            }
            return null;
        }

        /***********************************************************************************************************
         * Nombre de la función: MATRIZtoPNG
         * Funcionalidad: recibe una matriz de clase Pixel y un nombre de archivo y guarda la imagen en formato .png o .jpg
         * Parametros de entrada:
         *  --> image: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> filename: String que contiene el nombre con el que se guardará la imagen. Debe incluir el .png o .jpg
         *  Devuelve:
         *  --> True: Si se ha podido guardar la imagen
         *  --> False: Si no se ha podido guardar la imagen.
         ***********************************************************************************************************/
        static bool MATRIZtoPNG(CPixel[,] inimage, string filename) //De matriz result a .png
        {
            if (inimage != null && filename != null)
            {
                Bitmap outimage = new Bitmap(inimage.GetLength(0), inimage.GetLength(1));

                for (int i = 0; i < inimage.GetLength(0); i++) // Recorrido por la imagen para guardar los valores
                {
                    for (int j = 0; j < inimage.GetLength(1); j++)
                    {
                        outimage.SetPixel(i, j, Color.FromArgb(inimage[i, j].R, inimage[i, j].G, inimage[i, j].B));
                    }
                }
                outimage.Save(filename); // Guardar la imagen
                return true;
            }
            return false;
        }

        /***********************************************************************************************************
         * Nombre de la función: reverse
         * Funcionalidad: recibe una matriz de clase Pixel y devuelve una matriz de clase Pixel con los valores espejo.
         * Parametros de entrada:
         *  --> Image: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *
         *  Devuelve:
         *  --> Matriz de clase Pixel. Cada Pixel contiene los valores R, G y B.
         *              La forma de la matriz es la misma que la matriz de entrada
         *              La posición [0,0] representa el valor de arriba a la derecha de la matriz original y 
         *              la posición [anchura,altura] el valor de abajo a la izquierda de la matriz original
         *  --> null: Si no se ha pasado un parámetro de entrada != null.
         ***********************************************************************************************************/
        static CPixel[,] reverse(CPixel[,] inimage)
        {
            CPixel[,] result = null;
            int width, height;

            if (inimage != null)
            {
                width = inimage.GetLength(0);
                height = inimage.GetLength(1);
                result = new CPixel[width, height];
                int j, i;

                for (i = 0; i < width; i++)
                {
                    for (j = 0; j < height; j++)
                    {
                        result[i, j] = inimage[width - i - 1, j]; // Igualar al lado opuesto
                    }
                }
                return result;
            }
            return result;
        }

        // Enmarca la foto con un color negro
        static CPixel[,] enmarcar(CPixel[,] inimage)
        {
            CPixel[,] result = null;
            if (inimage != null)
            {
                int width = inimage.GetLength(0);
                int height = inimage.GetLength(1);
                result = new CPixel[width, height];
                int j, i;
                result = inimage;
                
                CPixel colorNegro = new CPixel(0, 0, 0); 

                for (i = 0; i < width; i++)
                {
                    for (j = 0; j < height; j++)
                    {
                        if(i == 0 || i == width - 1 || j == 0 || j == height - 1) // verificamos si estamos en los bordes
                        {
                            result[i, j] = new CPixel(colorNegro.R, colorNegro.G, colorNegro.B);
                        }
                    }
                }
                return result;
            }
            return result;
        }

        static CPixel[,] cambiocolor(CPixel[,] inimage, string color)
        {
            CPixel[,] result = null;
            int width, height;

            if(inimage != null)
            {
                width = inimage.GetLength(0);
                height = inimage.GetLength(1);
                result = new CPixel[width, height];

                CPixel colores = null;

                if (color == "rojo")
                { 
                    colores = new CPixel(255, 0, 0);             
                }
                else if (color == "verde")
                {
                    colores = new CPixel(0, 255, 0);              
                }
                else if (color == "azul")
                {
                    colores = new CPixel(0, 0, 255);                   
                }
                if(colores != null)
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            result[i, j] = new CPixel(colores.R, colores.G, colores.B);// cambiamos cada pixel por su tonalidad elegida por el usuario
                        }
                    }
                }
                return result; 
            } 
            return result;          
        }
        
        public static CPixel[,] collage(CPixel[,] inimage, CPixel[,] inimage2, CPixel[,] inimage3, CPixel[,] inimage4)
        {
            CPixel[,] result = null;
            
            if (inimage != null || inimage2 != null || inimage3 != null || inimage4 != null)
            {
                // Verifica que las dimensiones de todas las imágenes sean iguales
                int height = inimage.GetLength(0);
                int width = inimage.GetLength(1);

                if (height != inimage2.GetLength(0) || width != inimage2.GetLength(1) ||
                    height != inimage3.GetLength(0) || width != inimage3.GetLength(1) ||
                    height != inimage4.GetLength(0) || width != inimage4.GetLength(1))
                {
                   result = new CPixel[height, width];

                    // Combinar las imágenes en el collage
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            result[i, j] = MediaPixel(inimage[i, j], inimage2[i, j], inimage3[i, j], inimage4[i, j]);
                        }
                    }

                    return result;
                }
            }
            return result;
        }
        
        public static CPixel MediaPixel(CPixel pixel1, CPixel pixel2, CPixel pixel3, CPixel pixel4)
        {
            int averageRed = (pixel1.R + pixel2.R + pixel3.R + pixel4.R) / 4;
            int averageGreen = (pixel1.G + pixel2.G + pixel3.G + pixel4.G) / 4;
            int averageBlue = (pixel1.B + pixel2.B + pixel3.B + pixel4.B) / 4;

            return new CPixel(averageRed, averageGreen, averageBlue);
        }
      
        static void menu() //Menu
        {
            Console.WriteLine("Qué operación quieres realizar?");
            Console.WriteLine("0: Terminar");
            Console.WriteLine("1: Enmarcar la foto de verde");
            Console.WriteLine("2: Hacer un collage con 4 fotos");
            Console.WriteLine("3: Cambiar la tonalidad de la foto");
            Console.WriteLine("4: Invertir la imagen");
        }

        static void Main(string[] args)
        {
            string fotoelegida,fotoelegida2, fotoelegida3, fotoelegida4, colorelegido;
            string usuario, correo, contraseña;
            int opcion;
            bool guardada;

            Console.WriteLine("Introduce usuario");
            usuario = Console.ReadLine();
            StreamWriter escribir_usuario = new StreamWriter(usuario + ".txt");
            escribir_usuario.WriteLine(usuario);
            Console.WriteLine("Introduce correo");
            correo = Console.ReadLine();
            escribir_usuario.WriteLine(correo);
            Console.WriteLine("Contraseña, por favor");
            contraseña = Console.ReadLine();
            escribir_usuario.WriteLine(contraseña);

            CUsuario us = new CUsuario();
            us.nombre = usuario;
            us.correo = correo;
            us.contraseña = contraseña;

            Console.WriteLine("Qué imagen quieres manipular?"); // Comprobamos si la imagen que nos da el usuario existe
            fotoelegida = Console.ReadLine();
            CPixel[,] inimage = PNGtoMATRIZ(fotoelegida);

            if(inimage != null)
            {
                menu();
                opcion = Convert.ToInt32(Console.ReadLine());

                while (opcion != 0)
                {
                    switch (opcion)
                    {
                        case 0;
                            Console.WriteLine("Terminando Programa");
                            break;

                        case 1:
                            Console.WriteLine("Enmarcando de color negro...");
                            enmarcar(inimage);
                            if(enmarcar(inimage) != null)
                            {
                                guardada = MATRIZtoPNG(inimage, fotoelegida);
                                if(guardada == true)
                                {
                                    Console.WriteLine("Foto guardada tio");
                                }
                            }
                            break;
                            
                        case 2:
                            Console.WriteLine("Para el collage necesito 3 fotos mas tio");
                            Console.WriteLine("El nombre de la segunda foto tio");
                            fotoelegida2 = Console.ReadLine();
                            CPixel[,] inimage2 = PNGtoMATRIZ(fotoelegida2);
                            Console.WriteLine("El nombre de la tercera foto tio");
                            fotoelegida3 = Console.ReadLine();
                            CPixel[,] inimage3 = PNGtoMATRIZ(fotoelegida3);
                            Console.WriteLine("El nombre de la cuarta foto tio");
                            fotoelegida4 = Console.ReadLine();
                            CPixel[,] inimage4 = PNGtoMATRIZ(fotoelegida4);
                            
                            CPixel[,] fotocollage = collage(inimage,inimage2,inimage3,inimage4);

                            if(collage(inimage,inimage2,inimage3,inimage4) != null)
                            {
                                string collg = "collage.png";
                                guardada = MATRIZtoPNG(fotocollage,collg);
                                
                                if(guardada == true)
                                {
                                    Console.WriteLine("Foto guardada como collage.png tio");
                                }
                            }
                            break;
                        
                        case 3:
                            Console.WriteLine("¿Quieres cambiar a rojo, verde o azul?: ");
                            colorelegido = Console.ReadLine().ToLower();

                            Console.WriteLine("Perfecto, " + colorelegido + ", manos a la obra");
                            cambiocolor(inimage, colorelegido);
                            if(cambiocolor(inimage, colorelegido) != null)
                            {
                                guardada = MATRIZtoPNG(inimage, fotoelegida);
                                if(guardada == true)
                                {
                                    Console.WriteLine("Foto guardada tio");
                                }
                            }
                            break;

                        case 4:
                            Console.WriteLine("Espera un segundin mientras invierto la foto...");
                            reverse(inimage);
                            if(reverse(inimage) != null)
                            {
                                guardada = MATRIZtoPNG(inimage, fotoelegida);
                                if(guardada == true)
                                {
                                    Console.WriteLine("Foto guardada tio");
                                }
                            }
                            break;

                        default:
                            Console.WriteLine("Código de operacion incorrecto chavalín");
                            break;
                    }
                    menu();
                    opcion = Convert.ToInt32(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("No se pudo cargar la imagen");
            }
                        
            Console.WriteLine("Hasta otra! :)");
            Console.ReadKey();
        }
    }
}