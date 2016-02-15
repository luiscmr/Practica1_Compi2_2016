using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;

namespace Practica1_Compi2_2016
{
    interface Accion
    {
        Object do_action(ParseTreeNode pt_node);
        Object action(ParseTreeNode pt_node);
    }
}
