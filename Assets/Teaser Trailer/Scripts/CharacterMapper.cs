using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMapper
{
    public static int GetIndex(char c)
    {
        const string chars = " .-:+=*#%$@§¤";
        int index = chars.IndexOf(c);
        return index == -1 ? 0 : index;
    }

    public static char[,] FromImage(Texture2D image)
    {
        char[,] result = new char[image.width, image.height];

        for (int y = 0; y < image.height; ++y)
        {
            for (int x = 0; x < image.width; ++x)
            {
                Color32 c = image.GetPixel(x, image.height - y - 1);

                // todo: this is crude
                if (c == Color.white)
                    result[x, y] += '#';
                else
                if (c == Color.green)
                    result[x, y] += '*';
                else
                if (c == Color.cyan)
                    result[x, y] += '§';
                else
                if (c == Color.blue)
                    result[x, y] += '.';
                else
                    result[x, y] += ' ';
            }
        }

        return result;
    }

    readonly public static string[] testMap = new string[36]
    {
        "+                                                              +",
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "                ################################                " ,
        "                #...................-----------#                " ,
        "                #.....................---------#                " ,
        "                #......................--------#                " ,
        "                #......$..................-----#                " ,
        "                #............................--#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                #.............@................#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                #...................$..........#                " ,
        "                #................$....$........#                " ,
        "                #.................$......$.....#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                #..............................#                " ,
        "                ################################                " ,
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "                                                                " ,
        "+                                                              +"
    };

}

