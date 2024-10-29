using Healt.Datos;
using Healt.Entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Healt.Logica
{
    public class PersonalMedicoLogica
    {
        private readonly PacienteDatos _pacienteDatos;
        private readonly EstudiosDatos _estudiosDatos;
        private readonly EstudiosRealizadosDatos _estudiosRealizadosDatos;
        public PersonalMedicoLogica()
        {
            _pacienteDatos = new PacienteDatos();
            _estudiosDatos = new EstudiosDatos();
            _estudiosRealizadosDatos = new EstudiosRealizadosDatos();

        }

        public async Task SolicitarEstudios()
        {
            try
            {
                Console.Clear();
                MostrarEncabezado();

                string Cedula = ObtenerCedula();
                string Examen = await ObtenerEstudiosPorEspecialidad();
                string Resultado = "En espera";
                string fecha = DateTime.Now.ToString("yyyy-MM-dd");


                var nuevoEstudio = new EstudiosRealizadosModelos(
                    
                    Cedula,
                    Examen,
                    Resultado,
                    fecha
                );
                await _estudiosRealizadosDatos.InsertarEstudioRealizado(new List<EstudiosRealizadosModelos> { nuevoEstudio });








                Console.WriteLine("\n¡Paciente registrado exitosamente!");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al registrar el paciente: {ex.Message}");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }


        private void MostrarEncabezado()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("     Solicitar examenes");
            Console.WriteLine("=================================\n");
        }

        private string ObtenerCedula()
        {
            string cedula;
            do
            {
                Console.Write("Ingrese la cedula del paciente: ");
                cedula = Console.ReadLine()?.Trim() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(cedula));
            return cedula;
        }

        private string ObtenerEspecialidad()
        {
            string[] especialidades = {
                    "Cardiología",
                    "Radiología",
                    "Laboratorio Clínico",
                    "Neurología",
                    "Dermatología",
                    "Ginecología",
                    "Neumología",
                    "Oftalmología",
                    "Endocrinología",
                    "Gastroenterología",
                    "Urología",
                    "Oncología",
                    "Reumatología",
                    "Pediatría",
                    "Ortopedia",
                    "Otorrinolaringología"
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

            return especialidades[seleccion - 1];
        }

        public async Task<string> ObtenerEstudiosPorEspecialidad()
        {
            // Obtener la especialidad seleccionada
            string especialidad = ObtenerEspecialidad();

            // Llamar al método que lee los estudios por especialidad
            List<EstudiosModelo> estudios = await _estudiosDatos.LeerEstudiosPorEspecialidad(especialidad);

            // Verificar si se encontraron estudios
            if (estudios.Count == 0)
            {
                Console.WriteLine($"No se encontraron estudios para la especialidad: {especialidad}");
                return null; // Retornar null si no se encontraron estudios
            }
            else
            {
                Console.WriteLine($"\nEstudios disponibles para la especialidad '{especialidad}':");
                for (int i = 0; i < estudios.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {estudios[i].Nombre}: {estudios[i].Precio} COP");
                }

                // Solicitar al usuario que seleccione un estudio
                int seleccion;
                do
                {
                    Console.Write("\nSeleccione el número del estudio que desea: ");
                } while (!int.TryParse(Console.ReadLine(), out seleccion) || seleccion < 1 || seleccion > estudios.Count);

                // Retornar el nombre del estudio seleccionado
                return estudios[seleccion - 1].Nombre;
            }
        }



        public async Task InformacionPacientePorCedula()
        {
            Console.Clear();
            MostrarEncabezado2();
            string cedula = ObtenerCedula();
            // Obtener la información del paciente
            var paciente = await _pacienteDatos.BuscarPorCedula(cedula);
            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            // Obtener los estudios realizados
            var estudiosRealizados = await _estudiosRealizadosDatos.LeerEstudiosPorCedula(cedula);

            // Mostrar la información del paciente
            MostrarInformacionPaciente(paciente, estudiosRealizados);

        }



        private void MostrarEncabezado2()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("     Informacion Paciente");
            Console.WriteLine("=================================\n");
        }
        private void MostrarInformacionPaciente(PacienteModelos paciente, List<EstudiosRealizadosModelos> estudiosRealizados)
        {
            Console.WriteLine($"Cédula: {paciente.Cedula}");
            Console.WriteLine($"Nombre: {paciente.Nombre}");
            Console.WriteLine($"Edad: {paciente.Edad}");
            Console.WriteLine($"Diagnostico: {paciente.Diagnostico}");
            Console.WriteLine($"Cama: {paciente.CamaAsignada}");
            Console.WriteLine($"Fecha de Ingreso: {paciente.FechaIngreso}");
            Console.WriteLine("Historial Médico:");
            Console.WriteLine(paciente.HistorialMedico);

            if (estudiosRealizados.Any())
            {
                Console.WriteLine("\nEstudios Realizados:");
                foreach (var estudio in estudiosRealizados)
                {
                    Console.WriteLine($"- {estudio.Examen} (Fecha: {estudio.Fecha}, Resultado: {estudio.Resultado})");
                }
            }
            else
            {
                Console.WriteLine("\nNo se encontraron estudios realizados para este paciente.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }






    }
}
