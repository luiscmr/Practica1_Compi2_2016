using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;


namespace Practica1_Compi2_2016
{
    class Analizador
    {
        private Grammar gramatica;

        private Analizador()
        {
            gramatica = null;
        }

        public Analizador(Grammar gramatica)
        {
            this.gramatica = gramatica;
        }

        public Object parse(string str, Accion action)
        {
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser p = new Parser(lenguaje);
            ParseTree s_tree = p.Parse(str);
            if (s_tree.Root != null)
            {
                ActionMaker act = new ActionMaker(s_tree.Root);
                return act.getEval(action);
            }
            else 
            {
                MessageBox.Show("fail, go home!");
            }
            return null;
        }

        private class ActionMaker
        {
            private ParseTreeNode root;

            public ActionMaker(ParseTreeNode pt_root)
            {
                root = pt_root;
            }

            public Object getEval(Accion action)
            {
                //evaluar el árbol
                return action.do_action(root);
            }
        }
    }
}
