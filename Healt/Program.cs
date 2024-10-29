using Healt.Logica;
using Healt.Presentacion;


namespace HealthTechApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await MainAsync(args);
        }

        static async Task MainAsync(string[] args)
        {
            string opcion;

            while (true) 
            {
                Console.Clear(); // Limpiar la consola en cada iteración
                Console.WriteLine(@"Bienvenido a Hel111ixa, Software de Gestión Hospitalaria, llevando la atención al siguiente nivel
                                   1. Iniciar Sesión 
                                   2. Salir");

                opcion = Console.ReadLine();

                // Este es el menú
                switch (opcion)
                {
                    case "1":
                        await new UsuariosUI(new UsuarioLogica()).VerificarUsuarioAsync();
                        break;
                    case "2":
                        Console.WriteLine("Saliendo..."); // Mensaje opcional al salir
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elige una opción válida."); // Manejo de opción inválida
                        break;
                }
            } // Continúa hasta que el usuario elija salir
        }
    }
}

