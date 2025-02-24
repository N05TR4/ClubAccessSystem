import { ChartBarIcon, ClockIcon, BellIcon } from "@heroicons/react/24/outline"

export default function Dashboard() {
  return (
    <div className="space-y-6">
      <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
        {/* Card Estadísticas */}
        <div className="bg-white rounded-lg shadow-md p-6">
          <div className="flex items-center">
            <ChartBarIcon className="h-6 w-6 text-indigo-600" />
            <h3 className="ml-3 text-lg font-medium text-gray-900">Estadísticas</h3>
          </div>
          <p className="mt-2 text-sm text-gray-500">Resumen de actividad del mes</p>
          <div className="mt-4">
            <div className="text-2xl font-bold text-gray-900">2,450</div>
            <p className="text-sm text-green-600">+15.8% vs. mes anterior</p>
          </div>
        </div>

        {/* Card Actividad Reciente */}
        <div className="bg-white rounded-lg shadow-md p-6">
          <div className="flex items-center">
            <ClockIcon className="h-6 w-6 text-indigo-600" />
            <h3 className="ml-3 text-lg font-medium text-gray-900">Actividad Reciente</h3>
          </div>
          <div className="mt-4 space-y-3">
            <div className="flex items-center">
              <div className="w-2 h-2 bg-green-400 rounded-full"></div>
              <p className="ml-3 text-sm text-gray-500">Nueva actualización completada</p>
            </div>
            <div className="flex items-center">
              <div className="w-2 h-2 bg-blue-400 rounded-full"></div>
              <p className="ml-3 text-sm text-gray-500">Reunión programada para mañana</p>
            </div>
          </div>
        </div>

        {/* Card Notificaciones */}
        <div className="bg-white rounded-lg shadow-md p-6">
          <div className="flex items-center">
            <BellIcon className="h-6 w-6 text-indigo-600" />
            <h3 className="ml-3 text-lg font-medium text-gray-900">Notificaciones</h3>
          </div>
          <div className="mt-4 space-y-3">
            <div className="flex items-center justify-between">
              <p className="text-sm text-gray-500">Mensajes sin leer</p>
              <span className="bg-indigo-100 text-indigo-600 py-1 px-3 rounded-full text-xs">
                4 nuevos
              </span>
            </div>
            <div className="flex items-center justify-between">
              <p className="text-sm text-gray-500">Tareas pendientes</p>
              <span className="bg-indigo-100 text-indigo-600 py-1 px-3 rounded-full text-xs">
                8 tareas
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
