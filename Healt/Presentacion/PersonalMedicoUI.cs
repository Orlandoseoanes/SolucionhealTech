using Healt.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healt.Presentacion
{
    public class PersonalMedicoUI
    {
        private readonly UsuarioLogica _usuarioLogica;
        private readonly PersonalMedicoLogica _personalMedicoLogica;

        public PersonalMedicoUI(UsuarioLogica usuarioLogica)
        {
            _usuarioLogica = usuarioLogica;
            _personalMedicoLogica =new  PersonalMedicoLogica();
        }


        public async Task EjecutarAsync(string nombreUsuario)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine($"   ¡Bienvenido a Hel111ixa, Dr. {nombreUsuario}!   ");
            Console.WriteLine("=========================================");

            while (true)
            {
                Console.WriteLine(@"
                ¿Qué desea hacer?
                1. Solicitar estudios adicionales
                2. Consultar información de paciente
                3. Agregar información a la historia clinica de un paciente
                4. Salir");

                Console.WriteLine("=========================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        await _personalMedicoLogica.SolicitarEstudios();
                        break;
                    case "2":
                        await _personalMedicoLogica.InformacionPacientePorCedula();
                        break;
                    case "3":
                        //AUN NO ES FUNCIONAL;
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        break;
                }
            }
        }
    }
}

