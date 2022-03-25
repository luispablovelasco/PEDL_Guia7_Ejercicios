using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PEDL_Guia7_Ejercicios
{
    class AVL
    {

        public int valor;
        public AVL NodoIzquierdo;
        public AVL NodoDerecho;
        public AVL NodoPadre;
        public int altura;
        public Rectangle prueba;
        private DibujaAVL arbol;

        public AVL()
        {

        }

        public DibujaAVL Arbol
        {
            get { return arbol; }
            set { arbol = value; }
        }

        //Constructor
        public AVL(int valorNuevo, AVL izquierdo, AVL derecho, AVL padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquierdo;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
        }

        //Funcion para insertar un nuevo valor en el arbol AVL
        public AVL Insertar(int valorNuevo, AVL Raiz)
        {
            if (Raiz == null)
            {
                Raiz = new AVL(valorNuevo, null, null, null);
            }
            else if (valorNuevo > Raiz.valor)
            {
                Raiz.NodoDerecho = Insertar(valorNuevo, Raiz.NodoDerecho);
            }
            else
            {
                MessageBox.Show("Valor Existete en el arbol", "Error", MessageBoxButtons.OK);
            }

            //Realiza las rotaciones simples o doble segun el caso
            if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) == 2)
            {
                if (valorNuevo < Raiz.NodoIzquierdo.valor)
                {
                    Raiz = RotacionIzquierdaSimple(Raiz);
                }
                else
                {
                    Raiz = RotacionIzquierdaDoble(Raiz);
                }
            }

            if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 2)
            {
                if (valorNuevo > Raiz.NodoDerecho.valor)
                {
                    Raiz = RotacionDerechaSimple(Raiz);
                }
                else
                {
                    Raiz = RotacionDerechaDoble(Raiz);
                }
            }

            Raiz.altura = max(Alturas(Raiz.NodoIzquierdo), Alturas(Raiz.NodoDerecho)) + 1;
            return Raiz;
        }

        //Funcion de prueba para realizar las rotaciones

        //Funcion para obtener que rama es mayor
        private static int max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }
        private static int Alturas(AVL Raiz)
        {
            return Raiz == null ? -1 : Raiz.altura;
        }

        AVL nodoE, nodoP;

        public AVL Eliminar(int valorEliminar, ref AVL Raiz)
        {
            if (Raiz != null)
            {
                if (valorEliminar < Raiz.valor)
                {
                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                }
                else
                {
                    if (valorEliminar > Raiz.valor)
                    {
                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                    }
                    else
                    {
                        //Posicionado sobre el elemento a eliminar

                        AVL NodoEliminar = Raiz;
                        if (NodoEliminar.NodoDerecho == null)
                        {
                            Raiz = NodoEliminar.NodoIzquierdo;

                            if (Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                //MessageBox.Show("nodoE" + nodoE.valor.Tostring());

                                if (valorEliminar < nodoE.valor)
                                {
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                }
                                else
                                {
                                    nodoE = RotacionDerechaSimple(nodoE);
                                }
                            }

                            if (Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if (valorEliminar > nodoE.NodoDerecho.valor)
                                    nodoE = RotacionDerecheSimple(nodoE);

                                else
                                    nodoE = RotacionDerechaDoble(nodoE);
                                nodoP = RotacionDerechaSimple(nodoE);
                            }
                        }
                        else
                        {
                            if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) > 0)
                            {
                                AVL AuxiliarNodo = null;
                                AVL Auxiliar = Raiz.NodoIzquierdo;
                                bool Bandera = false;
                                while (Auxiliar.NodoDerecho != null)
                                {
                                    AuxiliarNodo = Auxiliar;
                                    Auxiliar = Auxiliar.NodoDerecho;
                                    Bandera = true;
                                }
                                Raiz.valor = Auxiliar.valor;
                                NodoEliminar = Auxiliar;
                                if (Bandera == true)
                                {
                                    AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                }
                                else
                                {
                                    Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                }
                                //Realiza las rotaciones simples o dobles segun el caso
                            }
                            else
                            {
                                if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) > 0)
                                {
                                    AVL AuxiliarNodo = null;
                                    AVL Auxiliar = Raiz.NodoDerecho;
                                    bool Bandera = false;
                                    while (Auxiliar.NodoIzquierdo != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoIzquierdo;
                                        Bandera = true;
                                    }
                                    Raiz.valor = Auxiliar.valor;
                                    NodoEliminar = Auxiliar;
                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.NodoIzquierdo = Auxiliar.NodoDerecho;
                                    }
                                    else
                                    {
                                        Raiz.NodoDerecho = Auxiliar.NodoDerecho;
                                    }
                                }
                                else
                                {
                                    if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 0)
                                    {
                                        AVL AuxiliarNodo = null;
                                        AVL Auxiliar = Raiz.NodoIzquierdo;
                                        bool Bandera = false;
                                        while (Auxiliar.NodoDerecho != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoDerecho;
                                            Bandera = true;
                                        }
                                        Raiz.valor = Auxiliar.valor;
                                        NodoEliminar = Auxiliar;
                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                        }
                                        else
                                        {
                                            Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nodo inexistente en el arbol","Error", MessageBoxButtons.OK);
            }
            return nodoP;
        }

        //Seccion de funciones de rotaciones

        //Rotacion Ixquierda simple

    }
}
