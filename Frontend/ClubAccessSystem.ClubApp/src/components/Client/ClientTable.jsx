import React from 'react'

import { MagnifyingGlassIcon, PencilIcon, TrashIcon } from "@heroicons/react/24/outline";

export default function ClientTable({ clients, searchQuery, setSearchQuery }) {
    const handleDelete = (clienteId) => {
        console.log("Eliminar cliente con ID:", clienteId);
        // Aquí puedes agregar la lógica para eliminar el usuario
    };

    // Función para manejar la edición de un usuario
    const handleEdit = (clienteId) => {
        console.log("Editar cliente con ID:", clienteId);
        // Aquí puedes agregar la lógica para editar el usuario
    };

      
  return (
    <div>
      {/* Campo de búsqueda */}
      <div className="relative flex-1 max-w-md mb-6">
        <input
          type="text"
          placeholder="Buscar Cliente..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <MagnifyingGlassIcon className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
      </div>

      {/* Tabla de usuarios */}
      <div className="overflow-x-auto rounded-lg border border-gray-200">
        <table className="min-w-full divide-y divide-gray-200 bg-white">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                ID
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Nombre
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Contacto
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Tipo Cliente
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Opciones
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200">
            {clients.map((client) => (
              <tr key={client.clienteId}>
                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                  {client.clienteId}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {client.nombre}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {client.contacto}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500" value={client.tipoCliente}>
                  {client.nombreTipoCliente }
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  <div className="flex space-x-4">
                    <button
                      onClick={() => handleEdit(client.clienteId)}
                      className="text-blue-600 hover:text-blue-900"
                    >
                      <PencilIcon className="w-5 h-5" />
                    </button>
                    <button
                      onClick={() => handleDelete(client.clienteId)}
                      className="text-red-600 hover:text-red-900"
                    >
                      <TrashIcon className="w-5 h-5" />
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
