﻿using System;
using System.Collections;
using System.Collections.Generic;


public class C_Sudoku
{
    private Case[,] s;

    public C_Sudoku()
    {
        s = new Case[9, 9];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s[i, j] = new Case(i, j);
            }
        }



        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 9; i++)
            {


                // algo adj matt
                //Meme x et y
                for (int a = 0; a < 9; a++)
                {
                    if (a != i) s[i, j].addadj(s[a, j]);

                }

                for (int a = 0; a < 9; a++)
                {
                    if (a != j) s[i, j].addadj(s[i, a]);
                }
                //Meme case
                for (int b = 0; b < 9; b++)
                {
                    for (int a = 0; a < 9; a++)
                    {

                        int k1 = s[a, b].getM();
                        int k2 = s[i, j].getM();
                        if (k1 == k2 && (a != i && b != j))
                        {
                            s[i, j].addadj(s[a, b]);
                        }

                    }
                }

            }

        }
    }

    public void display_matrice_adj()
    {
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(s[i, j].getV());
                s[i, j].displayadj();
            }
        }
    }
    public Case[,] getS()
    {
        return s;
    }
    public Case getcCase(int noCase)
    {
        int j = -1;
        for (int i = 0; i < noCase; i++)
        {
            if (i % 9 == 0)
            {
                j++;
            }

        }

        return s[noCase % 9, j];
    }

    public void setS(Case[,] s)
    {
        this.s = s;
    }

    public void fill_sudoku(int[] result)
    {
        int j = -1;

        for (int i = 0; i < 81; i++)
        {
            if (i % 9 == 0)
            {
                j++;
            }

            if (result[i] != -1)
            {
                s[i % 9, j].setV(result[i]);
            }

        }
    }

    public void display()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Console.WriteLine(s[j, i].getV());
            }
        }
    }


    public void reset()
    {

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s[j, i].setV(0);
            }
        }
    }

    public int[] greedyColoring(int[] result)
    {

        // A temporary array to store the available colors. True
        // value of available[cr] would mean that the color cr is
        // assigned to one of its adjacent vertices

        bool[] available = StoreColors();

        s[0, 0].setV(0);

        this.display();




        // Assign colors to remaining V-1 vertices
        AsignColors(available);

        int j = -1;

        for (int u = 0; u < 81; u++)
        {
            if (u % 9 == 0)
            {
                j++;
            }

            result[u] = s[u % 9, j].getV();

        }


        this.fill_sudoku(result);

        return result;
    }

    private void AsignColors(bool[] available)
    {

        int j = -1;

        for (int u = 0; u < 81; u++)
        {
            if (u % 9 == 0)
            {
                j++;
            }

            if (s[u % 9, j].getV() == 0)
            {
                // Process all adjacent vertices and flag their colors
                // as unavailable
                IEnumerator<Case> it = s[u % 9, j].GetEnum();
                while (it.MoveNext())
                {
                    int i = it.Current.getV();

                    if (i >= 0)
                    {
                        if (s[i, 0].getV() != -1 && s[i, 0].getV() < 9)
                        {
                            available[s[i, 0].getV()] = true;
                        }
                    }

                }

                // Find the first available color
                int cr;
                for (cr = 0; cr < 8; cr++)
                {
                    if (available[cr] == false)
                    {
                        break;
                    }
                }

                s[u % 9, j].setV(cr); // Assign the found color

                // Reset the values back to false for the next iteration
                it = s[u % 9, j].GetEnum();
                while (it.MoveNext())
                {
                    int i = it.Current.getV();

                    if (i >= 0)
                    {
                        if (s[i, 0].getV() != -1 && s[i, 0].getV() < 9)
                        {

                            available[s[i, 0].getV()] = false;
                        }
                    }

                }
            }
        }
    }



    private bool verif_color(int clr)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (s[j, i].getV() == clr)
                {
                    return true;
                }
            }
        }

        return false;
    }


    private bool[] StoreColors()
    {
        bool[] available = new bool[9];
        for (int cr = 0; cr < 9; cr++)
        {
            available[cr] = false;
        }

        return available;
    }
}