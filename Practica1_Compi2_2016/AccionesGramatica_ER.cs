using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;


namespace Practica1_Compi2_2016
{
    class AccionesGramatica_ER : Accion
    {
        public Object do_action(ParseTreeNode pt_node)
        {
            return action(pt_node);
        }

        public Object action(ParseTreeNode node)
        {
            Object result = null;

            switch (node.Term.Name.ToString())
            {

                case "S0":
                    if (node.ChildNodes.Count == 1) 
                    {
                        // ChildNodes[0] es igual a S1
                        result = action(node.ChildNodes[0]);
                    }
                    break;
                case "S1":
                    if (node.ChildNodes.Count == 3)
                    {
                        // ChildNodes[1] = L_CONTENIDO
                        result = action(node.ChildNodes[1]);
                    }
                    break;

                case "L_CONTENIDO":

                    MessageBox.Show(node.ChildNodes[0].Term.ToString());

                    if (node.ChildNodes[0].Term.Name.Equals("CONJ"))
                    {
                        // ChildNodes[2] = id
                        // ChildNodes[4] = CONJUNTO
                        // ChildNodes[6] = L_CONTENIDO

                        result = action(node.ChildNodes[2]);
                        // result tiene el id, lo guardo en donde lo vaya a usar
                        // luego lo uso para CONJUNTO

                        result = action(node.ChildNodes[4]);
                        MessageBox.Show(Convert.ToString(result));
                        // ChildNodes[6] = L_CONTENIDO
                        // esta es la recursividad
                        result = action(node.ChildNodes[6]);
                    }
                    else if (node.ChildNodes[0].Term.Name.Equals("error"))
                    {
                        // ChildNodes[2] = EXP_REGULAR
                        // result tendra expresion regular
                        result = action(node.ChildNodes[2]);

                        // ChildNodes[13] = E_CONT
                        // Contiene todo lo demas de contenido despues del error

                        result = action(node.ChildNodes[13]);
                    }
                    else if (node.ChildNodes[0].Term.Name.Equals("identificador"))
                    {
                        // ChildNodes[0] = id
                        result = action(node.ChildNodes[0]);
                        // ChildNodes[2] = EXP_REGULAR
                        result = action(node.ChildNodes[2]);
                        // ChildNodes[4] = RETORNO 
                        result = action(node.ChildNodes[4]);
                        // ChildNodes[6] = L_CONTENIDO
                        // esta es la recursividad
                        result = action(node.ChildNodes[6]);

                    }
                    break;

                case "E_CONT":

                    if (node.ChildNodes.Count == 1) 
                    {
                        result = action(node.ChildNodes[0]);
                    }
                    else if (node.ChildNodes.Count == 2)
                    {
                        //ChildNodes[0] = E_CONT

                        result = action(node.ChildNodes[0]);
                        //ChildNodes[1] = CONTENIDO
                        result = action(node.ChildNodes[1]);
                    }


                    break;
                case "CONJUNTO":
                    if (node.ChildNodes.Count == 1)
                    {
                        result = action(node.ChildNodes[0]);
                    }
                    else {
                        String s1 = Convert.ToString(action(node.ChildNodes[0]));
                        String s2 = Convert.ToString(action(node.ChildNodes[2]));
                        String r = s1 + node.ChildNodes[1].Term.ToString() + s2;

                        result = r;
                    }
                    break;  
                  
                case "EXP_REGULAR":
                    if (node.ChildNodes.Count == 1) 
                    {
                        result = node.ChildNodes[0].Token.Value;
                    }
                    else if (node.ChildNodes.Count == 2) 
                    {
                        result = action(node.ChildNodes[0]);
                        result = action(node.ChildNodes[1]);
                    }
                    else if (node.ChildNodes.Count == 3) 
                    {
                        if (node.ChildNodes[0].Term.Name.Equals("EXP_REGULAR"))
                        {

                            String s1 = Convert.ToString(action(node.ChildNodes[0]));
                            String s2 = Convert.ToString(action(node.ChildNodes[1]));
                            String s3 = Convert.ToString(action(node.ChildNodes[2]));
                            String r = s1 + s3 + s2;

                            MessageBox.Show(r);
                            result = r;

                        }
                        else {
                            String s1 = node.ChildNodes[0].Token.Value.ToString();
                            String s2 = Convert.ToString(action(node.ChildNodes[1]));
                            String s3 = node.ChildNodes[2].Token.Value.ToString();

                            String r = s1 + s2 + s3;

                            MessageBox.Show(r);
                            result = r;

                        }
                    }
                    break;
                case "CUANTIFICADORES":
                    result = node.ChildNodes[0].Token.Value;
                    break;
                case "OPERANDO":
                    MessageBox.Show(node.ChildNodes[0].Term.Name.ToString());
                    result = node.ChildNodes[0].Token.Value;
                    break;
                case "OPERADOR":
                    result = node.ChildNodes[0].Token.Value;
                    break;
                case "TIPO":
                    result = node.ChildNodes[0].Token.Value;
                    break;
            }

            return result;
        }


    }
}
