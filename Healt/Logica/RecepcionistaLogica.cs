using Healt.Datos;
using Healt.Entidades;

namespace Healt.Logica
{
    public class RecepcionistaLogica
    {
        private readonly PacienteDatos _pacienteDatos;
        private readonly CamaDatos _camaDatos;
        private readonly EstudiosDatos _estudiosDatos;

        public RecepcionistaLogica()
        {
            _pacienteDatos = new PacienteDatos();
            _camaDatos = new CamaDatos();
            _estudiosDatos = new EstudiosDatos();
        }
        public async Task IngresarPaciente()
        {
            try
            {
                Console.Clear();
                MostrarEncabezado();
                string nombre = ObtenerNombre();
                string edad = ObtenerEdad();
                string cedula = ObtenerCedula();
                string diagnostico = ObtenerDiagnostico();
                string especialidad = ObtenerEspecialidad();
                string cama = await ObtenerCamaAsignada();
                string fechaIngreso = DateTime.Now.ToString("yyyy-MM-dd");
                string historial = ObtenerHistorialMedico();

                var nuevoPaciente = new PacienteModelos(
                    nombre,
                    edad,
                    cedula,
                    diagnostico,
                    especialidad,
                    cama,
                    fechaIngreso,
                    historial
                );

                await _pacienteDatos.GuardarPacientes(new List<PacienteModelos> { nuevoPaciente });

                await _camaDatos.ActualizarCamaAocupada(int.Parse(cama));

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
            Console.WriteLine("     INGRESO DE NUEVO PACIENTE");
            Console.WriteLine("=================================\n");
        }

        private string ObtenerNombre()
        {
            string nombre;
            do
            {
                Console.Write("Ingrese el nombre del paciente: ");
                nombre = Console.ReadLine()?.Trim() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(nombre));
            return nombre;
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

        private string ObtenerEdad()
        {
            string edad;
            int edadNum;
            do
            {
                Console.Write("Ingrese la edad del paciente: ");
                edad = Console.ReadLine()?.Trim() ?? string.Empty;
            } while (!int.TryParse(edad, out edadNum) || edadNum < 0 || edadNum > 150);
            return edad;
        }

        private string ObtenerDiagnostico()
        {
            string diagnostico;
            do
            {
                Console.Write("Ingrese la razón de entrada del paciente: ");
                diagnostico = Console.ReadLine()?.Trim() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(diagnostico));
            return diagnostico;
        }

        private string ObtenerEspecialidad()
        {
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

            return especialidades[seleccion - 1];
        }

        private async Task<string> ObtenerCamaAsignada()
        {
            var camasDisponibles = await _camaDatos.ObtenerCamasDisponibles();

            if (!camasDisponibles.Any())
            {
                Console.WriteLine("No hay camas disponibles en este momento.");
                return string.Empty;
            }

            Console.WriteLine("\nCamas disponibles:");
            for (int i = 0; i < camasDisponibles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Cama {camasDisponibles[i].NumeroCama} - " +
                                $"Tipo: {camasDisponibles[i].TipoCama} - " +
                                $"Precio: ${camasDisponibles[i].Precio:N2}");
            }

            int seleccion;
            do
            {
                Console.Write("\nSeleccione el número de la cama: ");
            } while (!int.TryParse(Console.ReadLine(), out seleccion) ||
                    seleccion < 1 ||
                    seleccion > camasDisponibles.Count);

            return camasDisponibles[seleccion - 1].NumeroCama.ToString();
        }

        private string ObtenerHistorialMedico()
        {
            Console.WriteLine("\nIngrese el historial médico del paciente (presione Enter dos veces para finalizar):");
            var historial = new System.Text.StringBuilder();
            string linea;

            while (true)
            {
                linea = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(linea))
                {
                    if (historial.Length == 0)
                    {
                        Console.WriteLine("El historial no puede estar vacío. Por favor, ingrese información:");
                        continue;
                    }
                    break;
                }
                historial.AppendLine(linea);
                Console.WriteLine($"Historial actual: \n{historial}"); // Muestra el historial actual
            }

            return historial.ToString().Trim();
        }

        public async Task<List<CamasModelos>> ObtenerCamasDisponibles()
        {
            try
            {
                return await _camaDatos.ObtenerCamasDisponibles();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener camas: {ex.Message}");
                return new List<CamasModelos>();
            }
        }

        public async Task<List<PacienteModelos>> ObtenerPacientes()
        {
            try
            {
                return await _pacienteDatos.CargarPacientes();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener pacientes: {ex.Message}");
                return new List<PacienteModelos>();
            }
        }


        public string ObtenerEspecialidades()
        {
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

            return especialidades[seleccion - 1];
        }

        public async Task ActualizarEspecialidadPaciente(string id, string nuevaEspecialidad)
        {
            var paciente = await _pacienteDatos.BuscarPorId(id);
            if (paciente != null)
            {
                paciente.EspecialidadAsignada = nuevaEspecialidad;
                await _pacienteDatos.ActualizarEspecialidadAsignada(id, nuevaEspecialidad);
            }
            else
            {
                Console.WriteLine($"No se encontró el paciente con ID: {id}");
            }
        }

        public async Task MostrarEstudiosEnConsola()
        {
            var estudiosList = await _estudiosDatos.LeerEstudios();
            const int anchoTotal = 80;
            const int anchoEspecialidad = 25;
            const int anchoNombre = 35;
            const int anchoPrecio = 15;

            // Función helper para formatear precio (ahora acepta decimal)
            string FormatearPrecio(decimal precio) => $"${precio:N0}";

            // Encabezado
            Console.WriteLine("\nLista de Estudios Médicos Disponibles");
            Console.WriteLine(new string('-', anchoTotal));
            Console.WriteLine(
                "{0,-" + anchoEspecialidad + "} {1,-" + anchoNombre + "} {2," + anchoPrecio + "}",
                "Especialidad", "Nombre del Estudio", "Precio");
            Console.WriteLine(new string('-', anchoTotal));

            // Contenido
            foreach (var estudio in estudiosList.OrderBy(e => e.EspecialidadMedica))
            {
                Console.WriteLine(
                    "{0,-" + anchoEspecialidad + "} {1,-" + anchoNombre + "} {2," + anchoPrecio + "}",
                    estudio.EspecialidadMedica.Length > anchoEspecialidad - 3
                        ? estudio.EspecialidadMedica.Substring(0, anchoEspecialidad - 3) + "..."
                        : estudio.EspecialidadMedica,
                    estudio.Nombre.Length > anchoNombre - 3
                        ? estudio.Nombre.Substring(0, anchoNombre - 3) + "..."
                        : estudio.Nombre,
                    FormatearPrecio(estudio.Precio));
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }





    }



}
