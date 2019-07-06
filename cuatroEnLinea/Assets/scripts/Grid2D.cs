﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
    public int width = 10; //para el ancho de la matriz
    public int height = 10; //para el alto de la matriz
    public GameObject puzzlePiece; //para guardar la referencia de una esfera, que creara todas las esferas
    private GameObject[,] grid; // es la matriz de 2 dimensiones para trabajar

    void Start()
    {
        grid = new GameObject[width, height]; //inicializa la matriz con width y heght
        for (int x = 0; x < width; x++) //para mover una posicion en x cuando se termine de realizar una columna en y
        {
            for (int y = 0; y < height; y++) //para hacer las columnas de de la matriz en la pantalla
            {
                GameObject go = GameObject.Instantiate(puzzlePiece) as GameObject; //crea un objeto lo que hay en la variable puzzlePieze
                Vector3 posicion = new Vector3(x, y, 0); //crea una variable de tipo vector3 para saber donde poner la esfera segun los for
                go.transform.position = posicion; // le asignamos a el objeto go la pisicion que adquirimos anteriormente
                grid[x, y] = go; //a la matriz le asignamos el objeto recien creado y ubicado
            }
        }
    }

    [SerializeField] bool estaJugando = true; //muestra en el inspector una casilla con chulo mientras esta jugando y cuando para queda vacia
    int cambioColor = 2; //para cambiar el color de la esfera en cada iteracion

    void Update()
    {
        Vector3 mPosicion = Camera.main.ScreenToWorldPoint(Input.mousePosition); //obtiene la posicion del mouse
        int x = (int)(mPosicion.x + 0.5f); //convierte la posicion del mouse en x, en una variable entera
        int y = (int)(mPosicion.y + 0.5f); //convierte la posicion del mouse en y, en una variable entera

        if (estaJugando) //desde que no alla un ganador estara verdadera y si hay un ganador se volvera falsa
        {
            if (x >= 0 && y >= 0 && x < width && y < height && Input.GetKeyDown(KeyCode.Space)) //verifica si "x" y "y" estan dentro del rango de la matriz y si se presiona la tecla space
            {
                GameObject go = grid[x, y]; //crea un objeto en la posicion de la matriz indicada
                if (cambioColor % 2 == 0) //condicion para hacer que la esfera se ponga de color diferente cada que presione space 
                {
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.red); //cambia el color de la esfera a rojo
                    VerificGanador(x, y, Color.red); //llamado de funcion para ver si hay 4 en linea
                }
                else
                {
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.green); //cambia el color de la esfera a verde
                    VerificGanador(x, y, Color.green); //llamado de funcion para ver si hay 4 en linea
                }
                
                cambioColor += 1; //aumenta la variable para cambiar el color
            }
        }
        
        
    }

    public void VerificGanador(int x, int y, Color colorcito) //funcion para verificar si hay cuatro en linea
    {
        int contador = 0; //cuenta las esferas que hay en una linea del mismo

        for (int i = x - 3; i <= x + 3; i++) //bucle para iterar desde 3 posiciones antes, hasta 3 posiciones despues de la esfera seleccionada
        {
            if (i >= 0 && i < width) //verifica que i este dentro del rango de la matriz
            {
                if (grid[i, y].GetComponent<MeshRenderer>().material.color == colorcito) //si las esferas son del mismo color
                {
                    contador++; //se suma 1 a contador
                    if (contador == 4) //si el contador es igual a 4 
                    {
                        Debug.Log("gana fila"); //muestra que linea gano
                        estaJugando = false; //hace la variable estaJugando falsa para que se salga del loop principal y deje de cambiar el color de las esferas
                    }
                }
                else
                {
                    contador = 0; //si no entra a las otras condiciones significa que no hay gandor, y vuelve el contador a cero
                }
            }
            
        }
        contador = 0; //se hace el contador 0 para que no alla problemas con el siguiente for

        for (int i = y - 3; i <= y + 3; i++) //bucle para iterar desde 3 posiciones antes, hasta 3 posiciones despues de la esfera seleccionada
        {
            if (i >= 0 && i < height) //verifica que i este dentro del rango de la matriz
            {
                if (grid[x, i].GetComponent<MeshRenderer>().material.color == colorcito) //si las esferas son del mismo color
                {
                    contador++; //se suma 1 a contador
                    if (contador == 4) //si el contador es igual a 4
                    {
                        Debug.Log("gana columna"); //muestra que linea gano
                        estaJugando = false; //hace la variable estaJugando falsa para que se salga del loop principal y deje de cambiar el color de las esferas
                    }
                }
                else
                {
                    contador = 0; //si no entra a las otras condiciones significa que no hay gandor, y vuelve el contador a cero
                }
            }
            
        }
        contador = 0; //se hace el contador 0 para que no alla problemas con el siguiente for

        int vert1 = x - 3; //se crea una variable para que itere en x y empiese desde la izquierda
        for (int i = y - 3; i <= y + 3; i++) //bucle para iterar desde 3 posiciones antes, hasta 3 posiciones despues de la esfera seleccionada
        {
            if (i >= 0 && i < height && vert1 >= 0 && vert1 < width) //verifica que i este dentro del rango de la matriz y tambien vert1
            {
                if (grid[vert1, i].GetComponent<MeshRenderer>().material.color == colorcito) //si las esferas son del mismo color
                {
                    contador++; //se suma 1 a contador
                    if (contador == 4) //si el contador es igual a 4
                    {
                        Debug.Log("gana diagonal"); //muestra que linea gano
                        estaJugando = false; //hace la variable estaJugando falsa para que se salga del loop principal y deje de cambiar el color de las esferas
                    }
                }
                else
                {
                    contador = 0; //si no entra a las otras condiciones significa que no hay gandor, y vuelve el contador a cero
                } 
            }
            vert1 ++; //se aumenta la variable que reemplaza a x en 1 para que se valla moviendo a la derecha
        }
        contador = 0; //se hace el contador 0 para que no alla problemas con el siguiente for

        int vert2 = y + 3; //se crea una variable para itere en y, y empieze desde arriba
        for (int i = x - 3; i <= x + 3; i++) //bucle para iterar desde 3 posiciones antes, hasta 3 posiciones despues de la esfera seleccionada
        {
            if (i >= 0 && i < height && vert2 >= 0 && vert2 < width) //verifica que i este dentro del rango de la matriz y tambien vert2
            {
                if (grid[i, vert2].GetComponent<MeshRenderer>().material.color == colorcito) //si las esferas son del mismo color
                {
                    contador++; //se suma 1 a contador
                    if (contador == 4) //si el contador es igual a 4
                    {
                        Debug.Log("gana diagonal"); //muestra que linea gano
                        estaJugando = false; //hace la variable estaJugando falsa para que se salga del loop principal y deje de cambiar el color de las esferas
                    }
                }
                else
                {
                    contador = 0; //si no entra a las otras condiciones significa que no hay gandor, y vuelve el contador a cero
                }
            }
            vert2--; //se disminuye la variable que reemplaza a y en 1 para que se valla bajando
        }
    }
}