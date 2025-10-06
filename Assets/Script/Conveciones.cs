using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Convenciones.Ejemplo { 
    //namespace se utiliza para separar la compilación de los scripts
    public class Conveciones : MonoBehaviour
    {
        [Header("Variables públicas")]
        public int VariableIntPublica; //todas las variables públicas comienzan con PascalCase
        private int variablePrivada; //todas las variables privadas empiezan con camelCase. "_" al comienzo para acceder a esas variables desde otros script
        [Header("Variables privadas")]
        [SerializeField] private int variableSerializable;

        public bool IsActive, HasJump; //Bool's para permisos 
        const float P_NUMBER=0.2f;

        public UnityEvent OnExecuteMethod; //todos los eventos empiezan con "On". Utilizar verbos


        public int Propiedad 
        { 
            get 
            { 
                return variablePrivada; 
            } 

            private set
            { 
                if (value == 0) 
                { 
                    variablePrivada = value; 
                } 
            } 
        } //esto es una propiedad

        [ContextMenu("TestMetodo")]
        void MetodoPascalCase() //los metodos se escriben con PascalCase 
        {
            int variableInterna=20; //variables internas en camel case


            void Recibir(int variableInterna) //props siempre en camelCase
            {
                Debug.Log(variableInterna);
            }
        
            Recibir(variableInterna);
            OnExecuteMethod.Invoke(); //Eventos siempre al final del codigo
        }



    }
}
