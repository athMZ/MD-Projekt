﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public static class ListExtension
    {
        public static bool AddIfNotExists<T>(this List<T> list, T value)
        {

            if (!list.Contains(value))
            {

                list.Add(value);
                return true;
            }
            return false;
        }
    }
}
