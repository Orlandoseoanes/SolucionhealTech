using Healt.Datos;
using Healt.Entidades;

namespace Healt.Logica
{
    public class PacienteLogica
    {
        private readonly PacienteDatos pacienteDatos;

        public PacienteLogica()
        {
            pacienteDatos = new PacienteDatos();
        }

        /// Obtiene la lista de todos los pacientes.
        public async Task<List<PacienteModelos>> ObtenerPacientes()
        {
            return await pacienteDatos.CargarPacientes();
        }

        /// Obtiene un paciente específico por su ID.
        public async Task<PacienteModelos> ObtenerPaciente(string id)
        {
            var pacientes = await ObtenerPacientes();
            return pacientes.FirstOrDefault(p => p.Id == id);
        }

        /// Cambia la especialidad asignada a un paciente.
        public async Task CambiarEspecialidad(string id, string especialidadNueva)
        {
            var pacientes = await ObtenerPacientes();
            var paciente = pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente != null)
            {
                paciente.EspecialidadAsignada = especialidadNueva; // Cambiar la especialidad
                await pacienteDatos.GuardarPacientes(pacientes); // Guardar cambios en la base de datos
            }
            else
            {
                Console.WriteLine($"No se encontró el paciente con ID: {id}");
            }
        }

        /// Agrega un nuevo paciente a la lista.
        public async Task AgregarPaciente(PacienteModelos nuevoPaciente)
        {
            var pacientes = await ObtenerPacientes();
            pacientes.Add(nuevoPaciente);
            await pacienteDatos.GuardarPacientes(pacientes); // Guardar cambios en la base de datos
        }

        /// Elimina un paciente de la lista.
        public async Task EliminarPaciente(string id)
        {
            var pacientes = await ObtenerPacientes();
            var paciente = pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente != null)
            {
                pacientes.Remove(paciente); // Eliminar paciente
                await pacienteDatos.GuardarPacientes(pacientes); // Guardar cambios en la base de datos
            }
            else
            {
                Console.WriteLine($"No se encontró el paciente con ID: {id}");
            }
        }
    }
}
