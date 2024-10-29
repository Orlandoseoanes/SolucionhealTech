using Healt.Entidades;
using Healt.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healt.Presentacion
{
    public class RecepcionistaUI
    {
        private readonly UsuarioLogica _usuarioLogica;
        private readonly RecepcionistaLogica _recepcionistaLogica;

        public RecepcionistaUI(UsuarioLogica usuarioLogica)
        {
            _usuarioLogica = usuarioLogica;
            _recepcionistaLogica = new RecepcionistaLogica();
        }

        public async Task EjecutarAsync(string nombreUsuario)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine($"   ¡Bienvenido a Hel111ixa, {nombreUsuario}!   ");
            Console.WriteLine("=========================================");

            while (true)
            {
                Console.WriteLine(@"
                ¿Qué desea hacer?
                1. Ingresar un paciente
                2. Consultar disponibilidad de camas
                3. Todos los pacientes
                4. Cambiar de especialidad a paciente
                5. Mostrar Estudios disponibles
                6. Salir");

                Console.WriteLine("=========================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        await _recepcionistaLogica.IngresarPaciente();
                        break;
                    case "2":
                        await MostrarCamasDisponiblesAsync();
                        break;
                    case "3":
                        await MostrarPacientesAsync();
                        break;
                    case "4":
                        string[] especialidades = {
                            "Cardiología",
                            "Neurología",
                            "Pediatría",
                            "Medicina Interna",
                            "Cirugía General",
                            "Traumatología",
                            "Ginecología"
                        };
                        Console.WriteLine("\nEspecialidades disponibles:");
                        for (int i = 0; i < especialidades.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {especialidades[i]}");
                        }
                        int seleccion;
                        do
                        {
                            Console.Write("\nSeleccione el número de la especialidad: ");
                        } while (!int.TryParse(Console.ReadLine(), out seleccion) ||
                                seleccion < 1 ||
                                seleccion > especialidades.Length);
                        string nuevaEspecialidad = especialidades[seleccion - 1];
                        Console.Write("Ingrese el ID del paciente: ");
                        string id = Console.ReadLine();
                        await _recepcionistaLogica.ActualizarEspecialidadPaciente(id, nuevaEspecialidad);
                        Console.Clear();
                        Console.Write("Paciente actualizado correctamente");
                        break;
                    case "5":
                        await _recepcionistaLogica.MostrarEstudiosEnConsola();
                        break;
                    case "6":

                        return;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        break;
                }
            }
        }

        private async Task MostrarCamasDisponiblesAsync()
        {
            var camas = await _recepcionistaLogica.ObtenerCamasDisponibles();

            if (camas == null || !camas.Any())
            {
                Console.WriteLine("\nNo hay camas disponibles en este momento.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCamas Disponibles:");
            Console.WriteLine("------------------");

            foreach (var cama in camas)
            {
                Console.WriteLine($"Número de Cama: {cama.NumeroCama}");
                Console.WriteLine($"Tipo: {cama.TipoCama}");
                Console.WriteLine($"Precio: ${cama.Precio}");
                Console.WriteLine("------------------");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private async Task MostrarPacientesAsync()
        {
            var pacientes = await _recepcionistaLogica.ObtenerPacientes();

            if (pacientes == null || !pacientes.Any())
            {
                Console.WriteLine("\nNo hay pacientes registrados en este momento.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPacientes Registrados:");
            Console.WriteLine("----------------------");

            foreach (var paciente in pacientes)
            {
                Console.WriteLine($"ID: {paciente.Id}");
                Console.WriteLine($"Nombre: {paciente.Nombre}");
                Console.WriteLine($"Edad: {paciente.Edad}");
                Console.WriteLine($"Diagnóstico: {paciente.Diagnostico}");
                Console.WriteLine($"Especialidad Asignada: {paciente.EspecialidadAsignada}");
                Console.WriteLine($"Cama Asignada: {paciente.CamaAsignada}");
                Console.WriteLine($"Fecha de Ingreso: {paciente.FechaIngreso}");
                Console.WriteLine($"Historial Médico: {paciente.HistorialMedico}");
                Console.WriteLine("----------------------");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        




    }
}
