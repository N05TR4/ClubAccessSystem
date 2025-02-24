export default function Settings() {
    return (
      <div className="max-w-2xl mx-auto bg-white rounded-lg shadow">
        <div className="px-6 py-4 border-b">
          <h2 className="text-xl font-semibold text-gray-900">Configuración</h2>
          <p className="mt-1 text-sm text-gray-500">Administra tu información personal y preferencias</p>
        </div>
        <div className="p-6">
          <form className="space-y-6">
            <div>
              <label htmlFor="name" className="block text-sm font-medium text-gray-700">
                Nombre
              </label>
              <input
                type="text"
                id="name"
                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
                placeholder="Tu nombre"
              />
            </div>
  
            <div>
              <label htmlFor="email" className="block text-sm font-medium text-gray-700">
                Correo electrónico
              </label>
              <input
                type="email"
                id="email"
                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
                placeholder="tu@ejemplo.com"
              />
            </div>
  
            <div>
              <label htmlFor="notifications" className="block text-sm font-medium text-gray-700">
                Notificaciones
              </label>
              <select
                id="notifications"
                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
              >
                <option>Todas las notificaciones</option>
                <option>Solo importantes</option>
                <option>Ninguna</option>
              </select>
            </div>
  
            <div className="flex items-center">
              <button
                type="submit"
                className="bg-indigo-600 text-white px-4 py-2 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
              >
                Guardar cambios
              </button>
            </div>
          </form>
        </div>
      </div>
    )
  }
  
  